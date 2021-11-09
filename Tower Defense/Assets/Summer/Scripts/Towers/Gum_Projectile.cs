using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gum_Projectile : Projectile_Script
{
    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("Overwritten");
        if (other.gameObject.layer == 14 && other.gameObject.tag != "Sticky" && Target == other.transform)
        {
            Debug.Log("Sticky");
            other.gameObject.tag = "Sticky";
            GumTower.stickyEnemies.Add(other.gameObject);
            Debug.Log(GumTower.stickyEnemies);

            Destroy(gameObject);
        }
    }
}
