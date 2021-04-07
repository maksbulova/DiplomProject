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

    public bool Contains(T item)
    {
        return priorityQueue.ContainsKey(item);
    }

    public void Enqueue(T element, float priority)
    {
        if (priorityQueue == null)
        {
            priorityQueue = new Dictionary<T, float>();
        }

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

    public static LinkedList<Node> AStar(Graph graph, Node start, Node finish)
    {
        

        // узел для рассмотрения и его оценка
        PriorityQueue<Node> frontier = new PriorityQueue<Node>();

        // откуда мы пришли в узел, и цена пути в него из старта
        Dictionary<Node, (Node, float)> info = new Dictionary<Node, (Node, float)>();

        // рассмотренные узлы
        // Dictionary<Node, Node> closed = new Dictionary<Node, Node>();
        List<Node> closed = new List<Node>();

        // начали рассматривать
        frontier.Enqueue(start, 0);
        info.Add(start, (null, 0));
        while (frontier.Count > 0)
        {
            // перешли в узел с найменьшей оценкой
            Node current = frontier.Dequeue(false).Key;

            if (current == finish)
            {
                // возвращаем путь
                goto way;
            }
            else
            {
                // перебираем соседей текущего узла
                foreach (KeyValuePair<Node, Edge> neighbour in graph.nodeList[current])
                {
                    // оцениваем соседей и добавляем в очередь с приоритетом (фронтир)
                    if (!frontier.Contains(neighbour.Key) && !closed.Contains(neighbour.Key))
                    {
                        // данные о соседе: откуда мы в него пришли и сколько это стоило (вес)
                        info.Add(neighbour.Key, (current, info[current].Item2 + graph.nodeList[current][neighbour.Key].weight));
                        frontier.Enqueue(neighbour.Key, f(neighbour.Key));
                    }
                }
                closed.Add(current);
            }
        }

        return null;

    way:
        LinkedList<Node> way = new LinkedList<Node>();
        Node step = finish;
        way.AddLast(step);
        do
        {
            step = info[step].Item1;
            way.AddFirst(step);

        } while (step != start);
        return way;

        // эвристика узла - чем меньше тем приоритетнее
        float f(Node node)
        {
            // евклидовр расстояние до финиша
            float h = (node.transform.position - finish.transform.position).magnitude; // TODO точной длины ведь не надо?

            // цена пути от старта
            float g = info[node].Item2;

            return h + g;
        }

    }
    

    public static void MaxFlow(Graph graph, Node start, Node finish)
    {
        // Форда-Фалкерсона 

        // 1 обнуляємо усі потоки
        foreach (Dictionary<Node, Edge> node in graph.nodeList.Values)
        {
            foreach (Edge edge in node.Values)
            {
                edge.flow = 0;
            }
        }

        // 2 
    }

    private static Graph ResudalGraph(Graph initialGraph)
    {
        Graph resudalGraph = new Graph();


        foreach (KeyValuePair<Node, Dictionary<Node, Edge>> node in resudalGraph.nodeList)
        {
            foreach (KeyValuePair<Node, Edge> subNode in node.Value)
            {
                if (subNode.Value.ResidualFlow > 0)
                {
                    resudalGraph.AddNode(node.Key);
                    resudalGraph.AddEdge(node.Key, subNode.Key, subNode.Value);
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
