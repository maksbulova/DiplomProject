using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeManager : MonoBehaviour
{
    public GameObject EdgeSetupPanel;

    private GameObject edgeParent;

    public GameObject edgePrefab;

    private void Start()
    {
        edgeParent = GameObject.Find("Edges");
    }


    public void CreateEdge(Node A, Node B, Graph graph)
    {
        Edge edge = Instantiate(edgePrefab, Vector3.zero, Quaternion.identity).GetComponent<Edge>();
        edge.Initialize(graph, A, B);

        edge.transform.SetParent(edgeParent.transform);

        // graph.AddEdge(A, B, edge);
    }

    public void CreateEdge((Node, Node) nodePaar, Graph graph)
    {
        CreateEdge(nodePaar.Item1, nodePaar.Item2, graph);
    }



}
