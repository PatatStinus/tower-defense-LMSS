using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingAbility : MonoBehaviour
{
    [HideInInspector] public bool ability = true;
    [SerializeField] private float jumpWidth;
    [SerializeField] private float jumpHeight;
    
    private Vector3 jumpTarget;
    private Vector3 orgPos;
    private bool isJumping = false;
    private bool jumpAnim;
    private EnemyMovement movement;
    private float yPos;
    private float time = 0;
    private bool down = false;

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
        if (movement != null)
        {
            float xPos = Random.Range(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex].transform.position.x, EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex + 1].transform.position.x);
            float zPos = Random.Range(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex].transform.position.z, EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex + 1].transform.position.z);
            jumpTarget = new Vector3(xPos, transform.position.y, zPos);
            movement.NewTarget(jumpTarget);
            jumpAnim = true;
            yPos = Vector3.Distance(transform.position, jumpTarget);
            orgPos = transform.GetChild(0).transform.localPosition;
            movement.usingAbility = true;
        }
    }

    private void LerpToPos()
    {
        if(!down)
            time += Time.deltaTime / (jumpWidth * yPos * 0.1f);
        else if(down)
            time -= Time.deltaTime / (jumpWidth * yPos * 0.1f);

        if (time >= 1)
            down = true;

        transform.GetChild(0).transform.localPosition = Vector3.Lerp(orgPos, new Vector3(orgPos.x, orgPos.y + yPos / jumpHeight, orgPos.z), time);
        
        if(time <= 0)
        {
            jumpAnim = false;
            isJumping = false;
            movement.GetNewWayPoint();
            movement.usingAbility = false;
            time = 0;
            down = false;
        }
    }
}
