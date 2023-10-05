using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonLakeSpell : SpellParent
{
    [SerializeField] private float slowness = 2;
    private SphereCollider sc;

    public override void SpawnSpell(Vector3 spellPos)
    {
        Instantiate(spellEffect).transform.position = new Vector3(spellPos.x, 0, spellPos.z);
        spellActive = true;
        ManageMoney.LoseMoney(cost);
        sc = gameObject.AddComponent<SphereCollider>();
        sc.center = spellPos;
        sc.radius = size;
        sc.isTrigger = true;
        gameObject.layer = 3;
    }

    private void Update()
    {
        if (!spellActive) return;

        durationSpell -= Time.deltaTime;
        if (durationSpell < 0)
            sc.center = new Vector3(100f, 1000f, 100f); //Om OnTriggerExit te triggeren
        if (durationSpell < -0.3f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 14)
        {
            if (other.gameObject.TryGetComponent(out EnemyMovement movement))
                movement.divideSpeed = slowness;
            if (other.gameObject.TryGetComponent(out JumpingAbility jump))
                jump.canAbility = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            if (other.gameObject.TryGetComponent(out EnemyMovement movement))
                movement.divideSpeed = 1;
            if (other.gameObject.TryGetComponent(out JumpingAbility jump))
                jump.canAbility = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth health))
                health.TakeDamage(damage * Time.fixedDeltaTime);
        }
    }
}
