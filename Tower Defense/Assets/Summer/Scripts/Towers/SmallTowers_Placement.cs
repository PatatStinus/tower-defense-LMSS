using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTowers_Placement : BuildingPlacement
{   
    Balloon_Placement balloonScript;

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
            balloonScript = hit.transform.GetComponent<Balloon_Placement>();

            if (balloonScript != null && balloonScript.towerHoldCount > 0)
            {
                selectedObject.position = new Vector3(balloonScript.towerPlacePoint.position.x, balloonScript.towerPlacePoint.position.y + 0.5f, balloonScript.towerPlacePoint.position.z);
                canPlace = true;

                triggerRender.material = green;

                if (Input.GetMouseButtonDown(0) && canPlace)
                {
                    Trigger.SetActive(false);
                    placedObject = true;
                    selectedObject.gameObject.tag = "Placed";
                    selectedObject.gameObject.layer = placedInt;
                    selectedObject = null;

                    if (TryGetComponent<TowerShooting>(out var shoot))
                    {
                        shoot.detectionRange = 50f;
                    }
                    else if (TryGetComponent<GumTower>(out var gumShoot))
                    {
                        gumShoot.detectionRange = 50f;
                    }
                    balloonScript.towerHoldCount--;
                }
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
