using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Edge : MonoBehaviour
{
    public Node nodeA, nodeB;
    private float flow, capacity;
    private float weight;
    private Text capacityInputField;

    private LineRenderer line;



    // private GameObject manager;
    // private EdgeManager edgeManager;


    public float ResidualFlow
    {
        get { return capacity - flow; }
    }

    void Start()
    {

        

    }


    public void Initialize(Graph graph, Node from, Node to, float cap=1)
    {
        nodeA = from;
        nodeB = to;
        capacity = cap;
        flow = 0;
        CalculateWeight();

        nodeA.AddEdge(this);
        nodeB.AddEdge(this);

        

        line = gameObject.GetComponent<LineRenderer>();
        capacityInputField = transform.Find("Canvas/InputField/Text").GetComponent<Text>();

        // graph.AddEdge(nodeA, nodeB, this); это в менеджере!!

        DrawEdge();
    }

    public void CalculateWeight()
    {
        weight = (nodeA.transform.position - nodeB.transform.position).magnitude;
    }

    public void DrawEdge()
    {
        gameObject.transform.position = (nodeA.gameObject.transform.position + nodeB.gameObject.transform.position) / 2f;
        Vector3[] points = new Vector3[2] { nodeA.transform.position, nodeB.transform.position };
        line.SetPositions(points);
    }


    public void SetCapacity()
    {
        capacity = float.Parse(capacityInputField.text);
    }

}
