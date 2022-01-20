using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject tower;
    [SerializeField] int price;

    Ray ray;
    RaycastHit hit;

    public void towerSpawner_OnClick()
    {
        if (ManageMoney.money >= price)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray.origin, ray.direction, out hit, 1000);

            Instantiate(tower, hit.point, Quaternion.identity);
            ManageMoney.LoseMoney(price);
        }
    }
}
