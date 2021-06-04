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
    public static LinkedList<Node> AStar(Graph graph, Node start, Node finish, bool loaded)
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
                        float weight;
                        // данные о соседе: откуда мы в него пришли и сколько это стоило (вес или BPR)
                        if (loaded)
                        {
                            weight = BPR(graph.nodeList[current][neighbour.Key]);
                        }
                        else
                        {
                            weight = (graph.nodeList[current][neighbour.Key].distance /
                                graph.nodeList[current][neighbour.Key].speedLimit);
                        }
                        info.Add(neighbour.Key, (current, info[current].Item2 + weight));
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
        graph.ReGraph();

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

        LinkedList<Node> way = AStar(resudalGraph, start, finish, false);

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

            way = AStar(resudalGraph, start, finish, false);
        }

        // перенос инфы из остаточного графа в основной
        /*
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
        */

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

        // повертає ребро між вузлами, якзо такого нема - створює і повертає
        Edge GetEdge(Node nodeA, Node nodeB)
        {
            Edge edge;
            if (resudalGraph.nodeList[nodeA].ContainsKey(nodeB))
            {
                edge = resudalGraph.nodeList[nodeA][nodeB];
            }
            else
            {
                edge = edgeManager.CreateEdge(nodeA, nodeB, resudalGraph, false);
                edge.capacity = 0;
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


    private static float mu = 0.15f;
    private static float n = 4;

    // временные затраты от загружености дороги
    public static float BPR(Edge edge)
    {
        float tFreeFlow = edge.distance / edge.speedLimit;

        return tFreeFlow * (1 + mu * Mathf.Pow(edge.flow / edge.capacity, n));
    }


    private static bool WaySetBalansed(List<LinkedList<Node>> waysSet, float accuracy, Graph graph)
    {
        if (waysSet.Count == 1)
        {
            return true;
        }

        // для кожного шляху знайдемо ціну
        float[] waysPrices = new float[waysSet.Count];
        for (int i = 0; i < waysSet.Count; i++)
        {
            waysPrices[i] = WayPrice(waysSet[i], true, graph);
        }
        // ціна найдовшого та найшвидшого шляхів
        float maxWayPrice = waysPrices.Max();
        float minWayPrice = waysPrices.Min();

        return (maxWayPrice - minWayPrice) < accuracy ? true : false;
    }

    // розподіл потоку по шляхам
    public static void SmallBalance(List<LinkedList<Node>> waySet, float odFLow,  Graph graph, float accuracy=50)
    {
        int errorKey = 0;

        // на першому кроці рішення тривіальне
        if (waySet.Count == 1)
        {
            // весь потік закачуємо у прямий шлях
            ModifyWay(waySet[0], odFLow);
        }
        else
        {
            float flowDifference = Mathf.Infinity;
            while (flowDifference > accuracy && errorKey < 100)
            {
                errorKey++;

                // для кожного шляху знайдемо ціну
                float[] waysPrices = new float[waySet.Count];
                for (int i = 0; i < waySet.Count; i++)
                {
                    waysPrices[i] = WayPrice(waySet[i], true, graph);
                }
                // номер найдовшого та найшвидшого шляхів
                float maxWayPrice = waysPrices.Max();
                float minWayPrice = waysPrices.Min();

                int maxWayIndex = waysPrices.ToList().IndexOf(maxWayPrice);
                int minxWayIndex = waysPrices.ToList().IndexOf(minWayPrice);

                // з найдовшого шляху перерозподіляємо частку потоку на найкоротший шлях
                // TODO вирішити яку частку переносити
                float maxWayFlow = graph.nodeList[waySet[maxWayIndex].First.Value][waySet[maxWayIndex].First.Next.Value].flow;
                float minWayFlow = graph.nodeList[waySet[minxWayIndex].First.Value][waySet[minxWayIndex].First.Next.Value].flow;
                flowDifference = maxWayFlow - minWayFlow;

                if (flowDifference > accuracy) // TODO перепиши срочно, реализовано ужасно
                {
                    ModifyWay(waySet[maxWayIndex], -flowDifference/2);
                    ModifyWay(waySet[minxWayIndex], flowDifference/2);
                }
            }
            // Debug.Log($"small balance: {errorKey}");
        }

        // змінити потік по шляху
        void ModifyWay(LinkedList<Node> way, float flow)
        {
            // збільшуємо поток на шляху
            for (LinkedListNode<Node> node = way.First; node.Next != null; node = node.Next)
            {
                graph.nodeList[node.Value][node.Next.Value].flow += flow;
            }
        }
    }

    private static float WayPrice(LinkedList<Node> way, bool loaded, Graph graph)
    {
        float sumPrice = 0;
        for (LinkedListNode<Node> node = way.First; node.Next != null; node = node.Next)
        {
            if (loaded)
            {
                sumPrice += BPR(graph.nodeList[node.Value][node.Next.Value]);
            }
            else
            {
                sumPrice += graph.nodeList[node.Value][node.Next.Value].distance;
            }
            
        }

        return sumPrice;
    }



    public static void BigBalance((Node, Node, float)[] ODpaars, Graph graph)
    {
        graph.ReGraph();

        foreach (Dictionary<Node, Edge> node in graph.nodeList.Values)
        {
            foreach (Edge edge in node.Values)
            {
                edge.flow = 0;
                edge.FlowColor();
            }
        }

        // колво пучков = колву пар, каждый пучек это динамично изменяемый набор путей
        List<LinkedList<Node>>[] waySets = new List<LinkedList<Node>>[ODpaars.Length];

        // стартові пучки по пустій мережі 
        for (int i = 0; i < waySets.Length; i++)
        {
            LinkedList<Node> newWay = AStar(graph, ODpaars[i].Item1, ODpaars[i].Item2, false);
            waySets[i] = new List<LinkedList<Node>>();
            waySets[i].Add(newWay);
            SmallBalance(waySets[i], ODpaars[i].Item3, graph);
        }

        int newWayErrorKey = 0;
        int BallancedErrorKey = 0;

        // алгоритм працює доки можна додати новий шлях хочаб до однієй пари
        bool newWayExistsKey = true;
        while (newWayExistsKey && newWayErrorKey < 100)
        {
            newWayErrorKey++;

            newWayExistsKey = false;
            for (int i = 0; i < waySets.Length; i++)
            {
                LinkedList<Node> newWay = AStar(graph, ODpaars[i].Item1, ODpaars[i].Item2, true);
                if (!WayExists(waySets[i], newWay))
                {
                    newWayExistsKey = true;
                    waySets[i].Add(newWay);
                }
            }

            // коли усі пучки збалансовані - розширюємо їх
            bool allSetsBalancedKey = false;
            while (!allSetsBalancedKey && BallancedErrorKey < 100)
            {
                BallancedErrorKey++;
                allSetsBalancedKey = true;

                // перебираємо усі пучки
                for (int j = 0; j < waySets.Length; j++)
                {
                    if (!WaySetBalansed(waySets[j], 50, graph))
                    {
                        // якщо хочаб один незбалансований - ітерація повториться по усіх
                        allSetsBalancedKey = false;
                        SmallBalance(waySets[j], ODpaars[j].Item3, graph);
                    }
                }
            }
        }

        // Debug.Log($"new way key: {newWayErrorKey}");
        // Debug.Log($"ballanced key: {BallancedErrorKey}");

        float freeTime = 0;
        float congestionTime = 0;

        foreach (List<LinkedList<Node>> set in waySets)
        {
            foreach (LinkedList<Node> way in set)
            {
                freeTime += WayPrice(way, false, graph);
                congestionTime += WayPrice(way, true, graph);
            }
        }
        float congestionLvl = (congestionTime - freeTime) / freeTime;

        Debug.Log($"Congestion level: {congestionLvl}%");

        foreach (KeyValuePair<Node, Dictionary<Node, Edge>> node in graph.nodeList)
        {
            foreach (Edge edge in node.Value.Values)
            {
                edge.SetFlowText();
                edge.FlowColor();
            }
        }


        bool WayExists(List<LinkedList<Node>> waySet, LinkedList<Node> way)
        {
            // waySets[i].Contains(newWay) не працював, довелося замінити цією фією

            foreach (LinkedList<Node> w in waySet)
            {
                if (Enumerable.SequenceEqual(w, way))
                    return true;
            }
            return false;
        }
    }


    public static float[,] GenerateOD(District[] districts, Graph graph, float accuracy = 50f)
    {
        float[,] odMatrix = new float[districts.Length, districts.Length];

        // функція прибалюваності шляху між районами
        float fNodes(Node from, Node to, float beta=0.065f)
        {
            return Mathf.Exp(-beta * WayPrice(AStar(graph, from, to, false), false, graph));
        }

        float fDistricts(District from, District to, float beta=0.065f)
        {
            float averageF = 0;
            int steps = 0;

            foreach (Node fromNode in from.nodes)
            {
                foreach (Node toNode in to.nodes)
                {
                    averageF += fNodes(fromNode, toNode, beta);
                    steps++;
                }
            }
            averageF /= steps;
            return averageF;
        }

        // заповнили матрицю прибавливістю шляхів між районами
        for (int i = 0; i < districts.Length; i++)
        {
            for (int j = 0; j < districts.Length; j++)
            {
                odMatrix[i, j] = fDistricts(districts[i], districts[j]);
            }
        }

        int infStop = 0;
        float changeFactor;
        do
        {
            infStop++;

            // критерій зупинки, цей коофи повинні збігатися до 1
            changeFactor = 0;

            for (int i = 0; i < districts.Length; i++)
            {
                float rowSum = 0;
                for (int j = 0; j < districts.Length; j++)
                {
                    rowSum += odMatrix[i, j];
                }

                for (int j = 0; j < districts.Length; j++)
                {
                    float koof = districts[i].population / rowSum;
                    odMatrix[i, j] *= koof;

                    changeFactor += koof;
                }
            }

            for (int j = 0; j < districts.Length; j++)
            {
                float colSum = 0;
                for (int i = 0; i < districts.Length; i++)
                {
                    colSum += odMatrix[i, j];
                }

                for (int i = 0; i < districts.Length; i++)
                {
                    float koof = districts[i].workers / colSum;
                    odMatrix[i, j] *= koof;

                    changeFactor += koof;
                }
            }

            changeFactor /= districts.Length;

        } while (Mathf.Abs(changeFactor - 1) > accuracy && infStop < 100);


        return odMatrix;
    }

}
