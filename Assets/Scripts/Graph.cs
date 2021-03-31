using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Dictionary<Node, List<(Node, Edge)>> nodeList = new Dictionary<Node, List<(Node, Edge)>>();

    public void AddNode(Node node)
    {
        if (!nodeList.ContainsKey(node))
        {
            nodeList.Add(node, new List<(Node, Edge)>());
        }
    }

    public void AddEdge(Node from, Node to, Edge edge)
    {
        // from.AddEdge(edge);
        if (nodeList.ContainsKey(from) && !nodeList[from].Contains((to, edge)))
        {
            nodeList[from].Add((to, edge));
        }
        
    }

}
