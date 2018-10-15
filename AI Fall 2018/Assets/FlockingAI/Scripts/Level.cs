using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public Transform memberPrefab;
    public Transform enemyPrefab;
    public int numberOfMembers=20;
    public int numberOfEnemies=10;
    public List<Member> members;
    public List<Enemy> enemies;
    public float bounds;
    public float spawnRadius;
    public Terrain terrain;
    private void Start ()
    {
        // Initializes the members and enemies list
        members = new List<Member>();
        enemies = new List<Enemy>();

        // Spawns member units and enemies, whose quantity can be changed in the editor
        // using the Inspector
        //Spawn(memberPrefab, numberOfMembers);
        //Spawn(enemyPrefab, numberOfEnemies);
        Spawn(20);
        // Adds the spawned members to the above initalized list
        members.AddRange(FindObjectsOfType<Member>());
        enemies.AddRange(FindObjectsOfType<Enemy>());
	}

    // Generic Spawn function, takes in 2 parameters
    // The type of unit we want to spawn and the number of units we want to spawn
    private void Spawn(int count)
    {
        //Spawning enemies
        float x,y, z;
        
        Vector3 pos ;
        for (int i = -50; i <= 50; i++)
        {
            for (int j = -50; j <= 50; j++)
            {
                if (i == 50 || i == -50 || j == 50 || j == -50)
                {
                    y = (int)terrain.SampleHeight(new Vector3(i, 0, j));
                    y += 1.5f;
                    Instantiate(enemyPrefab, new Vector3(i, y, j), Quaternion.identity);
                }
            }
        }


        //Spawning units
        for (int i = 0; i < 40; i++)
        {
            x = Random.Range(-50,50);
            z = Random.Range(-50,50);

            y = (int)terrain.SampleHeight(new Vector3(x, 0, z));
            if (y >12 || y<10)
            {
                i--;
            }
            else {
                y += 1;
                Instantiate(memberPrefab, new Vector3(x, y, z), Quaternion.identity);
            }
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
