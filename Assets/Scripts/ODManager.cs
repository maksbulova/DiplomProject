using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ODManager : MonoBehaviour
{
    public Graph graph;
    public GameObject linePrefab;
    public District[] districts;

    
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
                    line.widthMultiplier = od[i, j];

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
}
