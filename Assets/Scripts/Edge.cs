using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

[ExecuteAlways]
public class Edge : MonoBehaviour
{
    public Graph manualGraph;

    public Node nodeA, nodeB;
    
    public float flow;
    [Delayed]
    public float capacity;
    public float weight;
    public bool twoWay = true;

    private Text capacityInputField;
    private Text flowText;

    // запривать!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public Edge oppositeLine;

    private LineRenderer line;

    public float ResidualFlow
    {
        get { return capacity - flow; }
    }

    private void OnValidate()
    {
        if (nodeA != null && nodeB != null)
        {
            Initialize(manualGraph, nodeA, nodeB);
        }

    }

    [ContextMenu("Hard initialization")]
    public void HardInit()
    {
        if (nodeA != null && nodeB != null)
        {
            DeleteEdge();
            Initialize(manualGraph, nodeA, nodeB);
        }
    }

    public void SoftInit()
    {

    }

    public void Initialize(Graph graph, Node from, Node to)
    {
        manualGraph = graph;

        nodeA = from;
        nodeB = to;


        flow = 0;
        CalculateWeight();

        nodeA.AddEdge(this);
        nodeB.AddEdge(this);

        graph.AddEdge(nodeA, nodeB, this);

        line = gameObject.GetComponent<LineRenderer>();

        capacityInputField = transform.Find("Canvas/InputField/Text").GetComponent<Text>();
        flowText = transform.Find("Canvas/Panel/FlowText").GetComponent<Text>();

        FlowColor();
        DrawEdge();

        
        if (twoWay)
        {
            // если встречки еще не уществует
            if (oppositeLine == null)
            {
                oppositeLine = this;
                // создает копию себя
                oppositeLine = Instantiate(gameObject, gameObject.transform.parent).GetComponent<Edge>();
                oppositeLine.oppositeLine = this;

                oppositeLine.Initialize(manualGraph, to, from);
            }
            else
            {
                if (oppositeLine.capacity != capacity)
                {
                    oppositeLine.capacity = this.capacity;
                }

                DrawEdge();
                oppositeLine.DrawEdge();
            }
        }
        else
        {
            if (oppositeLine != null)
            {
                GameObject tempOpLine = oppositeLine.gameObject;
                oppositeLine.DeleteEdge();

                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(tempOpLine);
                };
            }
        }
        

    }


    [ContextMenu("Recalculate graph")]
    public void ReGraph()
    {
        if (nodeA && nodeB)
        {
            manualGraph.RemoveEdge(nodeA, nodeB);
            manualGraph.AddEdge(nodeA, nodeB, this);
        }
    }

    [ContextMenu("Change direction")]
    private void ChangeDir()
    {
        Node temp = nodeA;
        nodeA = nodeB;
        nodeB = temp;

        ReGraph();
        DrawEdge();
    }


    private void OnDestroy()
    {
        DeleteEdge();
    }

    public void DeleteEdge()
    {
        nodeA?.edgeList.Remove(this);
        nodeB?.edgeList.Remove(this);
        if(nodeA && nodeB)
            manualGraph.RemoveEdge(nodeA, nodeB);

        if (oppositeLine != null)
            oppositeLine.oppositeLine = null;
    }

    public void CalculateWeight()
    {
        weight = (nodeA.transform.position - nodeB.transform.position).magnitude;
    }

    public void SetFlowText()
    {
        flowText.text = flow.ToString();
    }

    [ContextMenu("ReDraw")]
    public void DrawEdge()
    {
        line.widthMultiplier = Mathf.Clamp(capacity, 20, 100);
        Vector3 posA, posB;

        if (twoWay)
        {
            posA = nodeA.gameObject.transform.position;
            posB = nodeB.gameObject.transform.position;

            Vector3 dir = posB - posA;
            dir = new Vector3(dir.y, dir.x).normalized;

            posA += dir * line.widthMultiplier;
            posB += dir * line.widthMultiplier;
        }
        else
        {
            posA = nodeA.gameObject.transform.position;
            posB = nodeB.gameObject.transform.position;
        }

        gameObject.transform.position = (posA + posB) / 2f;
        Vector3[] points = new Vector3[2] { posA, posB };
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
