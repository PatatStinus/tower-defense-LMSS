using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class FreezeSpell : SpellParent
{
    [SerializeField] private float smokeHeight = 2f;
    [SerializeField] private Material freezeMat;
    [SerializeField] private GameObject freezeSmoke;
    private List<GameObject> enemies = new List<GameObject>();
    private List<VisualEffect> delaySmoke = new List<VisualEffect>();
    private List<Vector3> orgPosEnemy = new List<Vector3>();
    private List<Renderer> rendererEnemy = new List<Renderer>();
    private List<Material> orgMatEnemy = new List<Material>();

    public override void SpawnSpell(Vector3 spellPos)
    {
        base.SpawnSpell(spellPos);
        Instantiate(spellEffect).transform.position = new Vector3(spellPos.x, spellPos.y + .5f, spellPos.z);
        Freezing();
    }

    private void Freezing()
    {
        enemies.Clear();
        orgPosEnemy.Clear();
        foreach (var obj in collisionsInSpell)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null && enemy.gameObject.layer != 0)
                enemies.Add(obj.gameObject);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject smoke = Instantiate(freezeSmoke);
            smoke.transform.position = new Vector3(enemies[i].transform.position.x, enemies[i].transform.position.y + smokeHeight, enemies[i].transform.position.z);
            delaySmoke.Add(smoke.GetComponent<VisualEffect>());
            rendererEnemy.Add(enemies[i].transform.GetChild(0).gameObject.GetComponent<Renderer>());
            orgMatEnemy.Add(rendererEnemy[i].material);
            rendererEnemy[i].material = freezeMat;
            orgPosEnemy.Add(enemies[i].transform.position);
            if (enemies[i].TryGetComponent(out JumpingAbility jump))
                jump.canAbility = false;
        }
    }

    private void Update()
    {
        if (!spellActive) return;

        if (durationSpell > 0)
        {
            durationSpell -= Time.deltaTime;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                    enemies[i].transform.position = orgPosEnemy[i];
            }
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                delaySmoke[i].SetFloat("Delay", 100f);
                delaySmoke[i].SetFloat("DelayMax", 100f);
                if (enemies[i] != null)
                {
                    rendererEnemy[i].material = orgMatEnemy[i];
                    if (enemies[i].TryGetComponent(out JumpingAbility jump))
                        jump.canAbility = true;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
