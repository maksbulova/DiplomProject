using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeManager : MonoBehaviour
{

    private Node nodeA, nodeB;
    public GameObject edgePrefab;


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
        edge.nodeA = A;
        edge.nodeB = B;

        edge.DrawLine();

        graph.AddEdge(A, B, edge);
    }

    public (Node, Node) SetNodes()
    {
        Node A = nodeA;
        Node B = nodeB;
        nodeA = null;   // вот и пошли костыли
        nodeB = null;

        return (A, B); 

    }

}
