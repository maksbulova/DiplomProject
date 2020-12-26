using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public List<(Node, List<(Node, Edge)>)> graphList;

    public int NodesAmount
    {
        get
        {
            return graphList.Count;
        }
    }

    void Awake()
    {
        graphList = new List<(Node, List<(Node, Edge)>)>(); // пустой список
    }

    
    public int AddNode(Node node)
    {
        foreach ((Node, List<(Node, Edge)>) fromNode in graphList)
        {
            if (fromNode.Item1 == node)     // если попросили добавить уже существующий узел
            {
                return 0; // надо ли возвращать его номер 
            }
        }
        graphList.Add((node, new List<(Node, Edge)>()));  // пустой элемент списка обозначает узел без соседей

        return graphList.Count; // не назвать это костылем, но возвращает айди узла. кста не знаю насколько это будет работать если узлы удалять

    }



    public void AddEdge(Node fromNode, Node toNode, Edge edge)
    {
        foreach ((Node, List<(Node, Edge)>) element in graphList)   // перебрать список 
        {
            if (element.Item1 == fromNode)
            {
                element.Item2.Add((toNode, edge));
            }
        }
    }


    public void AddEdge(Node fromNode, Node toNode, int maxFlow)
    {
        Edge edge = new Edge();
        edge.maxFlow = maxFlow;

        foreach ((Node, List<(Node, Edge)>) element in graphList)   // перебрать список 
        {
            if (element.Item1 == fromNode)
            {
                element.Item2.Add((toNode, edge));
            }
        }
    }



}
