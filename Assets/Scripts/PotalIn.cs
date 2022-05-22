using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalIn : MonoBehaviour
{

    public Transform Taget;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(col.gameObject.name);
            col.transform.position = Taget.position;
            Debug.Log(col.transform.position);

        }
    }
}
