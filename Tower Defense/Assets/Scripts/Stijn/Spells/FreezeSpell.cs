using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class FreezeSpell : MonoBehaviour
{
    public float size;
    [SerializeField] private int cost = 100;
    [SerializeField] private float durationSpell = 3f;
    [SerializeField] private float smokeHeight = 2f;
    [SerializeField] private Material freezeMat;
    [SerializeField] private GameObject iceSpellEffect;
    [SerializeField] private GameObject freezeSmoke;
    private Collider[] collisionsInSpell;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 spellPos;
    private List<VisualEffect> delaySmoke = new List<VisualEffect>();
    private float orgTime = -1;
    private List<Vector3> orgPosEnemy = new List<Vector3>();
    private List<Renderer> rendererEnemy = new List<Renderer>();
    private List<Material> orgMatEnemy = new List<Material>();

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
        iceSpellEffect.transform.position = spellPos;
        Instantiate(iceSpellEffect);
        foreach (var obj in collisionsInSpell)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null)
                enemies.Add(obj.gameObject);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            freezeSmoke.transform.position = new Vector3(enemies[i].transform.position.x, enemies[i].transform.position.y + smokeHeight, enemies[i].transform.position.z);
            GameObject smoke = Instantiate(freezeSmoke);
            delaySmoke.Add(smoke.GetComponent<VisualEffect>());
            rendererEnemy.Add(enemies[i].transform.GetChild(0).gameObject.GetComponent<Renderer>());
            orgMatEnemy.Add(rendererEnemy[i].material);
            rendererEnemy[i].material = freezeMat;
            orgPosEnemy.Add(enemies[i].transform.position);
            if (enemies[i].GetComponent<JumpingAbility>() != null)
                enemies[i].GetComponent<JumpingAbility>().canAbility = false;
        }

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
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                delaySmoke[i].SetFloat("Delay", 100f);
                delaySmoke[i].SetFloat("DelayMax", 100f);
                if (enemies[i] != null)
                {
                    rendererEnemy[i].material = orgMatEnemy[i];
                    if (enemies[i].GetComponent<JumpingAbility>() != null)
                        enemies[i].GetComponent<JumpingAbility>().canAbility = true;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
