using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingAbility : MonoBehaviour
{
    [HideInInspector] public bool ability = true;
    [SerializeField] private float jumpWidth;
    private Vector3 jumpTarget;
    private Vector3 orgPos;
    private bool isJumping = false;
    private bool jumpAnim;
    private EnemyMovement movement;
    private float yPos;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if(!isJumping && ability)
        {
            Invoke("Jump", Random.Range(2f, 4f));
            isJumping = true;
        }
        if (isJumping && ability && jumpAnim)
            LerpToPos();
    }

    private void Jump()
    {
        float xPos = Random.Range(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex].transform.position.x, EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex + 1].transform.position.x);
        float zPos = Random.Range(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex].transform.position.z, EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex + 1].transform.position.z);
        jumpTarget = new Vector3(xPos, transform.position.y, zPos);
        movement.GetNewWayPoint();
        jumpAnim = true;
        yPos = Vector3.Distance(transform.position, jumpTarget) / 2;
        orgPos = transform.GetChild(0).transform.localPosition;
    }

    private void LerpToPos()
    {
        /*float distanceToTarget = Mathf.Abs((yPos * 2 - Vector3.Distance(transform.position, jumpTarget)) / (yPos * 2) - 1);*/
        float time = Mathf.Abs(Vector3.Distance(transform.position, jumpTarget) / (yPos * 2) - 1);
        if (time >= 1)
            Mathf.Abs(Vector3.Distance(transform.position, jumpTarget) / (yPos) - 1);

        transform.GetChild(0).transform.localPosition = Vector3.Lerp(orgPos, new Vector3(orgPos.x, orgPos.y + 10, orgPos.z), time);  /*new Vector3(orgPos.x, (-yPos / jumpWidth) * Mathf.Pow(Vector3.Distance(transform.position, jumpTarget) - yPos, 2) + 5, orgPos.z);*/
        if (time < 0)
        {
            jumpAnim = false;
            isJumping = false;
        }
    }
}
