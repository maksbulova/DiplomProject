using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManadger : MonoBehaviour
{
    public GameObject nodePrefab;
    private GameObject nodeParent;

    private void Start()
    {
        nodeParent = GameObject.Find("Nodes");
    }

    public void CreateNode(Graph graph)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject node = Instantiate(nodePrefab, pos, Quaternion.identity);
        node.transform.SetParent(nodeParent.transform);

        graph.AddNode(node.GetComponent<Node>());
    }
}
