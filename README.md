# MDVJ-Fundamentals
Unity version 2021.3.24f1

## Table of Contents  

[Activity 1.1](#act1.1)

[Rectilinear Movement 1.1](#rect_move_1)

[Rectilinear Movement 1.2](#rect_move_2)

[Events and Rectilinear Movement](#events_1)

[GLFS Excercise](#glfs_ex)

[Introduction to 2D Game Development | Sprites](#dev_2D)

[Introduction to 2D Game Development | Physics and Maps](#introduction-to-2d-game-development--physics-and-maps)

[Introduction to 2D Game Development | Background and Camera](#introduction-to-2d-game-development--background-and-camera)

[Audio and Pooling](#audio-and-pooling)

<a name="act1.1"/>

# Activity 1.1

ES

Proyecto de Unity con el objetivo de servir como plataforma para la implementación de distintos sistemas.

Para este primer avance se agregó:

- El 'Third Person' asset de Unity.
- Un Skybox del paquete AllSky Free.
- Modelos 3D del paquete 'Polygon Starter' de Synty Studios.

EN


Unity project with the objective of serving as a platform for the implementation of different systems.

For this first iteration it was added:

- The Unity 'Third Person' asset.
- A Skybox from the AllSky Free package.
- 3D models from the 'Polygon Starter' package by Synty Studios.

![MDCJ04](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/fad171ce-193f-4ec3-91cd-c4fcc2e2ad94)


<a name="rect_move_1"/>

# Rectilineal movement 1.1

For this exercise we learned how to move a object toward a goal using different method.
In this first example we use the translate method from the transform component of the game object as follows.

        Vector3 direction = goal.position - this.transform.position;
        float step = speed * Time.deltaTime;
        this.transform.Translate(direction.normalized * step);

![Activity_Follower_01](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/eb45d8d4-1c5d-4aa2-9712-5e93f159bc86)

By using the 'LookAt' method we manage to make the game object to face the goal where it moving. 

        this.transform.LookAt(goal.position);
        ...
        this.transform.Translate(direction.normalized * step, Space.World);

![Activity_Follower_02](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/c3792dd8-e7a6-4ef4-af06-143b74c2a9d4)


Finally, as a more advance implementation. Now, the object is moved by using the Unity Input Axis.
Itwas also implemented a system that when the player collides with the cyllinders, these change their state and add a point to the player score.

![Activity_Follower_03](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/2fb8e0f3-7608-4efb-a25f-104728fbf879)


<a name="rect_move_2"/>

# Rectilineal movement 1.2

## Part I

We improved over the last implementation of the rectilinear movement by adding a smooth rotation towards the target that the object is following.
To achieve this we use the Quaternion.Slerp method as follows.

        void ApplySlerpMovement()
        {
            Vector3 direction = goal.position - this.transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            float step = speed * Time.deltaTime;

            if (direction.magnitude > deadZone)
                this.transform.Translate(0f, 0f, step);
        }

To avoid jitter we added a if statement ta only allows the object to move if the distance between target and object is greater than a specified radius aka 'deadZone'.

![Activity_3_slerp](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/2eccf053-df1f-4516-b6c0-109c171828ec)


## Part II

In another excerisise, a waypont systme was creted. In this, the 'WaypointFollower' script recieves a list of gameObjects as waypoints.
Using the code from the previous part, the player moves towards the waypoint. However a new system was immplemented. When the player
reaches the current waypoint, the 'SetNewTarget' method changes the objective towards the next gameObject in the list.

        void SetNewTarget()
        {
            targetIndex++;

            if(targetIndex >= waypoints.Count)
                targetIndex = 0;

            currentTarget = waypoints[targetIndex];

            Waypoint waypoint = currentTarget.GetComponent<Waypoint>();
            if(waypoint != null) 
                waypoint.SetAsCurrent();
        }

![Activity_3_customway](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/758d5c6b-2b06-4419-a119-21f427822ac0)

## Part III

By using Unity's and 'WaypointCircuit' and 'WaypointProgressTracker' scripts we created a circuit that the 'Player' can follow.
There is a hidden sphere that works as a target that the 'Player' follows. The sphere moves along the circuit when the player comes closer.

![Activity_3_unitywaypoints](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/a83f0e75-381b-42d5-9a8c-9a4f79f59013)

## Part IV

Finally, by using adding and using the rigidbody component on a cylinder we move the object using the Input axis.

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float step = Time.deltaTime * speed;
        m_Rigidbody.MovePosition(transform.position + direction * Time.deltaTime * step);
        
![Activity_3_physics](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/c375687f-ac23-43ca-8078-c15e5ec807d0)

<a name="events_1"/>

# Events and Rectilinear movement

## Part I

For this first part we used Unity's Canvas event system and Collision event system.

When the buttons named 'Normal' or 'Turbo' are pressed, these change the speed of the player movement.
This uses the button event system to call the method 'SetSpeedTo(int speed)' from the player's speed.

    public void SetSpeedTo(int targetSpeed)
    {
        speed = targetSpeed;
        speedText.text = speed.ToString();
    }

For the colliion events, by using the 'OnTriggerEnter(Collider other)' we check the tags of the object being collected
to decide wether to increae the player's score or to reduce it's movement speed.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            CollectItem();
            Destroy(other.gameObject);
        }
            

        else if (other.CompareTag("Hazard"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }      
    }

![Events_1](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/9666341a-ce55-4ed2-a257-d84e69823271)

## Part II

As a next step, we added some obstacles that block the players movement. However, if the player picks a special item
the obstacles will move of the way of the player when they are near. To achieve this, by using the delegates, we notify the
obstacles when the player has picked up the item.

        public class PlayerInteractions : MonoBehaviour
        {

            public delegate void Message();
            public event Message OnItemTouch;

            ...
        }


        public class Obstacles : MonoBehaviour
        {  
            public float speed = 2f;
            bool escapeEnable =  false;

            void Start()
            {
                GameManager._player.GetComponent<PlayerInteractions>().OnItemTouch += BeginEscape;
            }

            private void Update()
            {
                if(escapeEnable)
                    Escape();
            }

            public void Escape()
            {
                Vector3 direction = GameManager.GetPlayerPosition() - this.transform.position;
                float step = speed * Time.deltaTime;

                if (Vector3.Distance(GameManager.GetPlayerPosition(), transform.position) < 2f)
                    this.transform.Translate(-direction.normalized * step, Space.World);
            }

            public void BeginEscape()
            {
                escapeEnable = true;
            }
        }


![Events_2](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/4cba5945-9569-411f-a3f4-49ccae1d6ebb)

## Part III

Another feature was the implemantion of a basic teleportation system through a portal.
This was easily achieved by chaning player position whe overlaping with a object with the Portal script.
The portal moves the player to a it's connected end point called 'exit portal'. To avoid a bug where
the player keeps teleporting forever, the exit portal has the spawn point a little bit away from it's own collider.

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            other.transform.position = new Vector3(exitPortal.spawnPoint.transform.position.x,
                                                   other.transform.position.y,
                                                   exitPortal.spawnPoint.transform.position.z);
    }


![Events_3](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/93878899-5f29-4c58-975d-d76be960c6b1)


## Part IV & V

By using a button event from the UI. We enable an object to move to a static target in the scene. A boolean value is changed
to allow the calling of the movement calculations in the 'Update()'.

        void Update()
        {
            if (!enable) return;

            this.transform.LookAt(goal.position);

            Vector3 direction = goal.position - this.transform.position;

            float step = speed * Time.deltaTime;
            this.transform.Translate(direction.normalized * step, Space.World);
            Debug.DrawRay(this.transform.position, direction, Color.red);
        }

        public void EnableMovement()
        {
            enable = true;
        }
        
![Events_4](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/f2f29444-7423-41b8-bb97-dcaf40082df5)

And by adding a delegate system, we notify this object when the player enters a specified zone where the object can follow it.
When the player is out of the zone, we notify the object so this will stop.

    private void Start()
    {
        goal.gameObject.GetComponent<PlayerInteractions>().OnEnterZone += EnableMovement;
        goal.gameObject.GetComponent<PlayerInteractions>().OnExitZone += DisableMovement;
    }

![Events_5](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/fd1e71f0-84b0-48d9-bb2b-54117f7eed96)


<a name="glfs_ex"/>

# GLFS Exercise

Small exercise to learn how to use GLFS tracking.
For this test I'm tracking jpg and hdr files.

![gits](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/1afcab8f-8c0e-48f7-b568-97df561769b2)


<a name="dev_2D"/>

# Introduction to 2D Game Development | Sprites

![2D_01](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/f295cf6e-2448-4539-ae2c-7b1ff62ed959)

For this prototype first we nedeed to achieve a 2D player movement controller.
This was achieved using Unity's Rigidbody2D.

For the horizontal movement I used the 'Input.GetAxis("Horizontal")' to get the movement direction on the X axis.

        float step = Time.deltaTime * speed;
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal") * step, m_rigidbody.velocity.y, 0f);

We ask if the X value of the direction is lesser or grater tha 0 to know the direction that the sprite should be facing.
By using the SpriteRenderer component we can flip the sprite.

        if (direction.x != 0f)
        {
                if(direction.x < 0f)
                    m_spriteRenderer.flipX = true;
                else if(direction.x > 0f)
                    m_spriteRenderer.flipX = false;
        }

To calculate the jump, we receive the input from the 'SPACE' key. When pressed, we apply a force in the Y axis.
To have a more smooth interaction between the key and the game, the input shoul be checked on the 'Update()' instead than the 'FixedUpdate()'.

        private void Update()
        {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_rigidbody.AddForce(new Vector2(m_rigidbody.velocity.x, jumpForcer));
                    ...
                }
        }

Finally we apply on the 'FixedUpate()' the movement direction calculated both in this method and in the 'Update()'.

        m_rigidbody.velocity = direction;

To have the animations respond to the input. We create a Animator Controller that has an integer parameter named "State".
By switching te state, we change the animation being player. In the scripts the states integers are defined by a enum.

        enum PlayerState { Idle, Run, Jump, Dead };
        PlayerState currentState = PlayerState.Idle;

The full logic looks like this.

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_rigidbody.AddForce(new Vector2(m_rigidbody.velocity.x, jumpForcer));
                SetAnimation(PlayerState.Jump);
                grounded = false;
            }
        }

        private void FixedUpdate()
        {
            float step = Time.deltaTime * speed;
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal") * step, m_rigidbody.velocity.y, 0f);
    
            if (direction.x != 0f)
            {
                if(direction.x < 0f)
                    m_spriteRenderer.flipX = true;
                else if(direction.x > 0f)
                    m_spriteRenderer.flipX = false;

                if (grounded)
                    SetAnimation(PlayerState.Run);

                else
                    SetAnimation(PlayerState.Jump);
            }

            if (direction.x == 0f && grounded)
                SetAnimation(PlayerState.Idle);

            m_rigidbody.velocity = direction;
        }

        private void SetAnimation(PlayerState state)
        {
            m_animator.SetInteger("State", (int)state);
        }


To have the interaction between the enemy 'Zombie' and our player. I created a script for the Zombie. This script get the triggers when
the Player overlaps with the zombie trigger collider.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_animator.SetInteger("State", (int)EnemyState.Attack);
            Controller_2D playerController = other.GetComponent<Controller_2D>();
            playerController.TakeDamage();
        }       
    }

Next chapter I'll be working with 2D Physics and Tiles.


<a name="#introduction-to-2d-game-development--physics-and-maps"/>

# Introduction to 2D Game Development | Physics and Maps

## Physics

This time I'll be working with Unity's 2D physics engine and Tile maps. I had to implement the next scenearios.

### A. Two Objects with no physics.

![a_nophysics](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/ea83a249-c89b-4647-957b-071e4524dc15)

### B. Only one object with physics (Rigidbody 2D).

![B_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/109b6cce-1557-471d-8780-2d396f49fe83)

For this I added a BoxCollider2D and a Rigidbody2D to one of the sprites in the scene. I also made a simple controller
so I can move it around the scene.

        private void FixedUpdate()
        {
            float step = Time.deltaTime * speed;
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal") * step, m_rigidbody.velocity.y, 0f);
            m_rigidbody.velocity = direction;
        }

### C. Two objects with physics (Rigidbody 2D).

I am using the same configuration as before for both objects (BoxCollider2D + Rigidbody2d). Only one has the controller script.

![C_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/0ca51d56-6fd2-4ec1-8e16-7c7500bbe4ec)

I added a Text on the Canvas so I can debug directly to the game viewport. When the object with the controller script collides
with another one, it displays a message in the screen.

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PhysicsObstacle obstacle = collision.gameObject.GetComponent<PhysicsObstacle>();
        
            if (obstacle != null)
            {
                UILog.text = "Debug Log: " + "I am colliding with " + "'" + obstacle.GetName();
            }
        }

In this case, the ground only  has Collider, not Rigidbody.

### D. Two objects have with physics (Rigidbody 2D), but one has more mass than the other.

I increased the mass of the RigidBody2D on the red box to 100. Now, the red object does not move so easily when 
pushed by the blue box.

![D_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/8bf09531-00f2-453f-94be-712f6b061804)

### E. One of the objects is a 'trigger'.

![E_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/536aad92-89fb-47c5-9661-0494a45d72f7)

Enabeling the 'Is Trigger' checkbox in the red box Collider now allows to overlap with the blue box.

         private void OnTriggerEnter2D(Collider2D collision)
         {
             PhysicsObstacle obstacle = collision.gameObject.GetComponent<PhysicsObstacle>();
        
             if (obstacle != null)
             {
                 UILog.text = "Debug Log: " + "I am overlaping a Trigger named " + "'" + obstacle.GetName() + "'.";
             }
         }

I had to disable the Rigidbody for the red box to stop it's free fall.

### F. Two objects have with physics (Rigidbody 2D), but one is also a 'trigger'.

![F_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/0c0134e6-6fa1-4a2a-b9ef-366071c944b8)

I just enable again the Rigidbody for the red box. It still overlaps with the blue box, so it displays the debug message.

### G. One object is 'Kinematic'.

By chaging the body type of the red box to 'Kinematic', no does not move when pushed by the blue box (or any object).

![G_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/bc0c234d-95aa-4cab-a364-3efc0f1b9bdc)

Finally, here is a small simulation where:

- There is a Static object that acts as an impassable barrier (red boxes).
- Exist an Area in which objects that fall into it are propelled forward (Green area with yellow arrow).
- Object that is dragged by another object at a fixed distance (Blue box pushing orange box).
- Object that when colliding with others follows a totally physical behavior (Orange box, gray box and blue box).

The orange box physcs layer allows it to interact with the blue and red boxes but it does not react with the gray box that is in another physics layer.
However, the blue box can interact with the gray one.

https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/e0719526-9197-43e0-bdee-734121df67c5

## Maps

https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/12e6e5b7-e44b-408e-9148-363fbdc4c602

As a second part of this activity, I learnt how to work with tile maps in Unity. First I created a basic level
using a palette. After that I set the collitions for the level using a Tilemap Collider 2D and a Composite Collider 2D.
For the movement of the player and it's animation system I am using the same controller I created for a past exercise.
However I did some changes to add new functionalities, like picking up items. By using Unity triggers, when my player can collect items
and increas it's score.

I also defined a delegate so the player can let know other object in the scene when it collected all of the items.

        public delegate void Message();
        public event Message OnAllItemsPicked;

        ...

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Item"))
            {
                itemsPicked++;
                itemScore.text = itemsPicked.ToString();
                Destroy(collision.gameObject);
        
                if (itemsPicked == 5)
                {
                    OnAllItemsPicked();
                    jumpForcer *= 1.5f;
                }
                    
            }
        }

