using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapSpell : MonoBehaviour
{
    public float size;
    [SerializeField] private int damage = 3;
    [SerializeField] private int cost = 100;
    [SerializeField] private float durationSpell = 3f;
    [SerializeField] private float distanceFromEnemy = 5;
    [SerializeField] private GameObject allEnemies;
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> zappedEnemies = new List<GameObject>();
    private GameObject closestEnemy;
    private Collider[] collisionsInSpell;
    private GameObject enemy;
    private Vector3 spellPos;
    private float orgTime = -1;
    private Vector3 orgPosEnemy;

    public void SpawnZap(Vector3 spellPos)
    {
        this.spellPos = spellPos;
        Zapped();
        ManaManager.LoseMana(cost);
    }

    private void Zapped()
    {
        collisionsInSpell = Physics.OverlapSphere(spellPos, size);
        foreach (var obj in collisionsInSpell)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null)
                this.enemy = obj.gameObject;
        }

        if (enemy != null)
        {
            orgPosEnemy = enemy.transform.position;
            enemy.GetComponent<EnemyHealth>().hp -= damage;
            zappedEnemies.Add(enemy);
        }
        else
            Destroy(this.gameObject);

        orgTime = durationSpell;
    }

    private void NewZap()
    {
        closestEnemy = null;
        enemies.Clear();

        for (int i = 0; i < allEnemies.transform.childCount; i++)
            enemies.Add(allEnemies.transform.GetChild(i).gameObject);

        for (int i = 0; i < enemies.Count; i++)
        {
            if(Vector3.Distance(enemy.transform.position, enemies[i].transform.position) < distanceFromEnemy)
            {
                for (int k = 0; k < zappedEnemies.Count; k++)
                {
                    if(zappedEnemies[k] == enemies[i])
                    {
                        
                        break;
                    }
                    else 
                    {
                        if(closestEnemy == null)
                            closestEnemy = enemies[i];

                        if(closestEnemy != enemies[i] && Vector3.Distance(enemy.transform.position, closestEnemy.transform.position) > Vector3.Distance(enemy.transform.position, enemies[i].transform.position))
                            closestEnemy = enemies[i];
                    }
                }
            }
        }

        enemy = null;

        if (closestEnemy != null)
        {
            enemy = closestEnemy;
            enemy.GetComponent<EnemyHealth>().hp -= damage;
            orgPosEnemy = enemy.transform.position;
            zappedEnemies.Add(enemy);
        }
        else
            Destroy(this.gameObject);

        orgTime = durationSpell;
    }

    private void Update()
    {
        if (orgTime > 0 && enemy != null)
        {
            orgTime -= Time.deltaTime;
            enemy.transform.position = orgPosEnemy;
        }
        else if (orgTime < 0 && orgTime != -1)
            NewZap();
    }
}
