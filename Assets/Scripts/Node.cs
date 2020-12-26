using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{

    private Graph globalGraph;
    private GameObject manager;
    private EdgeManager edgeManager;

    private int id;
    public enum NodeType     // задается префабом
    {
        sourse,
        sink, 
        normal
    }
    public NodeType nodeType;


    void Start()
    {
        globalGraph = GameObject.Find("Graph").GetComponent<Graph>();
        manager = GameObject.Find("Managers");
        edgeManager = manager.GetComponent<EdgeManager>();

        SetNode();
    }

    private void SetNode()
    {
        //  сздается префаб узла, потом уже существующий узел добавляется в граф
        id = globalGraph.AddNode(this); 

        string name = id.ToString();
        switch (nodeType)
        {
            case NodeType.sourse:
                name += " (s)";
                break;
            case NodeType.sink:
                name += " (t)";
                break;
            case NodeType.normal:
                break;
            default:
                break;
        }

        gameObject.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = name;
    }

    void Update()
    {
    }


    private void OnMouseDown()
    {

    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            edgeManager.RecieveNode(this);
        }

    }


    private Vector3 pos;
    private void OnMouseDrag()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }
}
