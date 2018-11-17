using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Spawner : MonoBehaviour {

    public GameObject[] prefabs;
    public string[] units = { "black","yellow","blue","white"};
    public Camera cam;
    
    int current_unit = 0;
  
    Node [,] grid;
    public Terrain terrain;
    public LayerMask mask;

	// Use this for initialization
	void Start () {
        grid = new GridMaker(terrain).Grid;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            try
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                if (!EventSystem.current.IsPointerOverGameObject()){
                    if (Physics.Raycast(ray, out hit, mask))
                    {
                        Debug.Log("3");
                        Instantiate(prefabs[current_unit], hit.point, Quaternion.identity);

                        //These are your indices in the NODE grid.
                        int i = 50 + (int)(hit.point.x) % 100;
                        int j = 50 + (int)(hit.point.z) % 100;
                        
                        // Updates the node property. 
                        grid[i,j].AddUnit(units[current_unit]);

                        //Testing purpose. To check if the list is updated or not.
                        //foreach(string s in grid[i, j].GetUnits())
                        //{
                        //    Debug.Log(s);
                        //}
                        //Debug.Log("Positions:" + grid[i,j].Position) ;

                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
            }
        }
	}

    //Called by the UI buttons. Updates the current prefab.
    public void changePrefab(int index)
    {

        current_unit = index;
        Debug.Log("Index=" + current_unit);
    }


    //using this to display the grid system in terrain. Visible only in scene tab.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector3 size = new Vector3(1, 0, 1);
        if (grid != null)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Gizmos.DrawCube(grid[i, j].Position, size);
                }
            }
        }
    }

}
