using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Node{
   
    Vector3 position;
    List<string> units = null; 
    
    public Node(Vector3 pos)
    {
        position = pos;
        units = new List<string>();
    }
    
    public Vector3 Position
    {
        get { return position; }
    }

    public List<string> GetUnits()
    {
        return units;         
    }
    public void AddUnit(string unit)
    {
        units.Add(unit);
    }
}

public class GridMaker{
    public Terrain terrain;
    public Node[,] grid = new Node[100, 100];
  

    Vector2 gridSizeInWorld;

	// Use this for initialization
	public GridMaker (Terrain terrain_in) {
        terrain = terrain_in;
        Vector3 start = new Vector3(50, 10, -50);
        Vector3 end = terrain.terrainData.size;
        int i, j, x = 50, z = -50;
      
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

   

    public Node[,] Grid
    {
        get { return grid; }
    }
}
