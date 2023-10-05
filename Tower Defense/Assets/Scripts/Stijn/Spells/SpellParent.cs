using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellParent : MonoBehaviour
{
    public float size;
    [SerializeField] protected int damage = 3;
    [SerializeField] protected int cost = 100;
    [SerializeField] protected float durationSpell = 3f;


    public virtual void SpawnSpell(Vector3 pos) { Debug.Log("Niet goed"); }
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
