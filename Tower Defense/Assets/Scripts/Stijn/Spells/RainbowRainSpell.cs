using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class RainbowRainSpell : SpellParent
{
    private List<EnemyHealth> enemies = new List<EnemyHealth>();

    public override void SpawnSpell(Vector3 spellPos)
    {
        base.SpawnSpell(spellPos);
        GameObject rainbowParticle = Instantiate(spellEffect);
        rainbowParticle.transform.position = new Vector3(spellPos.x, rainbowParticle.transform.position.y, spellPos.z);
        rainbowParticle.GetComponent<ParticleSystem>().Play();
        RainbowRaining();
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
                enemies[i].TakeDamage(damage);
            Invoke(nameof(RainbowRaining), .5f);
        }
        else
            Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!spellActive) return;

        durationSpell -= Time.deltaTime;
    }
}