When 5 itmes are collected, the delegate event 'OnAllItemsPicked()' is called. This event is recieved by the 'Platform' script
and reduces the alpha channel of the sprite renderer and also changes the physics layer.
The object with this script is the one that is blocking the player to go to the next level through a hole in the ground.

        [SerializeField]
        private SpriteRenderer m_renderer;
        
        public Controller_2D robot;
        
        void Start()
        {
            m_renderer = GetComponent<SpriteRenderer>();
            robot.OnAllItemsPicked += ChangeLayer;
        }
        
        void ChangeLayer()
        {
            m_renderer.color = new Color(1f, 1f, 1f, 0.5f);
            gameObject.layer = 9;
        }

For the moving platform on the 'FixedUpdate()' we use a Sin() function to move the platform along the Y axis. We achieve the player
to stay on the platform by making the platform it's parent when 'OnTriggerEnter2D' and unparenting ir when 'OnTriggerExit2D'.

        private void FixedUpdate()
        {
            Vector2 direction = new Vector2(initialPosition.x, initialPosition.y + Mathf.Sin(Time.fixedTime) * range);
        
            m_rigibody.position = direction;
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Transform otherTransform = collision.transform;
                otherTransform.parent = transform;
            }
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Transform otherTransform = collision.transform;
                otherTransform.parent = null;
            }
        }


