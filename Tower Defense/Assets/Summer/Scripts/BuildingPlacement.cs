using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] protected static Transform selectedObject = null;

    const string placedString = "Placed";
    protected const int placedInt = 12;

    protected RaycastHit hit;

    [SerializeField] Material green;
    [SerializeField] Material red;

    [SerializeField] protected LayerMask selectableLayer;
    [SerializeField] protected LayerMask terrainLayer;
    [SerializeField] protected LayerMask pathLayer;

    [SerializeField] protected bool placedObject = false;
    [SerializeField] public bool canPlace;

    [SerializeField] protected GameObject Trigger;

    Renderer triggerRender;

    protected Ray ray;

    protected virtual void Start()
    {
        Trigger.SetActive(true);
        canPlace = true;
        triggerRender = Trigger.GetComponent<Renderer>();
    }

    protected virtual void Update()
    {
        //Taking the mouse position relative to the screen
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Checking if a object is selected and that it hasnt been placed
        if (selectedObject != null && !placedObject)
        {
            //Checking if terrain layer is selected with raycast
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, terrainLayer))
            {
                //Setting the towers position as the mouse position
                selectedObject.position = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
            }

            if (Physics.Linecast(ray.origin, hit.point, pathLayer))
                triggerRender.material = red;
            else
                triggerRender.material = green;

            //Checking if the object should be able to be placed when clicking mouse
            if (Input.GetMouseButton(0) && canPlace == true && !Physics.Linecast(ray.origin, hit.point, pathLayer))
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
    }

    //Changing canPlace value and setting triggers 
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == placedString)
        {
            canPlace = false;
            triggerRender.material = red;
        }
        else
        {
            canPlace = true;
            triggerRender.material = green;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == placedString)
        {
            canPlace = true;
            triggerRender.material = green;
        }
    }
}
