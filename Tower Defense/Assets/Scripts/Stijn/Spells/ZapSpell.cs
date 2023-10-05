using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapSpell : SpellParent
{
    [SerializeField] private float distanceFromEnemy = 5;
    [SerializeField] private GameObject allEnemies;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject closestEnemy;
    private GameObject enemy;
    private Vector3 orgPosEnemy;
    private bool isZapping = false;
    private float orgTime = -1;

    public override void SpawnSpell(Vector3 spellPos)
    {
        base.SpawnSpell(spellPos);
        Zapped();
        isZapping = true;
        Instantiate(spellEffect).transform.position = new Vector3(spellPos.x, spellEffect.transform.position.y, spellPos.z);
    }

    private void Zapped() //Initial zap
    {
        foreach (var obj in collisionsInSpell)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null && enemy.gameObject.layer != 0)
                this.enemy = obj.gameObject;
        }

        if (enemy != null)
        {
            orgPosEnemy = enemy.transform.position;
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
            enemy.GetComponent<EnemyMovement>().isZapped = true;
            if (enemy.TryGetComponent(out JumpingAbility jump))
                jump.canAbility = false;
        }
        else
            Destroy(this.gameObject);

        orgTime = durationSpell;
    }

    private void NewZap() //Find new target to zap after previous zap was finished
    {
        closestEnemy = null;
        enemies.Clear();

        for (int i = 0; i < allEnemies.transform.childCount; i++)
            enemies.Add(allEnemies.transform.GetChild(i).gameObject);

        for (int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].GetComponent<EnemyMovement>().isZapped) //Skip al gezapte enemies
                continue;

            if (Vector3.Distance(enemy.transform.position, enemies[i].transform.position) < distanceFromEnemy) //Als enemy dicht genoeg bij de vorige gezapte enemy is
            {
                if(closestEnemy == null)
                {
                    closestEnemy = enemies[i];
                    continue;
                }

                if(Vector3.Distance(enemy.transform.position, closestEnemy.transform.position) > Vector3.Distance(enemy.transform.position, enemies[i].transform.position)) //Check of dit de dichtsbijzijnde enemy is
                    closestEnemy = enemies[i];
            }
        }

        enemy = null;

        if (closestEnemy != null)
        {
            enemy = closestEnemy;
            orgPosEnemy = enemy.transform.position;
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
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

    private void EnemyKilled() //If previous zapped enemy got killed by zap, find new target
    {
        closestEnemy = null;
        enemies.Clear();

        for (int i = 0; i < allEnemies.transform.childCount; i++)
            enemies.Add(allEnemies.transform.GetChild(i).gameObject);

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<EnemyMovement>().isZapped) //Skip al gezapte enemies
                continue;

            if (Vector3.Distance(orgPosEnemy, enemies[i].transform.position) < distanceFromEnemy) //Als enemy dicht genoeg bij de vorige gezapte enemy is
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
            orgPosEnemy = enemy.transform.position;
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
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
        if (!spellActive) return;

        if (orgTime > 0 && enemy != null)
        {
            orgTime -= Time.deltaTime;
            enemy.transform.position = orgPosEnemy;
        }
        else if (orgTime <= 0)
            NewZap();
        if (enemy == null && isZapping) //Object would be destroyed if enemy was not hit, function is for if enemy got killed by zap damage.
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
