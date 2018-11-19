using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Node {

    Vector3 position;
    List<string> units = null;
    int strength = 0;
    //public Color nodeColor;
    public int winningTeamColor; //Color of winning influence value

    public List<Node> neighbors = new List<Node>();

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

    public void CalculateStrength()
    {
        //value for how much influence a team has on a point. Values greater than 0 will have red influence, values less than 0 will have green influence 
        

        foreach(string s in units)
        {
            if(s.Equals("white"))
            {
                strength += 1;
            }
            else if(s.Equals("blue"))
            {
                strength += 2;
            }
            else if(s.Equals("yellow"))
            {
                strength += 3;
            }
            else if(s.Equals("black"))
            {
                strength += 4;
            }
            else if (s.Equals("gwhite"))
            {
                strength -= 1;
                Debug.Log("gwhite");
            }
            else if (s.Equals("gblue"))
            {
                strength -= 2;
                Debug.Log("gblue");
            }
            else if (s.Equals("gyellow"))
            {
                strength -= 3;
                Debug.Log("gyellow");
            }
            else if (s.Equals("gblack"))
            {
                strength -= 4;
                Debug.Log("gblack");
            }

            
        }
        

        if (strength > 4)
        {
            strength = 4;
        }
        if(strength < -4)
        {
            strength = -4;
        }
    }

    public void SetStrength(int _strength)
    {
        strength = _strength;
    }

    public int GetStrength()
    {
        return strength;
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
        
        for (i = 0, x = -50; i < 100 && x <= 50; i++,x++)
        {
            for (j=0,z=-50;j<100&&z<=50;j++,z++)
            {
                grid[i, j] = new Node( new Vector3(x, Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z)), z));
            }
            
            sw.WriteLine();
        }

        GetNeighbors();

        sw.Close();
	}

    public void GetNeighbors()
    {
        int x = 100;
        int y = 100;

        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                if (i % x == 0)    //Column on the far left
                {
                    grid[i, j].neighbors.Add(grid[i + 1, j]);
                    if (j % y == 0)    //Bottom Left Corner
                        grid[i, j].neighbors.Add(grid[i, j + 1]);
                    else if (j % y == y - 1)    //Top Left Corner
                        grid[i, j].neighbors.Add(grid[i, j - 1]);
                    else
                    {
                        grid[i, j].neighbors.Add(grid[i, j - 1]);
                        grid[i, j].neighbors.Add(grid[i, j + 1]);
                    }

                }
                else if (i % x == x - 1)    //Column on the far right
                {
                    grid[i, j].neighbors.Add(grid[i - 1, j]);
                    if (j % y == 0)    //Bottom Left Corner
                        grid[i, j].neighbors.Add(grid[i, j + 1]);
                    else if (j % y == y - 1)
                        grid[i, j].neighbors.Add(grid[i, j - 1]);
                    else
                    {
                        grid[i, j].neighbors.Add(grid[i, j - 1]);
                        grid[i, j].neighbors.Add(grid[i, j + 1]);
                    }
                }
                else if (j % y == 0)  //Bottom Row (sides excluded)
                {
                    grid[i, j].neighbors.Add(grid[i, j + 1]);
                    grid[i, j].neighbors.Add(grid[i + 1, j]);
                    grid[i, j].neighbors.Add(grid[i - 1, j]);
                }
                else if (j % y == y - 1)    //Top Row (sides excluded)
                {
                    grid[i, j].neighbors.Add(grid[i, j - 1]);
                    grid[i, j].neighbors.Add(grid[i + 1, j]);
                    grid[i, j].neighbors.Add(grid[i - 1, j]);
                }
                else    //Middle only
                {
                    grid[i, j].neighbors.Add(grid[i, j - 1]);
                    grid[i, j].neighbors.Add(grid[i, j + 1]);
                    grid[i, j].neighbors.Add(grid[i + 1, j]);
                    grid[i, j].neighbors.Add(grid[i - 1, j]);
                }
            }
        }
    }
    
    public Node[,] Grid
    {
        get { return grid; }
    }
}
