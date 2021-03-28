using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    private List<Edge> edgeList = new List<Edge>();


    public void AddEdge(Edge edge)
    {
        edgeList.Add(edge);
    }


    
    private void OnMouseDrag()  // перемещение узла
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;

        foreach (Edge edge in edgeList)
        {
            edge.DrawLine();
        }
    }
    
}
