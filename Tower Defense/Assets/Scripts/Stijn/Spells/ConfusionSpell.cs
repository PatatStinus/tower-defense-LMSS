using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfusionSpell : MonoBehaviour
{   
    public float size;
    [SerializeField] private int cost = 100;
    [SerializeField] private float durationSpell = 3f;
    [SerializeField] private GameObject spellEffect;
    [SerializeField] private GameObject particleEffect;
    private Collider[] collisionsInSpell;
    private List<GameObject> allConfused = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 spellPos;
    private float orgTime = -1;

    public void SpawnConfuse(Vector3 spellPos)
    {
        this.spellPos = spellPos;
        particleEffect.transform.position = spellPos;
        Instantiate(particleEffect);
        Confusing();
        ManaManager.LoseMana(cost);
    }

    private void Confusing()
    {
        enemies.Clear();
        collisionsInSpell = Physics.OverlapSphere(spellPos, size);
        foreach (var obj in collisionsInSpell)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null)
                enemies.Add(obj.gameObject);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<EnemyMovement>().isConfused = true;
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
                    enemies[i].GetComponent<EnemyMovement>().isConfused = false;

                Destroy(allConfused[i]);
            }
            Destroy(this.gameObject);
        }
    }
}
