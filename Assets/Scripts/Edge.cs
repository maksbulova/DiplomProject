using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class Edge : MonoBehaviour
{
    public Node nodeA, nodeB;
    public float flow, capacity;
    public float weight;
    private Text capacityInputField;
    private Text flowText;

    private LineRenderer line;

    public float ResidualFlow
    {
        get { return capacity - flow; }
    }



    public void Initialize(Graph graph, Node from, Node to, float cap=1)
    {
        nodeA = from;
        nodeB = to;
        capacity = cap;
        flow = 0;
        CalculateWeight();

        nodeA.AddEdge(this);
        nodeB.AddEdge(this);

        graph.AddEdge(from, to, this);

        line = gameObject.GetComponent<LineRenderer>();
        // line.colorGradient.colorKeys = new GradientColorKey[1];

        capacityInputField = transform.Find("Canvas/InputField/Text").GetComponent<Text>();
        flowText = transform.Find("Canvas/Panel/FlowText").GetComponent<Text>();

        FlowColor();
        DrawEdge();
    }

    public void CalculateWeight()
    {
        weight = (nodeA.transform.position - nodeB.transform.position).magnitude;
    }

    public void SetFlowText()
    {
        flowText.text = flow.ToString();
    }

    public void DrawEdge()
    {
        gameObject.transform.position = (nodeA.gameObject.transform.position + nodeB.gameObject.transform.position) / 2f;
        Vector3[] points = new Vector3[2] { nodeA.transform.position, nodeB.transform.position };
        line.SetPositions(points);
    }


    // виклик кнопкою
    public void SetCapacity()
    {
        if (capacityInputField.text.Length == 0)
        {
            capacity = 1;
        }
        else
        {
            capacity = float.Parse(capacityInputField.text, CultureInfo.InvariantCulture.NumberFormat);
        }

        capacityInputField.text = capacity.ToString(); // TODO не работает, вообще исправь чтоб запись через точку работала а не только запятую
    }
    
    public void FlowColor()
    {
        float load = flow / capacity;
        Color color = Color.Lerp(Color.white, Color.red, load);

        line.startColor = color;
        line.endColor = color;
    }

}
