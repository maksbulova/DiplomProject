using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationsManager : MonoBehaviour
{
    private Graph graph;

    private void Start()
    {
        graph = GameObject.Find("MainGraph").GetComponent<Graph>();
    }


    Node nodeA, nodeB;
    public void RecieveNode()
    {
        Physics.Raycast(ray: Camera.main.ScreenPointToRay(Input.mousePosition), hitInfo: out RaycastHit hit, maxDistance: 100);
        Node node = hit.collider.gameObject.GetComponent<Node>();

        if (nodeA == null)
        {
            nodeA = node;
        }
        else
        {
            if (node == nodeA)
            {
                nodeA = null;
            }
            else
            {
                nodeB = node;

                CreateEdge(nodeA, nodeB, graph);

                nodeA = null;
                nodeB = null;

            }
        }

    }

    public LinkedList<Node> way = Analysis.AStar(graph, )
}
