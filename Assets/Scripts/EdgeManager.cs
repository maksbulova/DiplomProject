using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeManager : MonoBehaviour
{
    public GameObject EdgeSetupPanel;

    private GameObject edgeParent;

    private Node nodeA, nodeB;
    public GameObject edgePrefab;

    private void Start()
    {
        edgeParent = GameObject.Find("Edges");
    }

    public void RecieveNode(Node node, Graph graph) // получив два узла делает между ними ребро
    {
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

    public void CreateEdge(Node A, Node B, Graph graph)
    {
        Edge edge = Instantiate(edgePrefab, Vector3.zero, Quaternion.identity).GetComponent<Edge>();
        edge.Initialize(graph, A, B);

        edge.transform.SetParent(edgeParent.transform);

        // graph.AddEdge(A, B, edge);
    }



}
