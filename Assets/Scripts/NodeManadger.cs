using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManadger : MonoBehaviour
{

    [SerializeField] private GameObject sourseNodePrefab, normalNodePrefab, sinkNodePrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            CreateNode(sourseNodePrefab);
        }
        else if (Input.GetKeyDown("f"))
        {
            CreateNode(sinkNodePrefab);
        }
        else if (Input.GetKeyDown("n"))
        {
            CreateNode(normalNodePrefab);
        }
    }

    private void CreateNode(GameObject pref) // создает префаб узла, но не в граф. Добавление его в граф происходит в инициализаторе узла
    {
        Vector3 v = Input.mousePosition;
        v.z = 10f;
        v = Camera.main.ScreenToWorldPoint(v);
        Instantiate(pref, v, Quaternion.identity);
    }
}
