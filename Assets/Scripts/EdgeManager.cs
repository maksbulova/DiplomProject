using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeManager : MonoBehaviour
{

    private Node nodeA, nodeB;
    public GameObject edgePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveNode(Node node) // получив два узла делает между ними ребро
    {
        if (nodeA == null)
        {
            nodeA = node;
        }
        else
        {
            if (node == nodeA)
            {
                nodeA = null;
            }
            else
            {
                nodeB = node;

                CreateEdge(nodeA, nodeB);
            }
        }
    }

    public void CreateEdge(Node A, Node B)
    {
        // создает префаб ребра, но добавить его в граф уже в инициализации самого ребра

        Instantiate(edgePrefab, Vector3.zero, Quaternion.identity); 
    }

    Node A, B;
    public (Node, Node) SetNodes()
    {
        A = nodeA;
        B = nodeB;
        nodeA = null;   // вот и пошли костыли
        nodeB = null;

        return (A, B); 

    }

}
