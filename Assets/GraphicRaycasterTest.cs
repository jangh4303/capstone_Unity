using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GraphicRaycasterTest : MonoBehaviour
{

    private GraphicRaycaster graphicRaycaster;
   


    // Start is called before the first frame update
    private void Awake()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();

    }

    // Update is called once per frame
    private void Update() { 
    
        PointerEventData ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        graphicRaycaster.Raycast(ped, results);

        if(results.Count<=0)
        {
            return;
        }

        Debug.Log("<color=blue>HIt : </color>" + results[0].gameObject.name);


    }
}
