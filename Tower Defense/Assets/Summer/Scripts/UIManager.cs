using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject tower;

    Ray ray;
    RaycastHit hit;

    public void towerSpawner_OnClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray.origin, ray.direction, out hit, 1000);

        Instantiate(tower, hit.point, Quaternion.identity);
    }
}
