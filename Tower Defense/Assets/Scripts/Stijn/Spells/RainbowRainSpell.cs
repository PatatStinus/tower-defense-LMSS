using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowRainSpell : MonoBehaviour
{
    public float size;
    [SerializeField] private int damage = 3;
    [SerializeField] private int cost = 100;
    [SerializeField] private float durationSpell = 3f;
    [SerializeField] private ParticleSystem rainbowRain;
    private GameObject rainbowParticle;
    private Collider[] collisionsInSpell;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 spellPos;
    private bool spellActive;
    private float orgTime = -1;

    public void SpawnRainbow(Vector3 spellPos)
    {
        this.spellPos = spellPos;
        spellActive = true;
        RainbowRaining();
        orgTime = durationSpell;
        rainbowParticle = Instantiate(rainbowRain.gameObject);
        rainbowParticle.transform.position = new Vector3(spellPos.x, rainbowParticle.transform.position.y, spellPos.z);
        rainbowParticle.GetComponent<ParticleSystem>().Play();
        rainbowParticle.GetComponent<RandomRainColor>().startRaining = true;
        ManaManager.LoseMana(cost);
    }

    private void RainbowRaining()
    {
        enemies.Clear();
        collisionsInSpell = Physics.OverlapSphere(spellPos, size);
        foreach (var obj in collisionsInSpell)
        {
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            if (enemy != null)
                enemies.Add(obj.gameObject);
        }

        if (durationSpell > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].GetComponent<EnemyHealth>().hp -= damage;
            Invoke("RainbowRaining", .5f);
        }
        else
        {
            rainbowParticle.GetComponent<RandomRainColor>().startRaining = false;
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
