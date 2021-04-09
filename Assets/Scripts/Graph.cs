using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "Graph", menuName = "ScriptableObjects/Graph", order = 1)]
public class Graph : ScriptableObject
{

    public Dictionary<Node, Dictionary<Node, Edge>> nodeList = new Dictionary<Node, Dictionary<Node, Edge>>();



    public void AddNode(Node node)
    {
        if (!nodeList.ContainsKey(node))
        {
            nodeList.Add(node, new Dictionary<Node, Edge>());
        }
    }

    public void AddEdge(Node from, Node to, Edge edge)
    {
        if (nodeList.ContainsKey(from) && !nodeList[from].ContainsValue(edge))
        {
            nodeList[from].Add(to, edge);

            from.AddEdge(edge);
            to.AddEdge(edge);

        }

    }

}
