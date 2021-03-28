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
        ValidMousePosition();

        if (buildMode && Input.GetMouseButtonDown(0))
        {
            managers.GetComponent<NodeManadger>().CreateNode(graph);
        }

    }

    private void ValidMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(ray);
        Physics.Raycast(ray, out RaycastHit hit);

        Debug.Log(hit.collider);
    }


    public void BuildButton()
    {
        buildMode = !buildMode;

        if (buildButton)
        {
            panel.color = buildModeColor;
        }
        else
        {
            panel.color = normalModeColor;
        }
    }

}
