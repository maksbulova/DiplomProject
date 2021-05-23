using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowDistribution : MonoBehaviour
{
    public Node from;
    public Node to;
    public float amount;
    public Graph graph;

    public void Distribute()
    {
        (Node, Node, float)[] paar = new (Node, Node, float)[] { (from, to, amount) };
        Analysis.BigBalance(paar, graph);
    }
}
