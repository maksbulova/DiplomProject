using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Edge : MonoBehaviour
{
    private int flow;
    public int maxFlow;
    private Node nodeA, nodeB;
    private Graph globalGraph;
    private GameObject manager;
    private EdgeManager edgeManager;
    private LineRenderer line;


    public int Flow
    {
        get
        {
            return flow;
        }
        set
        {
            if ((maxFlow - flow) >= value)
            {
                flow += value;
            }
            else
            {
                flow = maxFlow;
            }
        }
    }

    public int ResidualFlow
    {
        get
        {
            return maxFlow - flow;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        globalGraph = GameObject.Find("Graph").GetComponent<Graph>();
        manager = GameObject.Find("Managers");
        edgeManager = manager.GetComponent<EdgeManager>();

        line = gameObject.GetComponent<LineRenderer>();

        Initialize();
    }


    private void Initialize()
    {
        (Node, Node) nodes = edgeManager.SetNodes();
        nodeA = nodes.Item1;
        nodeB = nodes.Item2;

        globalGraph.AddEdge(nodeA, nodeB, this);

        line.positionCount = 2; // это объявление вынесено из дроу на случай когда придется с движением узла перерисовывать линию
        DrawLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    Vector3 pos;
    private void DrawLine()
    {
        pos = (nodeA.gameObject.transform.position + nodeB.gameObject.transform.position) / 2f;

        gameObject.transform.position = pos;
        // требует заранее заданого числа точек, это вынесено в инициализатор для оптимизации
        line.SetPosition(0, nodeA.transform.position);
        line.SetPosition(1, nodeB.transform.position);
    }


    public void SetMaxFlow()
    {
        maxFlow = int.Parse(GameObject.Find("Canvas/InputField/Text").GetComponent<Text>().text);
    }
}
