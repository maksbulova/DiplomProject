using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{

    private Graph graph;

    // Start is called before the first frame update
    void Start()
    {
        graph = GameObject.Find("Graph").GetComponent<Graph>();
    }


    public int CalculateMaxFlow()
    {
        // увесь поток = 0
        graph.SetMatrix(Graph.GraphType.main);

        graph.SetMatrix(Graph.GraphType.residual);

        graph.SetMatrix(Graph.GraphType.support);

        return 0;
    }


    

}
