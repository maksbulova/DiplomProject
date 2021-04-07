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

        Node node = Instantiate(nodePrefab, pos, Quaternion.identity).GetComponent<Node>();
        node.gameObject.transform.SetParent(nodeParent.transform);

        node.Initialize(graph);

        node.name =  "Вузол " + graph.nodeList.Keys.Count.ToString();
    }
}
