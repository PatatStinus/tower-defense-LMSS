using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeSpell : MonoBehaviour
{
    public float size;
    [SerializeField] private int cost = 100;
    [SerializeField] private float durationSpell = 3f;
    private Collider[] collisionsInSpell;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 spellPos;
    private float orgTime = -1;
    private List<Vector3> orgPosEnemy = new List<Vector3>();

    public void SpawnFreeze(Vector3 spellPos)
    {
        this.spellPos = spellPos;
        Freezing();
        ManaManager.LoseMana(cost);
    }

    private void Freezing()
    {
        enemies.Clear();
        orgPosEnemy.Clear();
        collisionsInSpell = Physics.OverlapSphere(spellPos, size);
        foreach (var obj in collisionsInSpell)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null)
                enemies.Add(obj.gameObject);
        }

        for (int i = 0; i < enemies.Count; i++)
            orgPosEnemy.Add(enemies[i].transform.position);

        orgTime = durationSpell;
    }

    private void Update()
    {
        if (orgTime > 0)
        {
            orgTime -= Time.deltaTime;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                    enemies[i].transform.position = orgPosEnemy[i];
            }
        }
        else if (orgTime < 0 && orgTime != -1)
            Destroy(this.gameObject);
    }
}