<a name="#introduction-to-2d-game-development--background-and-camera"/>

# Introduction to 2D Game Development | Background and Camera

## Background scroll

Implementations of three scrolls methods. The first type has two images, once one gets out of the screen it moves back to the
edge of the stack so it can be ready to scroll again in front of the camera.

![Fondo_1](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/2ec99929-1b74-43e5-8e85-bb9fe87714b6)

        void Update()
        {
            float step = Time.deltaTime * speedOffset;
            float horizontal = Input.GetAxis("Horizontal") * step;

            transform.position = new Vector3(transform.position.x + horizontal, 0, transform.position.z);

            if(transform.position.x <= leftLimit)
            {
                transform.position = new Vector3(nextImagePoint.position.x, 0f, transform.position.z);
            }
        }

The second method the background does not move, is startic, it only changes its position to have at any time one background in front
of the moving camera.

![Fondo_2](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/5fb4faa5-48d5-453f-b05e-12c2ff38fa1e)

        public class BackgroundRepeatStatic : MonoBehaviour
        {
            float background_w = 10.24f;
        
            void Update()
            {
                if (Camera.main.transform.position.x >= transform.position.x + background_w)
                {
                    transform.position += new Vector3(background_w * 2f, 0f, 0f);
                }
            }
        }
        
