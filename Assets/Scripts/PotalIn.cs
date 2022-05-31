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

            col.transform.position = Taget.position;
            col.transform.rotation = Taget.rotation;


        }
    }
}
