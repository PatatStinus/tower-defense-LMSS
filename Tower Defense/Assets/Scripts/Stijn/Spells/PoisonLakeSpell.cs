using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonLakeSpell : MonoBehaviour
{
    public float size;
    [SerializeField] private int damage = 3;
    [SerializeField] private int cost = 100;
    [SerializeField] private float durationSpell = 3f;
    [SerializeField] private float slowness = 2;
    [SerializeField] private GameObject allEnemies;
    private Collider[] collisionsInSpell;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 spellPos;
    private bool spellActive;
    private float orgTime;

    public void SpawnLake(Vector3 spellPos)
    {
        this.spellPos = spellPos;
        spellActive = true;
        PoisonLakeActive();
        orgTime = durationSpell;
        ManaManager.LoseMana(cost);
    }

    private void PoisonLakeActive()
    {
        if (durationSpell > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].GetComponent<EnemyHealth>().hp -= damage;
            Invoke("PoisonLakeActive", .5f);
        }
        else
        {
            spellActive = false;
            durationSpell = orgTime;
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].GetComponent<EnemyMovement>().divideSpeed = 1;
            }
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (spellActive) 
        {
            durationSpell -= Time.deltaTime;

            for (int i = 0; i < allEnemies.transform.childCount; i++)
                allEnemies.transform.GetChild(i).GetComponent<EnemyMovement>().divideSpeed = 1;

            enemies.Clear();
            collisionsInSpell = Physics.OverlapSphere(spellPos, size);
            foreach (var obj in collisionsInSpell)
            {
                EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
                if (enemy != null)
                    enemies.Add(obj.gameObject);
            }

            for (int i = 0; i < enemies.Count; i++)
                enemies[i].GetComponent<EnemyMovement>().divideSpeed = slowness;
        }
    }
}
