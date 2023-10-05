using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapSpell : SpellParent
{
    [SerializeField] private float distanceFromEnemy = 5;
    [SerializeField] private GameObject allEnemies;
    [SerializeField] private GameObject zapEffectStart;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject closestEnemy;
    private GameObject enemy;
    private GameObject zapEffectLong;
    private Collider[] collisionsInSpell;
    private Vector3 spellPos;
    private float orgTime = -1;
    private Vector3 orgPosEnemy;
    private bool isZapping = false;

    public override void SpawnSpell(Vector3 spellPos)
    {
        this.spellPos = spellPos;
        Zapped();
        ManageMoney.LoseMoney(cost);
        isZapping = true;
        zapEffectLong = Instantiate(zapEffectStart);
        zapEffectLong.transform.position = new Vector3(spellPos.x, zapEffectLong.transform.position.y, spellPos.z);
    }

    private void Zapped()
    {
        collisionsInSpell = Physics.OverlapSphere(spellPos, size);
        foreach (var obj in collisionsInSpell)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null && enemy.gameObject.layer != 0)
                this.enemy = obj.gameObject;
        }

        if (enemy != null)
        {
            orgPosEnemy = enemy.transform.position;
            enemy.GetComponent<EnemyHealth>().hp -= damage;
            enemy.GetComponent<EnemyMovement>().isZapped = true;
            if (enemy.TryGetComponent(out JumpingAbility jump))
                jump.canAbility = false;
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
            if(enemies[i].GetComponent<EnemyMovement>().isZapped)
                continue;

            if (Vector3.Distance(enemy.transform.position, enemies[i].transform.position) < distanceFromEnemy)
            {
                if(closestEnemy == null)
                {
                    closestEnemy = enemies[i];
                    continue;
                }

                if(Vector3.Distance(enemy.transform.position, closestEnemy.transform.position) > Vector3.Distance(enemy.transform.position, enemies[i].transform.position))
                    closestEnemy = enemies[i];
            }
        }

        enemy = null;

        if (closestEnemy != null)
        {
            enemy = closestEnemy;
            enemy.GetComponent<EnemyHealth>().hp -= damage;
            orgPosEnemy = enemy.transform.position;
            enemy.GetComponent<EnemyMovement>().isZapped = true;
            if (enemy.TryGetComponent(out JumpingAbility jump))
                jump.canAbility = false;
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].GetComponent<EnemyMovement>().isZapped = false;
                if (enemies[i].TryGetComponent(out JumpingAbility jump))
                    jump.canAbility = true;
            }

            Destroy(this.gameObject);
        }

        orgTime = durationSpell;
    }
    private void EnemyKilled()
    {
        closestEnemy = null;
        enemies.Clear();

        for (int i = 0; i < allEnemies.transform.childCount; i++)
            enemies.Add(allEnemies.transform.GetChild(i).gameObject);

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<EnemyMovement>().isZapped)
                continue;

            if (Vector3.Distance(orgPosEnemy, enemies[i].transform.position) < distanceFromEnemy)
            {
                if (closestEnemy == null)
                {
                    closestEnemy = enemies[i];
                    continue;
                }

                if (Vector3.Distance(orgPosEnemy, closestEnemy.transform.position) > Vector3.Distance(orgPosEnemy, enemies[i].transform.position))
                    closestEnemy = enemies[i];
            }
        }

        enemy = null;

        if (closestEnemy != null)
        {
            enemy = closestEnemy;
            enemy.GetComponent<EnemyHealth>().hp -= damage;
            orgPosEnemy = enemy.transform.position;
            enemy.GetComponent<EnemyMovement>().isZapped = true;
            if (enemy.TryGetComponent(out JumpingAbility jump))
                jump.canAbility = false;
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].GetComponent<EnemyMovement>().isZapped = false;
                if (enemies[i].TryGetComponent(out JumpingAbility jump))
                    jump.canAbility = true;
            }

            Destroy(this.gameObject);
        }

        orgTime = durationSpell;
    }

    private void Thunder()
    {
        durationSpell = 2f;
        damage = 50;
    }

    private void StopThunder()
    {
        durationSpell = 0.5f;
        damage = 20;
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
        else if (enemy == null && isZapping)
            EnemyKilled();
    }

    private void Start()
    {
        ThunderWeather.onThunder += Thunder;
        ThunderWeather.onStopThunder += StopThunder;
    }

    private void OnDisable()
    {
        ThunderWeather.onThunder -= Thunder;
        ThunderWeather.onStopThunder -= StopThunder;
    }
}
