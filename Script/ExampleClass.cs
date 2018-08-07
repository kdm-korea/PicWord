using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    public Transform target;
    public float speed = 3;
    GameObject go;

    public void MakeObj()
    {
        GameObject prefab = Resources.Load("butterfly-2") as GameObject;
        go = MonoBehaviour.Instantiate(prefab) as GameObject;
        go.name = "flyAnimat";
        go.transform.parent = transform;
        go.transform.position = transform.position;
        go.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
            go.transform.position = Vector3.MoveTowards(go.transform.position, target.position, step);

        if(go.transform.position == target.position)
        {
            Destroy(go);
            transform.GetComponent<ExampleClass>().enabled = false;
        }
    }
}