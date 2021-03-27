using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public enum GraphType
    {
        main,
        residual,
        support
    }
    // public GraphType graphType;

    // public List<(Node, List<(Node, Edge)>)> graphList;
    public List<Node> nodeList; // список узлов, из которых потом сложится граф целиком
    public List<Edge> edgeList; // список ребер
    private float[,,] mainGraph; // узлы из, узлы в, текущий-макс потоки
    private float[,] residualGraph, supportGraph;


    public int NodesAmount
    {
        get
        {
            return nodeList.Count;
        }
    }

    void Awake()
    {
        nodeList = new List<Node>();
        edgeList = new List<Edge>();
    }

    
    public int AddNode(Node newNode)
    {
        if (nodeList.Contains(newNode)) // если уже добавляли
        {
            return 0;
        }
        else
        {
            nodeList.Add(newNode);  // пустой элемент списка обозначает узел без соседей

            return nodeList.Count; // не назвать это костылем, но возвращает айди узла. кста не знаю насколько это будет работать если узлы удалять

        }
    }


    public void AddEdge(Edge edge)
    {
        if (!edgeList.Contains(edge)) // если еще не добавляли
        {
            edgeList.Add(edge);  // пустой элемент списка обозначает узел без соседей
        }
    }

    
    public void SetMatrix(GraphType graphType)
    {
        
        switch (graphType)
        {
            case GraphType.main:
                mainGraph = new float[nodeList.Count, nodeList.Count, 2];
                foreach (Edge edge in edgeList)
                {
                    mainGraph[edge.NodesID.Item1, edge.NodesID.Item2, 0] = 0; //edge.Flow;
                    mainGraph[edge.NodesID.Item1, edge.NodesID.Item2, 1] = edge.MaxFlow;
                }

                break;

            case GraphType.residual:
                residualGraph = new float[nodeList.Count, nodeList.Count];
                for (int i = 0; i < nodeList.Count; i++)
                {
                    for (int j = 0; j < nodeList.Count; j++)
                    {
                        residualGraph[i, j] = mainGraph[i, j, 1] - mainGraph[i, j, 0];
                        residualGraph[i, j] += mainGraph[j, i, 0]; // вот тут осторожно
                    }
                }
                break;

            case GraphType.support:


                break;

            default:
                break;
        }

    }


    private void MindBlowing()
    {
        List<LinkedList<int>> acceptableWays = new List<LinkedList<int>>();
        int startID = 0;
        int finishID = 0;
        foreach (Node node in nodeList)
        {
            if (node.nodeType == Node.NodeType.sourse)
            {
                startID = node.ID;
            }
            else if (node.nodeType == Node.NodeType.sink)
            {
                finishID = node.ID;
            }
        }

        LinkedList<int> way = new LinkedList<int>();
        way.AddLast(startID);


        for (int i = 0; i < residualGraph.GetUpperBound(1); i++)
        {
            if (residualGraph[startID, i] != 0)
            {

            }
        }

    }

    private void DLS(int startID, int finishID, int limit)
    {

    }



    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            trick();
        }
    }

    private void trick()
    {
        // craete process info
        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = @"C:\Users\maksb\AppData\Local\Microsoft\WindowsApps\python.exe"; // на счет интерпретатора сомневаюсь

        // script and arguments
        var script = @"C:\Disk\Univer\4 course\olefir\unity proj\Graph\DinicAlgorithm.py";
        // var C = "dfdfd";
        var source = 0;
        var sink = 5;

        psi.Arguments = $"\"{script}\"\"{source}\"\"{sink}\"";

        // process conf
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        // execue
        var errors = "";
        var results = "";

        using(var process = Process.Start(psi))
        {
            errors = process.StandardError.ReadToEnd();
            results = process.StandardOutput.ReadToEnd();

        }

        UnityEngine.Debug.Log(results);
    }

    /*
    private void trick2()
    {
        // create engine
        var engine = Python.CreateEngine();

        // prov scr and arg
        var script = @"C:\Disk\Univer\4 course\olefir\unity proj\Graph\DinicAlgorithm.py";
        var source = engine.CreateScriptSourceFromFile(script);

        var argv = new List<string>();
        argv.Add("");
        argv.Add("arg1");
        argv.Add("arg2");

        // engine.GetSysModule().SetVariable("argv", argv);
    }
    */

}
