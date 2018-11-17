using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceMap : MonoBehaviour {

    public GridMaker grid;
    public Spawner spawner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetValueOnGrid(Node node)
    {
        List<KeyValuePair<GameObject, int>> spawnedObjects = spawner.GetListOfSpawnedObjects();

        foreach(KeyValuePair<GameObject, int> x in spawnedObjects)
        {
        }
    }
}
