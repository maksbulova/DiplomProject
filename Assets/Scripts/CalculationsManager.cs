﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationsManager : MonoBehaviour
{


    public void ShowWay(Graph graph, Node start, Node finish)
    {
        LinkedList<Node> way = Analysis.AStar(graph, start, finish);

        if (way == null)
        {
            Debug.Log($"З вузла {start.name} до вузла {finish.name} не має шляху");

        }
        else
        {
            Debug.Log($"Найшвидший шлях з вузла {start.name} до вузла {finish.name}, це:");
            foreach (Node node in way)
            {
                Debug.Log(node.name);
            }

        }
    }

}