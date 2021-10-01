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
        //Taking the mouse position relative to the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Checking if a object is selected and that it hasnt been placed
        if (selectedObject != null && !placedObject)
        {
            //Checking if terrain layer is selected with raycast
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, terrainLayer))
            {
                //Setting the towers position as the mouse position
                selectedObject.position = new Vector3(hit.point.x, selectedObject.position.y, hit.point.z);
            }
            //Checking if the object should be able to be placed when clicking mouse
            if (Input.GetMouseButton(0) && canPlace == true)
            {
                //When building placed it deactivates trigger and changes its layer + tag to placed
                Trigger.SetActive(false);
                placedObject = true;
                selectedObject.gameObject.tag = placedString;
                selectedObject.gameObject.layer = placedInt;
                selectedObject = null;
            }
        }
        else
        {
            //Checking if the object is selectable
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, selectableLayer))
            {
                //Selecting
                selectedObject = hit.transform;
            }
        }

        //if (Input.GetMouseButtonDown(0) && selectedObject != null && canPlace)
        //{
        //    selectedObject.gameObject.tag = placedString;
        //    selectedObject.gameObject.layer = placedInt;
        //    selectedObject = null;
        //}
    }

    //Changing canPlace value and setting triggers 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == placedString)
        {
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
        if (other.gameObject.tag == placedString)
        {
            canPlace = true;
            Trigger.GetComponent<Renderer>().material = green;
        }
    }
}