For the thirt type, the scroll is achieved by offseting the texture parameter in the material.

![Fondo_3](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/0a69a7ef-23da-4cf6-8aca-c005f6ab2c48)

        void Update()
        {
            float step = Time.deltaTime * speedOffset;
            float horizontal = Input.GetAxis("Horizontal") * step;
            Offset += horizontal;
            meshRenderer.material.SetVector("_Offset", new Vector2(Offset, 0));
        }


### Parallax

For the parallax effect we simply move the background layers at different velocities. The next gifs shows two methods:
One where the whole Plane is moved.

![Fondo_4](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/6c8c31ed-e0fd-45eb-a8d7-46dc07801556)

And another where only the texture is moved by using it's offset.

![Fondo_5](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/17f57062-ef38-4a17-aab3-ddfc521d2ee6)


### Cinemachine

Using Cinemachine allows to have a more smooth and nice camera movement. Here are two examples with different zones to follow the player.

![Fondo_Cam_1](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/4d1d2248-1f9a-4099-baac-fabdfabbf51f)

![Fondo_Cam_2](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/3dcc4e7d-861d-476c-b58e-f283a21c8a2f)

### Camera limits

By suing camera limits we can stop the camera to move to zones that need to be always offscreen. 

![Fondo_Coll_1](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/58a6bbd6-e88e-4407-827b-291809c14259)

