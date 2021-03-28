using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManadger : MonoBehaviour
{
    public GameObject nodePrefab;


    public void CreateNode(Graph graph)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // pos.z = 0;
        graph.AddNode(Instantiate(nodePrefab, pos, Quaternion.identity).GetComponent<Node>());
    }
}
