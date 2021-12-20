using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Placement : MonoBehaviour
{
    [SerializeField] float yOffset;

    [SerializeField] static Transform selectedObject = null;

    const int placedInt = 12;

    protected RaycastHit hit;

    [SerializeField] Material green;
    [SerializeField] Material red;

    [SerializeField] LayerMask selectableLayer;
    [SerializeField] LayerMask terrainLayer;
    [SerializeField] LayerMask pathLayer;

    [SerializeField] public bool placedObject = false;
    [SerializeField] bool canPlace;

    public Transform towerPlacePoint;
    public int towerHoldCount;

    Renderer triggerRender;

    protected Ray ray;

    private void Start()
    {
        canPlace = true;
        towerHoldCount = 1;
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Checking if a object is selected and that it hasnt been placed
        if (selectedObject != null && !placedObject)
        {
            //Checking if terrain layer is selected with raycast
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, terrainLayer) || Physics.Raycast(ray.origin, ray.direction, out hit, 1000, pathLayer))
            {
                //Setting the towers position as the mouse position
                selectedObject.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
            }

            //if (Physics.Linecast(ray.origin, hit.point, pathLayer))
            //    triggerRender.material = green;
            //else
            //    triggerRender.material = red;

            //Checking if the object should be able to be placed when clicking mouse
            if (Input.GetMouseButton(0) && canPlace == true && Physics.Linecast(ray.origin, hit.point, pathLayer))
            {
                placedObject = true;
                selectedObject.gameObject.tag = "Balloon";
                selectedObject.gameObject.layer = 17;
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
}
