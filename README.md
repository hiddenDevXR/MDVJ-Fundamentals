# MDVJ-Fundamentals
Unity version 2021.3.24f1

## Table of Contents  
[Activity 1.1](#act1.1)  
[Rectilinear Movement](#rect_move)

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

<a name="rect_move"/>

# Rectilineal movement

For this excercise we learnt how to move a object toward a goal using different method.
In this first example we use the translate method from the tranform component of the game object as follows.

        Vector3 direction = goal.position - this.transform.position;
        float step = speed * Time.deltaTime;
        this.transform.Translate(direction.normalized * step);

![Activity_Follower_01](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/eb45d8d4-1c5d-4aa2-9712-5e93f159bc86)

After that by using the look at method we manage to make the game object to face the goal where it moving. 
By using the 'LookAt' method in the update, the object continoysly faces the goal.

        this.transform.LookAt(goal.position);
        ...
        this.transform.Translate(direction.normalized * step, Space.World);

![Activity_Follower_02](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/c3792dd8-e7a6-4ef4-af06-143b74c2a9d4)


Finally, as a more advance implementation. Now, the object is moved by using the Unity Input Axis.
Itwas also implemented a system that when the player collides with the cyllinders, these change their state and add a point to the player score.

![Activity_Follower_03](https://github.com/hiddenDevXR/MDVJ-Fundamentals/assets/86928162/2fb8e0f3-7608-4efb-a25f-104728fbf879)






