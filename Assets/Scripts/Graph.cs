using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[ExecuteAlways]
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
    public void RemoveNode(Node node) // не тестил
    {
        if (nodeList.ContainsKey(node))
        {
            foreach (KeyValuePair<Node, Dictionary<Node, Edge>> nodes in nodeList)
            {
                nodes.Value.Remove(node);
            }

            nodeList.Remove(node);

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

    public void RemoveEdge(Node from, Node to) // не тестил
    {
        if (nodeList.ContainsKey(from) && nodeList[from].ContainsKey(to))
        {
            nodeList[from].Remove(to);
        }
    }

}
