using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfusionSpell : SpellParent
{
    [SerializeField] private GameObject rotatingDuck;
    private List<GameObject> allConfused = new List<GameObject>();
    private List<EnemyMovement> enemies = new List<EnemyMovement>();

    public override void SpawnSpell(Vector3 spellPos)
    {
        base.SpawnSpell(spellPos);
        Instantiate(spellEffect).transform.position = spellPos;
        Confusing();
    }

    private void Confusing()
    {
        enemies.Clear();
        foreach (var obj in collisionsInSpell)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null && enemy.gameObject.layer != 0)
                enemies.Add(enemy);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].isConfused = true;
            allConfused.Add(Instantiate(rotatingDuck, enemies[i].transform));
            allConfused[i].transform.position = new Vector3(enemies[i].transform.position.x, enemies[i].transform.position.y + 2, enemies[i].transform.position.z);
        }
    }

    private void Update()
    {
        if (!spellActive) return;

        if (durationSpell > 0)
            durationSpell -= Time.deltaTime;
        else
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
