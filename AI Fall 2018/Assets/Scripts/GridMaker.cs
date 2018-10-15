using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Node{
    int height;
    Vector3 position;

    public int gCost; // cost to travel to node, based on height
    public int hCost; // based on grid distance
    public int fCost; //gCost + hCost

    public int gridX; // the x position in the grid
    public int gridY; // the y position in the grid

    public bool isWalkable; 

    public Node parent;

    public Node(int h,Vector3 pos, int gridXpos, int gridYpos)
    {
        height = h;
        position = pos;
        gridX = gridXpos;
        gridY = gridYpos;
        if(h == 0 || h<=7||h>=14)
        {
            isWalkable = false;
        }
        else
        {
            isWalkable = true;
        }

        gCost = height; // g cost is set to the height for now. It should really be the difference in height between the starting node and this node
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

    public int Fcost
    {
        get{ return fCost; }
    }
}

public class GridMaker : MonoBehaviour {
    public Terrain terrain;
    public Node[,] grid = new Node[100, 100];
    public LayerMask unWalkableMask;

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

                //new Vector3(x,10, z)
                h = (int)terrain.SampleHeight(new Vector3(x, 0, z)); 
                
                // check for unwalkable layer
                if (Physics.CheckSphere(new Vector3(x, h, z), radius,unWalkableMask))
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


                grid[i, j] = new Node(h, new Vector3(x, h, z), i, j);
                sw.Write(grid[i, j].Height + " ");

                
                    
                //Debug.Log("" + grid[i, j
                
            }
            
            sw.WriteLine();
            //Debug.Log("\n");
        }
        
       sw.Close();
        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //get a node from a world point
    public Node GetNodeFromWorld(Vector3 worldPos)
    {
        int x;
        int y=0;
        Debug.Log("actual"+worldPos.x + " " + worldPos.y + " " + worldPos.z);
        for (x=0;x<100; x++)
        {
            for (y=0; y < 100; y++)
            {
                Debug.Log("node"+grid[x, y].Position.x + " " + grid[x, y].Position.y + " " + grid[x, y].Position.z);
                if(grid[x,y].Position.x == (int)worldPos.x && grid[x, y].Position.z == (int)worldPos.z)
                {
                    break;
                }
            }
        }
        return grid[x,y];
    }

    // Get the neighboring nodes that can be travelled to
    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                //skip this one
                if (x == 0 && y == 0)
                {
                    
                }
                else
                {
                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    // check if it is within the bounds of the grid
                    if (checkX >= 0 && checkX < gridSizeInWorld.x && checkY >= 0 && checkY < gridSizeInWorld.y)
                    {
                        //add it to the grid of neighbors
                        neighbors.Add(grid[checkX, checkY]);
                    }
                }
                
            }
        }

        return neighbors;
    }

    public List<Node> path;
    public void OnDrawGizmos()
    {
        //Draw Logic
        if (grid != null)
        {
            foreach(Node n in grid)
            {
                if (path != null)
                {
                    if(path.Contains(n)){
                        Gizmos.color = Color.gray;
                        Gizmos.DrawCube(n.Position, Vector3.one);
                    }
                }
            }
        }
    } 
}
