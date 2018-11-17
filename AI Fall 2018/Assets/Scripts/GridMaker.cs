using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Node {
    Vector3 position;

    public Color32 influenceColor;

    public int influenceOnNode;

    public Node(Vector3 pos)
    {
        position = pos;
        influenceColor = Color.white;
        influenceOnNode = 0;
    }
}

public class GridMaker : MonoBehaviour {
    public Terrain terrain;
    public Node[,] grid = new Node[100, 100];

    private Vector2 gridSizeInWorld;

	// Use this for initialization
	void Start () {
        Vector3 start = new Vector3(50, 10, -50);
        Vector3 end = terrain.terrainData.size;
        int i, j, x = 50, z = -50;
        Debug.LogFormat(""+Terrain.activeTerrain.SampleHeight(new Vector3(-47,0,-16)));

        gridSizeInWorld = terrain.terrainData.size;

        for (i = 0, x = -50; i < 100 && x <= 50; i++,x++)
        {
            for (j=0,z=-50;j<100&&z<=50;j++,z++)
            {
                grid[i, j] = new Node( new Vector3(x, 50, z));
            }
        }
	}
}