using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowDistribution : MonoBehaviour
{
    public Node from1, to1, from2, to2;
    public float amount1, amount2;
    public Graph graph;

    public void Distribute()
    {
        (Node, Node, float)[] paar = new (Node, Node, float)[] { (from1, to1, amount1), (from2, to2, amount2) };
        Analysis.BigBalance(paar, graph);
    }
}
