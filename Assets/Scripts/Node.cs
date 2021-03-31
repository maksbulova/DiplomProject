using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    private List<Edge> edgeList = new List<Edge>();


    public void AddEdge(Edge edge)
    {
        if (!edgeList.Contains(edge))
        {
            edgeList.Add(edge);

        }
    }


    public void Initialize(Graph graph)
    {
        graph.AddNode(this);
    }


    
    private void OnMouseDrag()  // перемещение узла
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;

        foreach (Edge edge in edgeList)
        {
            edge.DrawEdge();
            edge.CalculateWeight();
        }
    }
    
}
