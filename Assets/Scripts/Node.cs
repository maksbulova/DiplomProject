using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{

    private Graph globalGraph;
    private GameObject manager;
    private EdgeManager edgeManager;

    public int ID { get; private set; }

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
        ID = globalGraph.AddNode(this); 

        string name = ID.ToString();
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


    private void OnMouseOver()  // правый клик по узлу строит ребра
    {
        if (Input.GetMouseButtonDown(1))
        {
            edgeManager.RecieveNode(this);
        }

    }


    private Vector2 pos;        
    private void OnMouseDrag()  // драг узла
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //pos.z = 0;
        transform.position = pos;
    }
}