In this example, the camera does not move when the player falls through the pit.

![Fondo_Coll_2](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/e50274de-bff6-46d5-9293-df95582b7ff9)

### Target Group

Implementation of target groups using two different weights.

![Group_1](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/5727ef84-7848-46ca-b647-be8032f748ef)

![Group_2](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/6dcef492-bb3d-43d0-98c0-d6823fc7ee99)


### Zoom

This is a simple zoom control implementation. It changes the camera Ortographic Size by pressing 'W' or 'S'.

![Zoomshish](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/0ad7f125-76d1-42d5-a5ea-00dcd65bd759)

        void Update()
        {
            float zoom = Input.GetAxis("Vertical") * Time.deltaTime * 5f;
            virtualCamera.m_Lens.OrthographicSize += zoom;
        }

### Camera change

A smooth change between cameras using cinemachine is easily achieved by simple turning off and on the cameras.

![Change](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/3cfe03cd-8027-4139-bf1d-3f8878ed9f74)

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                vCam1.SetActive(true);
                vCam2.SetActive(false);
            }
        
            else if (Input.GetKeyDown(KeyCode.E))
            {
                vCam1.SetActive(false);
                vCam2.SetActive(true);
            }
        }


<a name="#audio-and-pooling"/>

# Audio and Pooling

