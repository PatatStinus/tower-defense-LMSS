using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillPlacement : BuildingPlacement
{
    [SerializeField] LayerMask placedLayer;
    protected override void Update()
    {
        if (gameObject.layer == 12)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, placedLayer) && selectedObject == null && hit.collider.gameObject == gameObject)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    selectedObject = hit.transform;
                    Trigger.SetActive(true);
                    placedObject = false;
                    canPlace = true;
                }
            }
        }
        base.Update();
    }
}
