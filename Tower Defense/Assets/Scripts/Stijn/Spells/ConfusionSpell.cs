using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfusionSpell : SpellParent
{
    [SerializeField] private GameObject spellEffect;
    [SerializeField] private GameObject particleEffect;
    private Collider[] collisionsInSpell;
    private List<GameObject> allConfused = new List<GameObject>();
    private List<EnemyMovement> enemies = new List<EnemyMovement>();
    private Vector3 spellPos;
    private float orgTime = -1;

    public override void SpawnSpell(Vector3 spellPos)
    {
        this.spellPos = spellPos;
        GameObject particleDuck = Instantiate(particleEffect);
        particleDuck.transform.position = spellPos;
        Confusing();
        ManageMoney.LoseMoney(cost);
    }

    private void Confusing()
    {
        enemies.Clear();
        collisionsInSpell = Physics.OverlapSphere(spellPos, size);
        foreach (var obj in collisionsInSpell)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null && enemy.gameObject.layer != 0)
                enemies.Add(enemy);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].isConfused = true;
            allConfused.Add(Instantiate(spellEffect, enemies[i].transform));
            allConfused[i].transform.position = new Vector3(enemies[i].transform.position.x, enemies[i].transform.position.y + 2, enemies[i].transform.position.z);
        }

        orgTime = durationSpell;
    }

    private void Update()
    {
        if (orgTime > 0)
            orgTime -= Time.deltaTime;
        else if (orgTime < 0 && orgTime != -1)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] != null)
                    enemies[i].isConfused = false;

                Destroy(allConfused[i]);
            }
            Destroy(this.gameObject);
        }
    }
}
