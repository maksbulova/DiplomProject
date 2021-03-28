using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    private List<Edge> edgeList;


    // private GameObject manager;
    // private EdgeManager edgeManager;



    void Start()
    {
        // manager = GameObject.Find("Managers");
        // edgeManager = manager.GetComponent<EdgeManager>();

    }

    /*
    вынес добавлеение в граф в ноде менеджер
    public void Initialize(Graph graph)
    {
        graph.AddNode(this); 
    }
    */


    public void AddEdge(Edge edge)
    {
        edgeList.Add(edge);
    }



    private void OnMouseOver()  // правый клик по узлу строит ребра
    {
        if (Input.GetMouseButtonDown(1))
        {
            // edgeManager.RecieveNode(this);
        }

    }


    /*
    private void OnMouseDrag()  // драг узла
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;

        foreach (Edge edge in edgeList)
        {
            edge.DrawLine();
        }
    }
    */
}
