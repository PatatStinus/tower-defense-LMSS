using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSpell : MonoBehaviour
{
    public float f_FSSize;
    [SerializeField] private int i_FSCost = 100;
    [SerializeField] private float timeFreezing = 3f;
    //[SerializeField] private ParticleSystem freezeEffect;
    private Collider[] collisionsInFreeze;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 freezePos;
    private float orgTime;
    private List<Vector3> orgPosEnemy = new List<Vector3>();

    public void SpawnFreeze(Vector3 freezePos)
    {
        this.freezePos = freezePos;
        Freezing();
        //freezeEffect.transform.position = new Vector3(freezePos.x, freezeEffect.transform.position.y, freezePos.z);
        //freezeEffect.Play();
        ManaManager.LoseMana(i_FSCost);
    }

    private void Freezing()
    {
        enemies.Clear();
        orgPosEnemy.Clear();
        collisionsInFreeze = Physics.OverlapSphere(freezePos, f_FSSize);
        foreach (var obj in collisionsInFreeze)
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            if (enemy != null)
                enemies.Add(obj.gameObject);
        }

        for (int i = 0; i < enemies.Count; i++)
            orgPosEnemy.Add(enemies[i].transform.position);

        orgTime = timeFreezing;
    }

    private void Update()
    {
        if (orgTime > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].transform.position = orgPosEnemy[i];
            orgTime -= Time.deltaTime;
        }
    }
}
