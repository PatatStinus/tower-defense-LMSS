using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTowers_Placement : BuildingPlacement
{
    [SerializeField] LayerMask balloonLayer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (placedObject) return;

        base.Update();

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, balloonLayer))
        {
            selectedObject.position = new Vector3(hit.point.x, hit.transform.position.y + 0.5f, hit.point.z);

            if (Input.GetMouseButton(0) && canPlace == true)
            {
                Trigger.SetActive(false);
                placedObject = true;
                selectedObject.gameObject.tag = "Placed";
                selectedObject.gameObject.layer = placedInt;
                selectedObject = null;
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);


    }
}
