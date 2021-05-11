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
            // TODO надо ли перезаписывать приоритет или сравнивать с текущим
            // priorityQueue[element] = priority;
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
            float h = (node.transform.position - finish.transform.position).magnitude; // sqrmagnitude не подходит

            // цена пути от старта
            float g = info[node].Item2;

            return h + g;
        }

    }
    

    public static void MaxFlow(Graph graph, Node start, Node finish)
    {
        EdgeManager edgeManager = GameObject.Find("Managers").GetComponent<EdgeManager>();
        List<Edge> temporaryEdges = new List<Edge>();
        float sumFlow = 0;
        // Форда-Фалкерсона 

        // 1 обнуляємо усі потоки
        foreach (Dictionary<Node, Edge> node in graph.nodeList.Values)
        {
            foreach (Edge edge in node.Values)
            {
                edge.flow = 0;
                edge.FlowColor();
            }
        }

        Graph resudalGraph = ResudalGraph(graph); // проверь

        // 2

        LinkedList<Node> way = AStar(resudalGraph, start, finish);

        int stop = 0;
        while (way != null)
        {
            stop += 1;
            if (stop >= 100)
            {
                Debug.Log("Стоп кран");
                break;
            }
            
            // через знайдений коротший шлях пускаємо потік

            // знайдемо найменшу 
            float cMin = Mathf.Infinity;
            for (LinkedListNode<Node> node = way.First; node.Next != null; node = node.Next)
            {
                float c = resudalGraph.nodeList[node.Value][node.Next.Value].ResidualFlow;
                cMin = Mathf.Min(cMin, c);
            }

            // збільшуємо поток на шляху
            for (LinkedListNode<Node> node = way.First; node.Next != null; node = node.Next)
            {

                // resudalGraph.nodeList[node.Value][node.Next.Value].flow += cMin;
                // TODO добавить обратное ребро!
                // graph.GetEdge(node.Value, node.Next.Value).flow += cMin;
                // graph.GetEdge(node.Next.Value, node.Value).flow -= cMin;

                GetEdge(node.Value, node.Next.Value).flow += cMin;
                GetEdge(node.Next.Value, node.Value).flow -= cMin;
                
            }

            resudalGraph = ResudalGraph(resudalGraph);

            way = AStar(resudalGraph, start, finish);
        }

        // перенос инфы из остаточного графа в основной
        foreach (KeyValuePair<Node, Dictionary<Node, Edge>> node in resudalGraph.nodeList)
        {
            foreach (KeyValuePair<Node, Edge> subnode in node.Value)
            {
                Edge resudalEdge = resudalGraph.nodeList[node.Key][subnode.Key];
                if (resudalEdge.capacity != 0)
                {
                    graph.nodeList[node.Key][subnode.Key].flow = resudalEdge.flow;
                }
            }
        }

        foreach (Edge e in temporaryEdges)
        {
            GameObject.Destroy(e.gameObject);
        }
        temporaryEdges.Clear();

        // обновление интерфейса для все ребер
        foreach (KeyValuePair<Node, Dictionary < Node, Edge >> node in graph.nodeList)
        {
            foreach (Edge edge in node.Value.Values)
            {
                edge.SetFlowText();
                edge.FlowColor();
            }
        }
        
        // оказалось не костыль, а по определению
        foreach (Edge fin in graph.nodeList[start].Values)
        {
            sumFlow += fin.flow;
        }
        Debug.Log($"Максимальний потік = {sumFlow}");

        Edge GetEdge(Node nodeA, Node nodeB)
        {
            Edge edge;
            if (resudalGraph.nodeList[nodeA].ContainsKey(nodeB))
            {
                edge = resudalGraph.nodeList[nodeA][nodeB];
            }
            else
            {
                edge = edgeManager.CreateEdge(nodeA, nodeB, resudalGraph, 0);
                temporaryEdges.Add(edge);
            }
            return edge;
        }


        Graph ResudalGraph(Graph initialGraph)
        {
            // по идее он не создает новые узлы а ссылается на оригинальный граф
            // так что и привязки к геймобджектам не надо

            Graph resGraph = ScriptableObject.CreateInstance("Graph") as Graph;



            foreach (KeyValuePair<Node, Dictionary<Node, Edge>> node in initialGraph.nodeList)
            {
                foreach (KeyValuePair<Node, Edge> subNode in node.Value)
                {
                    if (subNode.Value.ResidualFlow > 0)
                    {
                        resGraph.AddNode(node.Key);
                        resGraph.AddNode(subNode.Key);
                        resGraph.AddEdge(node.Key, subNode.Key, subNode.Value);
                    }
                    /*
                     * оно ведь и не добавтися
                    else if(subNode.Value.ResidualFlow == 0)
                    {
                        resGraph.RemoveEdge(node.Key, subNode.Key); // ?????????????
                    }
                    */
                }
            }
            if (resGraph.nodeList.Count > 0)
            {
                return resGraph;
            }
            else
            {
                return null;
            }

        }

    }

}
