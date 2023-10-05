using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellParent : MonoBehaviour
{
    public float size;
    [SerializeField] protected int damage = 3;
    [SerializeField] protected int cost = 100;
    [SerializeField] protected float durationSpell = 3f;
    [SerializeField] protected GameObject spellEffect;
    protected Vector3 spellPos;
    protected bool spellActive;
    protected Collider[] collisionsInSpell;


    public virtual void SpawnSpell(Vector3 pos)
    {
        spellPos = pos;
        spellActive = true;
        ManageMoney.LoseMoney(cost);
        collisionsInSpell = Physics.OverlapSphere(spellPos, size);
    }
}
