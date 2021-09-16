using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    public Transform selectedObject = null;

    RaycastHit hit;

    public LayerMask selectableLayer;
    public LayerMask terrainLayer;

    public bool placedObject = false;
    public bool canPlace;

    //public GameObject prefab;

    //public GameObject Trigger;

    private void Start()
    {
        //TriggerG.SetActive(true);
        //Trigger.SetActive(false);

        canPlace = true;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.Log($"selected object:{selectedObject}");
        if (selectedObject != null && !placedObject)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, terrainLayer))
            {
                //Debug.Log("Moving");
                selectedObject.position = new Vector3(hit.point.x, selectedObject.position.y, hit.point.z);
            }
            if (Input.GetMouseButton(0) && canPlace == true)
            {
                //Instantiate(prefab, transform.position, transform.rotation);
                Destroy(gameObject);
                placedObject = true;
            }
        }
        else
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, selectableLayer))
            {
                Debug.Log("Selected object: " + hit.transform.name);
                selectedObject = hit.transform;
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedObject != null)
        {
            selectedObject = null;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Placed" || other.gameObject.tag == "NPC")
    //    {
    //        canPlace = false;
    //        Trigger.SetActive(true);
    //        //TriggerG.SetActive(false);
    //    }
    //    else
    //    {
    //        canPlace = true;
    //        Trigger.SetActive(false);
    //        //TriggerG.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Placed" || other.gameObject.tag == "NPC")
    //    {
    //        canPlace = true;
    //        Trigger.SetActive(false);
    //        //TriggerG.SetActive(true);
    //    }
    //}
}
