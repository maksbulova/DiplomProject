using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ODManager : MonoBehaviour
{
    public Graph graph;
    public GameObject linePrefab;
    public District[] districts;

    [ContextMenu("Population")]
    public void CheckPopulation()
    {
        int population = 0; 
        int workers = 0; 

        foreach (District district in districts)
        {
            population += district.population;
            workers += district.workers;
        }

        Debug.Log($"Мешканців: {population}");
        Debug.Log($"Робочих місць: {workers}");
        Debug.Log($"Різниця: {population - workers}");
    }

    [ContextMenu("GenerateOD")]
    public void GenerateOD()
    {
        float[,] od = Analysis.GenerateOD(districts, graph);

        
        for (int i = 0; i < districts.Length; i++)
        {
            for (int j = 0; j < districts.Length; j++)
            {
                Debug.Log($"З {districts[i].name} в {districts[j].name}: {od[i, j]}");
            }
        }
    }

    public void VisualizeOD()
    {
        float[,] od = Analysis.GenerateOD(districts, graph);

        for (int i = 0; i < districts.Length; i++)
        {
            Vector3 posA = districtPosition(districts[i]);
            for (int j = 0; j < districts.Length; j++)
            {
                if (i != j)
                {
                    Vector3 posB = districtPosition(districts[j]);

                    LineRenderer line = Instantiate(linePrefab, districts[i].transform).GetComponent<LineRenderer>();
                    float t = Mathf.InverseLerp(10000, 50000, od[i, j]);
                    line.widthMultiplier = Mathf.Lerp(100, 800, t);

                    Vector3[] points = new Vector3[2] { posA, posB };
                    line.SetPositions(points);
                }
            }
        }

        Vector3 districtPosition(District district)
        {
            Vector3 pos = new Vector3();
            foreach (Node node in district.nodes)
            {
                pos += node.gameObject.transform.position;
            }
            pos /= district.nodes.Count;
            return pos;
        }
    }

    public void DistributeFlows()
    {
        float[,] odMatrix = Analysis.GenerateOD(districts, graph);

        List<(Node, Node, float)> ODpaars = new List<(Node, Node, float)>();

        for (int i = 0; i < districts.Length; i++)
        {
            for (int j = 0; j < districts.Length; j++)
            {
                if (i != j)
                {
                    int paarAmount = districts[i].nodes.Count * districts[j].nodes.Count;
                    float flow = odMatrix[i, j] / paarAmount;

                    foreach (Node from in districts[i].nodes)
                    {
                        foreach (Node to in districts[j].nodes)
                        {
                            ODpaars.Add((from, to, flow));
                        }
                    }
                }
            }
        }

        Analysis.BigBalance(ODpaars, graph);
    }
}
