using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{

    private Graph mainGraph, residualGraph, levelGraph;

    // Start is called before the first frame update
    void Start()
    {
        mainGraph = GameObject.Find("Graph").GetComponent<Graph>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CalculateMaxFlow()
    {

        // 1. поток каждого ребра = 0
        foreach ((Node, List<(Node, Edge)>) fromNode in mainGraph.graphList)
        {
            foreach ((Node, Edge) toNode in fromNode.Item2)
            {
                toNode.Item2.Flow = 0;
            }
        }





        return 0;
    }


    private void CreateResidualGraph(ref Graph graph, out Graph residualGraph)
    {
        residualGraph = new Graph();
        
        residualGraph.graphList = new List<(Node, List<(Node, Edge)>)>();



        foreach ((Node, List<(Node, Edge)>) fromNode in graph.graphList)
        {
            foreach ((Node, Edge) toNode in fromNode.Item2)
            {
                if (toNode.Item2.ResidualFlow != 0)
                {
                    residualGraph.AddNode(fromNode.Item1);

                    residualGraph.AddEdge(fromNode.Item1, toNode.Item1, toNode.Item2.ResidualFlow);
                }
            }
        }
    }

}
