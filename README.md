# MDVJ-Fundamentals
Unity version 2021.3.24f1

## Table of Contents  

[Activity 1.1](#act1.1)

[Rectilinear Movement 1.1](#rect_move_1)

[Rectilinear Movement 1.2](#rect_move_2)

[Events and Rectilinear Movement](#events_1)

[GLFS Excercise](#glfs_ex)

[Introduction to 2D Game Development | Sprites](#dev_2D)

[Introduction to 2D Game Development | Physics and Maps](#2D_Phyxs)


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


<a name="#2D_Phyxs"/>
# Introduction to 2D Game Development | Physics and Maps

asdasd
as
das
dasasd

![B_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/109b6cce-1557-471d-8780-2d396f49fe83)

![C_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/0ca51d56-6fd2-4ec1-8e16-7c7500bbe4ec)

![D_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/8bf09531-00f2-453f-94be-712f6b061804)

![E_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/536aad92-89fb-47c5-9661-0494a45d72f7)

![F_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/0c0134e6-6fa1-4a2a-b9ef-366071c944b8)

![G_Phyxs](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/bc0c234d-95aa-4cab-a364-3efc0f1b9bdc)


https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/e0719526-9197-43e0-bdee-734121df67c5







