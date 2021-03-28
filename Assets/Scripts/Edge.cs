using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Edge : MonoBehaviour
{
    public Node nodeA, nodeB;
    private float flow, capacity;
    private float weight;

    private LineRenderer line;



    // private GameObject manager;
    // private EdgeManager edgeManager;


    public float ResidualFlow
    {
        get { return capacity - flow; }
    }

    void Start()
    {

        // manager = GameObject.Find("Managers");
        // edgeManager = manager.GetComponent<EdgeManager>();

        line = gameObject.GetComponent<LineRenderer>();

    }


    private void Initialize(Graph graph, Node nodeA, Node nodeB, float cap)
    {
        this.nodeA = nodeA;
        this.nodeB = nodeB;
        capacity = cap;
        flow = 0;

        graph.AddEdge(nodeA, nodeB, this);

        DrawLine();
    }


    public void DrawLine()
    {

        gameObject.transform.position = (nodeA.gameObject.transform.position + nodeB.gameObject.transform.position) / 2f;
        line.positionCount = 2;
        line.SetPosition(0, nodeA.transform.position);
        line.SetPosition(1, nodeB.transform.position);
    }


}
