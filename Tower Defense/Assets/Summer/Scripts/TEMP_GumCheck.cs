using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_GumCheck : MonoBehaviour
{
    public int stuckCount = 2;
    float movementSpeed;
    EnemyMovement thisEnemyMove;
    TEMP_GumCheck gumCheck;

    float distance = 1.2f;

    [SerializeField] LayerMask enemyLayer;

    float timer = 2;

    float baseSpeed;

    Transform startParent;

    private void Start()
    {
        thisEnemyMove = GetComponent<EnemyMovement>();
        gumCheck = GetComponent<TEMP_GumCheck>();
        baseSpeed = thisEnemyMove.f_Speed;
        startParent = transform.parent;
    }

    private void Update()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.forward * 1f, Color.red);

        if (gameObject.tag == "Sticky" && timer > 0)
        {
            timer -= Time.deltaTime;
            Check();
        }
        else if (timer <= 0)
        {
            gameObject.tag = "Enemy";
            timer = 2f;
            transform.parent = startParent;
            stuckCount = 2;
            thisEnemyMove.f_Speed = baseSpeed;
            GumTower.stickyEnemies.Remove(gameObject);
        }
    }

    private void Check()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), -transform.forward * distance,
            out RaycastHit hit, distance, enemyLayer) && hit.collider.gameObject != gameObject)
        {
            Debug.Log("Enemy hit");
            hit.collider.gameObject.TryGetComponent<EnemyMovement>(out var hitMove);
            hit.collider.gameObject.TryGetComponent<TEMP_GumCheck>(out var hitCheck);

            thisEnemyMove.f_Speed = 0.5f;

            if (hitMove == null || hitCheck == null)
                return;
          
            if (stuckCount > 0 && hitCheck.stuckCount > 0)
            {
                if (hit.collider.gameObject.tag != "Sticky")
                {
                    hit.collider.gameObject.tag = "Sticky";
                    GumTower.stickyEnemies.Add(hit.collider.gameObject);
                }

                if (GumTower.stickyEnemies.IndexOf(gameObject) < GumTower.stickyEnemies.IndexOf(hit.collider.gameObject))
                {
                    hit.collider.transform.parent = transform;
                    hitMove.pathIndex = thisEnemyMove.pathIndex;
                    hitMove.f_Speed = thisEnemyMove.f_Speed;
                }
                else
                {
                    transform.parent = hit.collider.transform;
                    thisEnemyMove.pathIndex = hitMove.pathIndex;
                    thisEnemyMove.f_Speed = hitMove.f_Speed;
                }

                hitCheck.stuckCount--;
                stuckCount--;
            }
        }
    }
}