## Audio

For this exercise we learn about audio in Unity. First, here is a simple looping audio that plays on awake.

https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/f06f0d21-2c1c-43ab-8112-bbc8b008e2de

Then, I made the audio 3D by changing the Audio source spatial blend. The next example uses
Dopler: 5, Spread: 100, Min Distance: 5, Max Distance: 100, Volumen Rollof: Linear

By changing the spread and the distance we manage to modidy the behavior of the dopplet effect of the moving sphere.
Also it looks that it also increses the volume of the audio. Not directly in the component, but sound "closer" to the listener. By using the Unity standar values, the doppler effect occurs almost immediately.

https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/4fc64f49-4fd3-43be-b327-e7fccd389928

I also learnt how to use the audio mixer. Here is an example of a basic monster "Groan" whit an echo.

https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/b7521b73-e5d8-4ff2-89cc-f6da7aefe6ca

By playing around a little more, I think I manage to make an ordinary spark attack into something more menacing.
This was achieved by chanhging the pitch and adding a little bit of chorus. I still need to invest more time trying and researching how this channels work as I was unable to detect a true difference bwtween channels.

https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/0dc93851-7e18-471b-af8d-bcfaeb34613e

And changed the mosnter groan to an human one.

https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/737422fc-2c64-488a-88cb-ca0cffef7b36

We also learn how to manage audio from code. In this example we can see a spehere whit a looping audio whit a random
pitch. This plays by pressing 'P', and stops by pressing 'S'. This implementation was perfect for a basic looping step sfx.

https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/af785a15-14c7-4d4e-9de6-b7b1d842141e

        void Update()
        {
            float step = Time.deltaTime * speed;
        
            if (Input.GetKeyDown(KeyCode.P)) {
                m_Paused = false;
                m_AudioSource.Play();
            }
        
            else if(Input.GetKeyDown(KeyCode.S)) {
                m_Paused = true;
                m_AudioSource.Stop();
            }
        
            if (!m_Paused)
            {
                m_AudioSource.pitch = Random.Range(1f, 3f);
                transform.position += Vector3.right * step;
            }        
        }

In another exaple, I tried to change the audio volume of an immpact sfx based on the velocity of the moving object.

https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/e80febf0-af26-4b0f-809e-1d6ad65b3ca4

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Obstacle"))
            {
                hitVol = speed * 0.1f;
                m_audioSource.PlayOneShot(m_audioClip, hitVol);
            }
        }

## Pooling

For pooling I worked on a system (that still needs polish) that adds items to the pool once the player picks them.
When an object is collected it is added to the pool and it stop being displayed. Each object has a counter, when it reaches 3 it is destroyed. In the scene it is always at least 1 item. When there is less that 1 items in the scene, at least one the items in the pool returns to the scene.

https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/3064c418-1731-4c14-beda-343977954c26

        public void AddToPool(Item item)
        {
        
            item.index++;
            item.gameObject.SetActive(false);
        
            if (item.index == 3)
            {
                if(itemPool.Contains(item))
                {
                    itemPool.Remove(item);
                }
                gameObject.GetComponent<AudioSource>().PlayOneShot(itemDestroyed, 1f);
                Destroy(item.gameObject);
                return;
            }
        
            
        
            if (!itemPool.Contains(item))        
            {
               if(itemPool.Count < poolMaxSize)
                {
                    gameObject.GetComponent<AudioSource>().PlayOneShot(itemBrandNew, 1f);
                    itemPool.Add(item);             
                }                
            }
        
            else
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(itemAdded, 1f);
            }
        
            ManageItems();
        }





