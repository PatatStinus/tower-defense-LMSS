using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] Transform selectedObject = null;

    const string placedString = "Placed";
    const int placedInt = 12;

    RaycastHit hit;

    [SerializeField] Material green;
    [SerializeField] Material red;

    [SerializeField] LayerMask selectableLayer;
    [SerializeField] LayerMask terrainLayer;

    [SerializeField] bool placedObject = false;
    [SerializeField] bool canPlace;

    [SerializeField] GameObject Trigger;

    private void Start()
    {
        Trigger.SetActive(true);
        canPlace = true;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (selectedObject != null && !placedObject)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, terrainLayer))
            {
                selectedObject.position = new Vector3(hit.point.x, selectedObject.position.y, hit.point.z);
            }
            if (Input.GetMouseButton(0) && canPlace == true)
            {
                //Instantiate(prefab, transform.position, transform.rotation);
                Trigger.SetActive(false);
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

        if (Input.GetMouseButtonDown(0) && selectedObject != null && canPlace)
        {
            selectedObject.gameObject.tag = placedString;
            selectedObject.gameObject.layer = placedInt;
            selectedObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Placed")
        {
            Debug.Log("Triggered");
            canPlace = false;
            Trigger.GetComponent<Renderer>().material = red;
        }
        else
        {
            canPlace = true;
            Trigger.GetComponent<Renderer>().material = green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Placed")
        {
            canPlace = true;
            Trigger.GetComponent<Renderer>().material = green;
        }
    }
}
