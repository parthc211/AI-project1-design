using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfluenceMap : MonoBehaviour {

    public Spawner spawner;
    public List<Node> initalNodes = new List<Node>();
    public List<int> xPosNode = new List<int>();
    public List<int> zPosNode = new List<int>();
    public GameObject TextureImage;

    private const int STRENGTH_MAX = 4;

    //public Texture2D influenceMapTexture;

	// Use this for initialization
	void Start () {
        TextureImage.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.G))
        {
            for(int i = 0; i < STRENGTH_MAX; i++)
            {
                CalculateLinearDrop();
            }

            TextureImage.SetActive(true);

            CreateInfluenceMap();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            TextureImage.SetActive(false);
        }
	}

    public void CalculateLinearDrop()
    {
        int change = 0;
        List<Node> iNodes = GetNodesWithInfluences();

        foreach (Node n in iNodes)
        {
            foreach (Node neighbor in n.neighbors)
            {
                
                if (n.GetStrength() > neighbor.GetStrength())
                {
                    neighbor.SetStrength(n.GetStrength() - 1);
                    change++;
                    Debug.Log(change);
                }
                else if(n.GetStrength() < neighbor.GetStrength())
                {
                    //this one doesn't work
                    neighbor.SetStrength(n.GetStrength() + 1);
                    change--;
                    Debug.Log(change);
                }
                

            }
        }

    }

    public List<Node> GetNodesWithInfluences()
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

                
                /*
                Color pixelColor;
                
                pixelColor.r = gridNodes[i, j].GetStrength() * 2;
                pixelColor.g = 0;
                pixelColor.b = 0;
                
                pixelColor.r = gridNodes[i, j].nodeColor.r * gridNodes[i, j].GetStrength();
                pixelColor.g = gridNodes[i,j].nodeColor.g * gridNodes[i, j].GetStrength();
                pixelColor.b = gridNodes[i, j].nodeColor.b * gridNodes[i, j].GetStrength();
                pixelColor.a = 255;
                */
          
    
                Color pixelColor = new Color(0, 0, 0);

                if (gridNodes[i, j].GetStrength() == 0)
                {

                    pixelColor.r = 200;
                    pixelColor.g = 200;
                    pixelColor.b = 200;
                    pixelColor.a = 255;
                }
                else if (gridNodes[i, j].GetStrength() == 1)
                {

                    pixelColor.r = 255;
                    pixelColor.g = 0;
                    pixelColor.b = 0;
                    pixelColor.a = 255;
                }
                else if (gridNodes[i, j].GetStrength() == 2)
                {
                    pixelColor.r = 200;
                    pixelColor.g = 0;
                    pixelColor.b = 0;
                    pixelColor.a = 255;
                }
                else if (gridNodes[i, j].GetStrength() == 3)
                {
                    pixelColor.r = 150;
                    pixelColor.g = 0;
                    pixelColor.b = 0;
                    pixelColor.a = 255;
                }
                else if (gridNodes[i, j].GetStrength() == 4)
                {
                    pixelColor.r = 100;
                    pixelColor.g = 0;
                    pixelColor.b = 0;
                    pixelColor.a = 255;
                }
                else if (gridNodes[i, j].GetStrength() == -1)
                {

                    pixelColor.r = 0;
                    pixelColor.g = 255;
                    pixelColor.b = 0;
                    pixelColor.a = 255;
                }
                else if (gridNodes[i, j].GetStrength() == -2)
                {
                    pixelColor.r = 0;
                    pixelColor.g = 200;
                    pixelColor.b = 0;
                    pixelColor.a = 255;
                }
                else if (gridNodes[i, j].GetStrength() == -3)
                {
                    pixelColor.r = 0;
                    pixelColor.g = 150;
                    pixelColor.b = 0;
                    pixelColor.a = 255;
                }
                else if (gridNodes[i, j].GetStrength() == -4)
                {
                    pixelColor.r = 0;
                    pixelColor.g = 100;
                    pixelColor.b = 0;
                    pixelColor.a = 255;
                }

                influenceMapTexture.SetPixel(i, j, pixelColor);
            }
        }

        influenceMapTexture.Apply();

        TextureImage.GetComponent<Image>().sprite = Sprite.Create(influenceMapTexture, new Rect(0, 0, 100, 100), new Vector3(0.5f, 0.5f));
    }
}
