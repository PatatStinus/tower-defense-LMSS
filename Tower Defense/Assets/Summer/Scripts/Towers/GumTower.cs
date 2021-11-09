using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumTower : OffensiveTowers
{
    public static List<GameObject> stickyEnemies = new List<GameObject>();
    public override void ShootAtTarget()
    {
        if (timer <= 0)
        {
            bullet.GetComponent<Gum_Projectile>().turret = turret;
            bullet.GetComponent<Gum_Projectile>().Target = closestEnemy;
            Instantiate(bullet, turret.position, Quaternion.identity);
            timer = fireRate;
        }
    }
}
