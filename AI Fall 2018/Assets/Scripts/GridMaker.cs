using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Node{
   
    Vector3 position;

    
    public Node(Vector3 pos)
    {
        position = pos;
    }
    
}

public class GridMaker : MonoBehaviour {
    public Terrain terrain;
    public Node[,] grid = new Node[100, 100];
  

    public Vector2 gridSizeInWorld;

	// Use this for initialization
	void Start () {
        Vector3 start = new Vector3(50, 10, -50);
        Vector3 end = terrain.terrainData.size;
        int i, j, x = 50, z = -50;
        int radius = 1;
        Debug.LogFormat(""+Terrain.activeTerrain.SampleHeight(new Vector3(-47,0,-16)));

        gridSizeInWorld = terrain.terrainData.size;

        //writes a grid of node G costs to a text file
        StreamWriter sw = new StreamWriter("test.txt");

        int h;
        for (i = 0, x = -50; i < 100 && x <= 50; i++,x++)
        {
            for (j=0,z=-50;j<100&&z<=50;j++,z++)
            {

               
                grid[i, j] = new Node( new Vector3(x, Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z)), z));
               
                
                    
                //Debug.Log("" + grid[i, j
                
            }
            
            sw.WriteLine();
            //Debug.Log("\n");
        }
        
       sw.Close();
        
        
	}
	
	// Update is called once per frame
 
}
