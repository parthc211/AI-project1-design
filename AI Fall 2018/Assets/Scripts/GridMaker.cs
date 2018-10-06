using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Node{
    int height;
    Vector3 position;

    public Node(int h,Vector3 pos)
    {
        height = h;
        position = pos;
    }

    public int Height
    {
        get { return height; }
    }

    public Vector3 Position
    {
        get
        {
            return position;
        }
    }
}

public class GridMaker : MonoBehaviour {
    public Terrain terrain;
    Node[,] grid = new Node[100, 100];
    public LayerMask unWalkableMask;

	// Use this for initialization
	void Start () {
        Vector3 start = new Vector3(50, 10, -50);
        Vector3 end = terrain.terrainData.size;
        Debug.Log(start);
        Debug.Log(end);
        int i, j,x=50, z=-50;
        Debug.LogFormat("" + x + " " + z);
        int radius = 1;
        Debug.LogFormat(""+Terrain.activeTerrain.SampleHeight(new Vector3(-47,0,-16)));
        StreamWriter sw = new StreamWriter("test.txt");
        int h;
        for (i = 0, x = -50; i < 100 && x <= 50; i++,x++)
        {
            for (j=0,z=-50;j<100&&z<=50;j++,z++)
            {

                //new Vector3(x,10, z)
                int height = (int)terrain.SampleHeight(new Vector3(x, 0, z)); ;
                h = (height > 7) ? (height!=9)? 1 : 9 : 0;
                
                // check for unwalkable layer
                if (Physics.CheckSphere(new Vector3(x, height, z), radius,unWalkableMask))
                {
                    h = 0;
                }
                
                //check for making bridges walkable
                if (((x<=32&&x>=28)&&(z<=1 && z>=-4))) 
                {
                    Debug.Log(i + " " + j);
                    h = 9;
                }
                if (((x <= -8 && x >= -10) && (z <= -7 && z >= -15)))
                {
                    Debug.Log(i + " " + j);
                    h = 9;
                }
                h = (h > 0) ? 1 : 0;
                grid[i, j] = new Node(h, new Vector3(x, h, z));
                sw.Write(grid[i, j].Height + " ");

                
                    
                //Debug.Log("" + grid[i, j
                
            }
            
            sw.WriteLine();
            //Debug.Log("\n");
        }
        Debug.LogFormat("" + x + " " + z+" "+grid[96,34]);
        sw.Close();
        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDrawGizmos()
    {
        //Draw Logic
        if(grid!=null)
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Gizmos.color = (grid[i,j].Height != 0) ? Color.white : Color.red;
                Gizmos.DrawCube(grid[i,j].Position, new Vector3(1, 1, 1));
            }
        }
    }
}
