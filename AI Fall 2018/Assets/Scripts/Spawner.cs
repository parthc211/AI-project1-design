using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour {

    public GameObject[] prefabs;
    public Camera cam;
    
    int current_prefab = 0;

    public LayerMask mask;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            try
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.Log("2");
                if (Physics.Raycast(ray, out hit, mask))
                {
                    Debug.Log("3");
                    Instantiate(prefabs[current_prefab], hit.point, Quaternion.identity);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
            }
        }
	}

    public void changePrefab(int index)
    {

        current_prefab = index;
        Debug.Log("Index=" + current_prefab);
    }
   
}
