using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Analysis
{
    public static LinkedList<Node> AStar(Graph graph, Node start, Node finish)
    {
        LinkedList<Node> way = new LinkedList<Node>();



        return null;
    }

    public static void MaxFlow(Graph graph, Node start, Node finish)
    {
        // Форда-Фалкерсона

        // 1 обнуляємо усі потоки
        foreach (List<(Node, Edge)> node in graph.nodeList.Values)
        {
            foreach ((Node, Edge) edge in node)
            {
                edge.Item2.flow = 0;
            }
        }

        // 2 
    }

    private static Graph ResudalGraph(Graph initialGraph)
    {
        Graph resudalGraph = new Graph();

        foreach (KeyValuePair<Node, List<(Node, Edge)>> node in resudalGraph.nodeList)
        {
            foreach ((Node, Edge) subNode in node.Value)
            {
                if (subNode.Item2.ResidualFlow > 0)
                {
                    resudalGraph.AddNode(node.Key);
                    resudalGraph.AddEdge(node.Key, subNode.Item1, subNode.Item2);
                }
            }
        }
        if (resudalGraph.nodeList.Count > 0)
        {
            return resudalGraph;
        }
        else
        {
            return null;
        }
        
    }
}
