using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public Transform memberPrefab;
    public Transform enemyPrefab;
    public int numberOfMembers;
    public int numberOfEnemies;
    public List<Member> members;
    public List<Enemy> enemies;
    public float bounds;
    public float spawnRadius;

	private void Start ()
    {
        // Initializes the members and enemies list
        members = new List<Member>();
        enemies = new List<Enemy>();

        // Spawns member units and enemies, whose quantity can be changed in the editor
        // using the Inspector
        Spawn(memberPrefab, numberOfMembers);
        Spawn(enemyPrefab, numberOfEnemies);

        // Adds the spawned members to the above initalized list
        members.AddRange(FindObjectsOfType<Member>());
        enemies.AddRange(FindObjectsOfType<Enemy>());
	}

    // Generic Spawn function, takes in 2 parameters
    // The type of unit we want to spawn and the number of units we want to spawn
    private void Spawn(Transform prefab, int count)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), 2.0f, Random.Range(-spawnRadius, spawnRadius)), Quaternion.identity);
        }
    }

    // A function to get all the neighboring members within a certain radius
    // Takes in two parameters
    // The member whose neighbors we want to find
    // The radius in which we want to look for the neighbors
    public List<Member> GetNeighbors(Member member, float radius)
    {
        // Initialize a list to store the neighbors found
        List<Member> neighborsFound = new List<Member>();

        foreach(var otherMember in members)
        {
            // if we are looking at the member itself we don't want to add it as a neighbor
            // so we continue
            if (otherMember == member)
                continue;

            // Add the member as neighbor if it is in the defined radius
            if(Vector3.Distance(member.transform.position, otherMember.transform.position) <= radius)
            {
                neighborsFound.Add(otherMember);
            }
        }

        return neighborsFound;
    }

    // Similar function to getting the neighbors
    // But instead of neighbors we get the list of enemies near the member
    public List<Enemy> GetEnemies(Member member, float radius)
    {
        List<Enemy> returnEnemies = new List<Enemy>();

        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(member.position, enemy.transform.position) <= radius)
            {
                returnEnemies.Add(enemy);
            }
        }

        return returnEnemies;
    }
}
