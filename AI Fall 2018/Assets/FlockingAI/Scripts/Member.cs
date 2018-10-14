using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : MonoBehaviour {

    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    public Level level;
    public MemberConfig conf;

    private Vector3 wanderTarget;

	private void Start ()
    {
        level = FindObjectOfType<Level>();
        conf = FindObjectOfType<MemberConfig>();

        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
	}

    private void Update()
    {
        // The units here are moved using transform.position
        // After calculating the acceleration and stuff, we need to move them using rigidbody
        // I think it would be something like rigidbody.AddForce() function

        // TODO: add keyboard inputs to change the value of different attributes of the using keystrokes

        acceleration = Combine();
        acceleration = Vector3.ClampMagnitude(acceleration, conf.maxAcceleration);

        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, conf.maxVelocity);

        velocity.y = 0;

        position = position + velocity * Time.deltaTime;

        transform.position = position;

        // TODO: move the members using Rigidbody instead of transforms
    }

    // Calculate the Wander of a member
    protected Vector3 Wander()
    {
        float jitter = conf.wanderJitter * Time.deltaTime;
        wanderTarget += new Vector3(RandomBinomial() * jitter, 0.0f, RandomBinomial() * jitter);
        wanderTarget = wanderTarget.normalized;
        wanderTarget *= conf.wanderRadius;

        Vector3 targetInLocalSpace = wanderTarget + new Vector3(conf.wanderDistance, 0.0f, conf.wanderDistance);
        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);

        targetInWorldSpace -= this.position;

        return targetInWorldSpace.normalized;
    }

    // Calcluate the cohesion between the members
    private Vector3 Cohesion()
    {
        Vector3 cohesionVector = new Vector3();
        int countMembers = 0;

        var neighbors = level.GetNeighbors(this, conf.cohesionRadius);

        if (neighbors.Count == 0)
            return cohesionVector;

        foreach (var member in neighbors)
        {
            if(IsInFOV(member.position))
            {
                cohesionVector += member.position;
                countMembers++;
            }
        }

        if (countMembers == 0)
            return cohesionVector;

        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this.position;
        cohesionVector = Vector3.Normalize(cohesionVector);

        return cohesionVector;
    }

    // Calculate the alignment of the members
    private Vector3 Alignment()
    {
        Vector3 alignVector = new Vector3();

        var members = level.GetNeighbors(this, conf.alignmentRadius);

        if (members.Count == 0)
            return alignVector;

        foreach (var member in members)
        {
            if(IsInFOV(member.position))
            {
                alignVector += member.velocity; 
            }
        }

        return alignVector.normalized;
    }

    // Calculate the separation between members
    private Vector3 Separation()
    {
        Vector3 separateVector = new Vector3();

        var members = level.GetNeighbors(this, conf.separationRadius);

        if (members.Count == 0)
            return separateVector;

        foreach (var member in members)
        {
            if(IsInFOV(member.position))
            {
                Vector3 movingTowards = this.position - member.position;
                
                if(movingTowards.magnitude > 0)
                {
                    separateVector += movingTowards.normalized / movingTowards.magnitude;
                }
            }
        }

        return separateVector.normalized;
    }

    // Calculate the avoidance to avoid the enemies
    private Vector3 Avoidance()
    {
        Vector3 avoidVector = new Vector3();

        var enemyList = level.GetEnemies(this, conf.avoidanceRadius);

        if (enemyList.Count == 0)
            return avoidVector;

        foreach (var enemy in enemyList)
        {
            avoidVector += RunAway(enemy.transform.position);
        }

        return avoidVector.normalized;
    }

    // Vector to calculate the velocity needed to run away from the enemies
    private Vector3 RunAway(Vector3 target)
    {
        Vector3 neededVelocity = (position - target).normalized * conf.maxVelocity;

        return neededVelocity - velocity;
    }

    // Calculate the final Vector based on the priorities of different attributes
    virtual protected Vector3 Combine()
    {
        Vector3 finalVec = conf.cohesionPriority * Cohesion() +
                           conf.wanderPriority * Wander() +
                           conf.alignmentPriority * Alignment() +
                           conf.separationPriority * Separation() +
                           conf.avoidancePriority * Avoidance();

        return finalVec;
    }

    // Helper function
    private float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }

    // Checks if the member is in the field of view of another member or not.
    // We can change the max FOV of the member config class to balance it properly, using the inspector
    private bool IsInFOV(Vector3 vec)
    {
        return Vector3.Angle(this.velocity, vec - this.position) <= conf.maxFOV;
    }
}
