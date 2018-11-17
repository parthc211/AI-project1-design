using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour {

    public GameObject[] prefabs;
    public Camera cam;

    public LayerMask mask;

    private int current_prefab = 0;

    public List<KeyValuePair<GameObject, int>> spawnedObjects = new List<KeyValuePair<GameObject, int>>();

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            try
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, mask))
                {
                    GameObject spawn = Instantiate(prefabs[current_prefab], hit.point, Quaternion.identity) as GameObject;
                    spawnedObjects.Add(new KeyValuePair<GameObject, int>(spawn, current_prefab + 1));
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
            }

            Debug.Log("Spawned Objects: " + spawnedObjects.Count);
        }
	}

    public List<KeyValuePair<GameObject, int>> GetListOfSpawnedObjects()
    {
        return spawnedObjects;
    }

    public void ChangePrefab(int index)
    {
        current_prefab = index;
    }
}
