using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Graph : MonoBehaviour
{
    // public Dictionary<Node, List<(Node, Edge)>> nodeList = new Dictionary<Node, List<(Node, Edge)>>();
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
