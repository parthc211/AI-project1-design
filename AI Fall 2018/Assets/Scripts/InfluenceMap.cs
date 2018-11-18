using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfluenceMap : MonoBehaviour {

    public Spawner spawner;
    public List<Node> initalNodes = new List<Node>();

    public GameObject TextureImage;

    //public Texture2D influenceMapTexture;

	// Use this for initialization
	void Start () {
        TextureImage.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.G))
        {
            TextureImage.SetActive(true);

            initalNodes = GetInitialNodesWithInfluences();

            Debug.Log("Initial Values " + initalNodes.Count);

            CreateInfluenceMap();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            TextureImage.SetActive(false);
        }
	}

    public List<Node> GetInitialNodesWithInfluences()
    {
        Node[,] gridNodes = spawner.GetGrid();
        
        List<Node> nodes = new List<Node>();

        for(int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if(gridNodes[i, j].GetStrength() > 0)
                {
                    nodes.Add(gridNodes[i, j]);
                }
            }
        }

        return nodes;
    }

    public void CreateInfluenceMap()
    {
        Texture2D influenceMapTexture = new Texture2D(100, 100, TextureFormat.ARGB32, false);

        Node[,] gridNodes = spawner.GetGrid();

        for (int i = 0; i < gridNodes.GetLength(0); i++)
        {
            for(int j = 0; j < gridNodes.GetLength(1); j++)
            {
                

                Color pixelColor;
                /*
                pixelColor.r = gridNodes[i, j].GetStrength() * 2;
                pixelColor.g = 0;
                pixelColor.b = 0;
                */
                pixelColor.r = gridNodes[i, j].nodeColor.r * gridNodes[i, j].GetStrength();
                pixelColor.g = gridNodes[i,j].nodeColor.g * gridNodes[i, j].GetStrength();
                pixelColor.b = gridNodes[i, j].nodeColor.b * gridNodes[i, j].GetStrength();
                pixelColor.a = 255;
                

                influenceMapTexture.SetPixel(i, j, pixelColor);
            }
        }

        influenceMapTexture.Apply();

        TextureImage.GetComponent<Image>().sprite = Sprite.Create(influenceMapTexture, new Rect(0, 0, 100, 100), new Vector3(0.5f, 0.5f));
    }
}
