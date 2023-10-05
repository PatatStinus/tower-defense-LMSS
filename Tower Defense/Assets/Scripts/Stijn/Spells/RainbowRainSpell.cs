using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class RainbowRainSpell : SpellParent
{
    [SerializeField] private ParticleSystem rainbowRain;
    private Collider[] collisionsInSpell;
    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private Vector3 spellPos;
    private bool spellActive;
    private float orgTime = -1;

    public override void SpawnSpell(Vector3 spellPos)
    {
        this.spellPos = spellPos;
        spellActive = true;
        RainbowRaining();
        orgTime = durationSpell;
        GameObject rainbowParticle = Instantiate(rainbowRain.gameObject);
        rainbowParticle.transform.position = new Vector3(spellPos.x, rainbowParticle.transform.position.y, spellPos.z);
        rainbowParticle.GetComponent<ParticleSystem>().Play();
        ManageMoney.LoseMoney(cost);
    }

    private void RainbowRaining()
    {
        enemies.Clear();
        collisionsInSpell = Physics.OverlapSphere(spellPos, size);
        foreach (var obj in collisionsInSpell)
        {
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            if (enemy != null && enemy.gameObject.layer != 0)
                enemies.Add(enemy);
        }

        if (durationSpell > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].hp -= damage;
            Invoke("RainbowRaining", .5f);
        }
        else
        {
            spellActive = false;
            durationSpell = orgTime;
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (spellActive)
            durationSpell -= Time.deltaTime;
    }
}
