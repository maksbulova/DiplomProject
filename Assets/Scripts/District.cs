using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District : MonoBehaviour
{
    public int population, workers;

    public List<Node> nodes;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.gameObject.transform.parent.GetComponent<Node>();
        if (!nodes.Contains(node))
        {
            nodes.Add(node);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Node node = collision.gameObject.transform.parent.GetComponent<Node>();
        if (nodes.Contains(node))
        {
            nodes.Remove(node);
        }
    }
}
