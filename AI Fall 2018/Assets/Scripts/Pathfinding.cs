using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    // the seeker is this object, the target is the position it seeks
    // ideally the target is a point we would be able to set with a mouse click
    public Transform seeker, target;

    GridMaker grid;

    void Awake()
    {
        grid = GetComponent<GridMaker>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        FindPath(seeker.position, target.position);
	}

    //Find the path between the seeker and target
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Debug.Log("Starting the serch");
        Node startNode = grid.GetNodeFromWorld(startPos);
        Node targetNode = grid.GetNodeFromWorld(targetPos);
       
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);


        //loop through the open set
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for(int i = 0; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            Debug.Log("start");

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                Debug.Log("end");
                return;
            }

            //loop through neighbors
            foreach(Node neighbor in grid.GetNeighbors(currentNode))
            {
                //if it is not walkable or is inside the closed set, skip this
                if(!neighbor.isWalkable || closedSet.Contains(neighbor))
                {
                    continue;
                }
                
                // finding the movement cost to neighbor
                // I was following the method in the unity tutorial, however our grid is implemented differently
                // I believe we want to change this to set the gCost to the height difference between the starting node and neighboring node
                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if(newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.fCost = neighbor.gCost + neighbor.hCost;
                    neighbor.parent = currentNode;

                    if(!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
    }

    // retrace the path
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        //path is backwards, reverse it
        path.Reverse();
        Debug.Log("1");

    }

    // get the distance between two nodes
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(distX > distY)
        {
            // I was following a tutorial that used these values but I don't know if they are correct for this project
            // We may want to change this section before submitting
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distX + 10 * (distY - distX);
        }
    }
}
