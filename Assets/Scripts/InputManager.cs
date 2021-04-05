using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public enum Mode { idle, build, analize }
    public static Mode mode = Mode.idle;


    private GameObject managers;

    private Graph graph;

    public Color normalModeColor, buildModeColor, analizeModeColor;
    private Image panel;
    // private Button buildButton, analizeButton;

    private void Start()
    {
        managers = GameObject.Find("Managers");

        graph = GameObject.Find("MainGraph").GetComponent<Graph>();

        // buildButton = GameObject.Find("Canvas/Panel/BuildModeButton").GetComponent<Button>();
        // analizeButton = GameObject.Find("Canvas/Panel/AnalizeModeButton").GetComponent<Button>();

        panel = GameObject.Find("Canvas/Panel").GetComponent<Image>();

    }


    private void Update()
    {

        // левый клик по пустой позиции создает узел
        if (Input.GetMouseButtonDown(0) && ValidMousePosition())
        {
            managers.GetComponent<NodeManadger>().CreateNode(graph);
        }
        // правый клик по двум узлам создает ребро
        else if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray: Camera.main.ScreenPointToRay(Input.mousePosition), hitInfo: out RaycastHit hit, maxDistance: 100))
            {
                Node node = hit.collider.gameObject.GetComponent<Node>();
                if (node != null)
                {
                    // записали узел
                    RecieveNode(node, out (Node, Node) nodePaar);
                    if (nodePaar != (null, null))
                    {
                        managers.GetComponent<EdgeManager>().CreateEdge(nodePaar, graph);
                    }
                }
            }
        }


        switch (mode)
        {
            case Mode.idle:
                break;

            case Mode.build:


                break;

            case Mode.analize:
                break;

            default:
                break;
        }



    }

    private Node nodeA, nodeB;
    public void RecieveNode(Node node, out (Node, Node) nodePaar) // получив два узла делает между ними ребро
    {
        nodePaar = (null, null);
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

                nodePaar = (nodeA, nodeB);

                nodeA = null;
                nodeB = null;

            }
        }
    }



    private bool ValidMousePosition()
    {
        return !Physics.Raycast(ray: Camera.main.ScreenPointToRay(Input.mousePosition), hitInfo: out RaycastHit hit, maxDistance: 100);
    }

    public void NormalButton()
    {
        mode = Mode.idle;
        panel.color = normalModeColor;
    }
    public void BuildButton()
    {
        mode = Mode.build;
        panel.color = buildModeColor;
    }

    public void AnalizeButton()
    {
        mode = Mode.analize;
        panel.color = analizeModeColor;
    }


}
