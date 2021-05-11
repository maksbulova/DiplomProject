using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteAlways]
public class Node : MonoBehaviour
{
    public Graph manualGraph;
    public List<Edge> edgeList = new List<Edge>();
    

    public void AddEdge(Edge edge)
    {
        if (!edgeList.Contains(edge))
        {
            edgeList.Add(edge);
        }
    }

    private void Start()
    {
        ManualInit();
    }

    public void Initialize(Graph graph)
    {

        graph.AddNode(this);
        manualGraph = graph;
    }

    public void ManualInit()
    {
        Initialize(manualGraph);
    }

    private void OnDestroy()
    {
        Debug.Log("destroy");
        DeleteNode();
    }

    public void DeleteNode()
    {
        foreach (Edge edge in edgeList)
        {
            // edge.DeleteEdge();
            if (edge.nodeA == this)
            {
                edge.nodeA = null;
            }
            else if (edge.nodeB == this)
            {
                edge.nodeB = null;
            }
        }

        manualGraph.RemoveNode(this);
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
