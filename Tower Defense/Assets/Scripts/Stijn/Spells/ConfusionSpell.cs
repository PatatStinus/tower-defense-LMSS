using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfusionSpell : MonoBehaviour
{   
    public float f_CSSize;
    [SerializeField] private int i_CSCost = 100;
    [SerializeField] private float timeConfused = 3f;
    //[SerializeField] private ParticleSystem freezeEffect;
    private Collider[] collisionsInConfuse;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 confusePos;
    private float orgTime;

    public void SpawnConfuse(Vector3 confusePos)
    {
        this.confusePos = confusePos;
        Confusing();
        //freezeEffect.transform.position = new Vector3(freezePos.x, freezeEffect.transform.position.y, freezePos.z);
        //freezeEffect.Play();
        ManaManager.LoseMana(i_CSCost);
    }

    private void Confusing()
    {
        enemies.Clear();
        collisionsInConfuse = Physics.OverlapSphere(confusePos, f_CSSize);
        foreach (var obj in collisionsInConfuse)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null)
                enemies.Add(obj.gameObject);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<EnemyMovement>().isConfused = true;
        }

        orgTime = timeConfused;
    }

    private void Update()
    {
        if (orgTime > 0)
            orgTime -= Time.deltaTime;
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] != null)
                    enemies[i].GetComponent<EnemyMovement>().isConfused = false;
            }
        }
    }
}
