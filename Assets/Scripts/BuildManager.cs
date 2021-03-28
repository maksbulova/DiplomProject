using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    private GameObject managers;

    private Graph graph;

    private bool buildMode = false;

    public Color normalModeColor, buildModeColor;
    private Image panel;
    private Button buildButton;

    private void Start()
    {
        managers = GameObject.Find("Managers");

        graph = GameObject.Find("MainGraph").GetComponent<Graph>();

        buildButton = GameObject.Find("Canvas/Panel/BuildModeButton").GetComponent<Button>();
        panel = GameObject.Find("Canvas/Panel").GetComponent<Image>();

    }


    private void Update()
    {
        if (buildMode)
        {
            if (Input.GetMouseButtonDown(0) && ValidMousePosition())
            {
                managers.GetComponent<NodeManadger>().CreateNode(graph);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray: Camera.main.ScreenPointToRay(Input.mousePosition), hitInfo: out RaycastHit hit, maxDistance: 100))
                {
                    Node node = hit.collider.gameObject.GetComponent<Node>();
                    if (node != null)
                    {
                        managers.GetComponent<EdgeManager>().RecieveNode(node, graph);
                    } 
                }
                
            }

        }

    }

    private bool ValidMousePosition()
    {
        return !Physics.Raycast(ray: Camera.main.ScreenPointToRay(Input.mousePosition), hitInfo: out RaycastHit hit, maxDistance: 100);
    }

    public void BuildButton()
    {
        buildMode = !buildMode;

        if (buildMode)
        {
            panel.color = buildModeColor;
        }
        else
        {
            panel.color = normalModeColor;
        }
    }

}
