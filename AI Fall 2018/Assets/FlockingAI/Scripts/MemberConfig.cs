using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberConfig : MonoBehaviour {

    public float maxFOV = 120.0f;
    public float maxAcceleration;
    public float maxVelocity;

    // Wander Variables
    public float wanderJitter;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderPriority;

    // Cohesion Variables
    public float cohesionRadius;
    public float cohesionPriority;

    // Alignment Variables
    public float alignmentRadius;
    public float alignmentPriority;

    // Separation Variables
    public float separationRadius;
    public float separationPriority;

    // Avoidance Variables
    public float avoidanceRadius;
    public float avoidancePriority;

    private void Update()
    {
        // Wander Priority manipulation
        if(Input.GetKey(KeyCode.W))
        {
            wanderPriority += 1 * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Q))
        {
            wanderPriority -= 1 * Time.deltaTime;
        }

        // Cohesion Priority manipulation
        if (Input.GetKey(KeyCode.C))
        {
            cohesionPriority += 1 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cohesionPriority -= 1 * Time.deltaTime;
        }

        // Alignment Priority manipulation
        if (Input.GetKey(KeyCode.L))
        {
            alignmentPriority += 1 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.K))
        {
            alignmentPriority -= 1 * Time.deltaTime;
        }

        // Separation Priority manipulation
        if (Input.GetKey(KeyCode.S))
        {
            separationPriority += 1 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            separationPriority -= 1 * Time.deltaTime;
        }
    }
}
