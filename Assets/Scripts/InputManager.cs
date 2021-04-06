﻿using System.Collections;
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

        // левый клик
        if (Input.GetMouseButtonDown(0))
        {
            switch (mode)
            {
                case Mode.idle:
                    break;

                case Mode.build:
                    // левый клик по пустой позиции создает узел
                    if (CheckMousePosition().collider == null)
                        managers.GetComponent<NodeManadger>().CreateNode(graph);
                    break;

                case Mode.analize:
                    break;

                default:
                    break;
            }
            
        }
        // правый клик по двум узлам создает ребро
        else if (Input.GetMouseButtonDown(1))
        {

            switch (mode)
            {
                case Mode.idle:
                    break;
                case Mode.build:
                    // выбираем два узла и строим ребро, сбрасываем выделение при клике по уже выбраном, либо вне узлов
                    RaycastHit hit = CheckMousePosition();

                    if (RecieveNode(hit.collider?.gameObject.GetComponent<Node>(), out (Node, Node) nodePaar))
                    {
                        managers.GetComponent<EdgeManager>().CreateEdge(nodePaar, graph);
                    }

                    break;
                case Mode.analize:
                    break;
                default:
                    break;
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


    // TODO выделить цветом выбраный узел
    private Node nodeA, nodeB;
    public bool RecieveNode(Node node, out (Node, Node) nodePaar) // получив два узла делает между ними ребро
    {
        nodePaar = (null, null);
        if (node == null)
        {
            nodeA = null;
            nodeB = null;
        }
        else if (nodeA == null)
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

                return true;
            }
        }

        return false;
    }

    private RaycastHit CheckMousePosition()
    {
        Physics.Raycast(ray: Camera.main.ScreenPointToRay(Input.mousePosition), hitInfo: out RaycastHit hit, maxDistance: 100);
        return hit;
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
