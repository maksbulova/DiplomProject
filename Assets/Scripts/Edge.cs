using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Edge : MonoBehaviour
{
    private float flow;
    public float MaxFlow { get; private set; }
    public Node nodeA, nodeB;
    private Graph globalGraph;
    private GameObject manager;
    private EdgeManager edgeManager;
    private LineRenderer line;

    public (int, int) NodesID
    {
        get
        {
            return (nodeA.ID, nodeB.ID);
        }
    }

    public float Flow
    {
        get { return flow; }
        set
        {
            if ((MaxFlow - flow) >= value)
            {
                flow += value;
            }
            else
            {
                flow = MaxFlow;
            }
        }
    }

    public float ResidualFlow
    {
        get { return MaxFlow - flow; }
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

        globalGraph.AddEdge(this);

        DrawLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void DrawLine()
    {
        Vector3 pos = (nodeA.gameObject.transform.position + nodeB.gameObject.transform.position) / 2f;

        gameObject.transform.position = pos;
        line.positionCount = 2;
        line.SetPosition(0, nodeA.transform.position);
        line.SetPosition(1, nodeB.transform.position);
    }


    public void SetMaxFlow()
    {
        MaxFlow = int.Parse(GameObject.Find("Canvas/InputField/Text").GetComponent<Text>().text);
    }
}
