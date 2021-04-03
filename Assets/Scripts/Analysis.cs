using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public struct PriorityQueue<T>
{
    private Dictionary<T, float> priorityQueue;


    public int Count
    {
        get { return priorityQueue.Count; }
    }

    public void Enqueue(T element, float priority)
    {
        if (priorityQueue.ContainsKey(element))
        {
            priorityQueue[element] = priority; // TODO надо ли перезаписывать приоритет или сравнивать с текущим
        }
        else
        {
            priorityQueue.Add(element, priority);
        }
    }


    public KeyValuePair<T, float> Dequeue(bool maxPriority)
    {
        KeyValuePair<T, float> priorityElement;
        if (maxPriority)
        {
            priorityElement = GetMax();
        }
        else
        {
            priorityElement = GetMin();
        }
        
        priorityQueue.Remove(priorityElement.Key);
        return priorityElement;
    }

    public KeyValuePair<T, float> Peek(bool maxPriority)
    {
        if (maxPriority)
        {
            return GetMax();
        }
        else
        {
            return GetMin();
        }
        
    }

    private KeyValuePair<T, float> GetMax()
    {
        return priorityQueue.Aggregate((m, n) => m.Value > n.Value ? m : n);   // TODO смысл копипаста понял, но не тестил
    }

    private KeyValuePair<T, float> GetMin()
    {
        return priorityQueue.Aggregate((m, n) => m.Value < n.Value ? m : n);
    }

}

public static class Analysis
{


    /*
    public static LinkedList<Node> AStar(Graph graph, Node start, Node finish)
    {
        LinkedList<Node> way = new LinkedList<Node>();

        // узел для рассмотрения и его оценка
        List<(Node, float)> open = new List<(Node, float)>() { (start, 0) };

        // рассмотренный узел и узел откуда мы пришли в рассмотренный
        List<(Node, Node)> closed = new List<(Node, Node)>();

        while (open.Count > 0)
        {
            foreach (var item in collection)
            {

            }
        }

        return null;
    }
    */

    public static LinkedList<Node> BFS(Graph graph, Node start, Node finish)
    {

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
