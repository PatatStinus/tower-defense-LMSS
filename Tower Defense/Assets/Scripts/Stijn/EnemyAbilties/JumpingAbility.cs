using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingAbility : MonoBehaviour
{
    [SerializeField] private float jumpWidth;
    [SerializeField] private float jumpHeight;
    [SerializeField] private AnimationCurve curve;
    
    [HideInInspector] public bool canAbility = true;
    private bool confusedJump;
    private Vector3 jumpTarget;
    private Vector3 orgPos;
    private Vector3 orgFreeze;
    private bool windingUp;
    private bool landed;
    private bool isJumping = false;
    private bool jumpAnim;
    private EnemyMovement movement;
    private float yPos;
    private float time = 0;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        canAbility = true;
    }

    private void Update()
    {
        if(!isJumping)
        {
            Invoke("Jump", Random.Range(2f, 4f));
            isJumping = true;
        }
        if (isJumping && jumpAnim)
            LerpToPos();

        if (windingUp)
            WindUp();
        if (landed)
            Landed();
    }

    private void Jump()
    {
        if (movement != null && movement.i_waypoitIndex < EnemyPathMaking.t_Points[movement.pathIndex].Length - 2 && canAbility && !movement.isConfused || movement != null && movement.isConfused && movement.i_waypoitIndex > 1 && canAbility)
        {
            float xPos;
            float zPos;
            if(!movement.isConfused)
            {
                xPos = Random.Range(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex].transform.position.x, EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex + 1].transform.position.x);
                zPos = Random.Range(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex].transform.position.z, EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex + 1].transform.position.z);
                confusedJump = false;
            }
            else
            {
                xPos = Random.Range(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex - 1].transform.position.x, EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex - 2].transform.position.x);
                zPos = Random.Range(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex - 1].transform.position.z, EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex - 2].transform.position.z);
                confusedJump = true;
            }
            jumpTarget = new Vector3(xPos, transform.position.y, zPos);
            movement.NewTarget(jumpTarget);
            yPos = Vector3.Distance(transform.position, jumpTarget);
            orgPos = transform.GetChild(0).transform.localPosition;
            movement.usingAbility = true;
            movement.divideSpeed = .5f;
            windingUp = true;
            orgFreeze = transform.position;
        }
        else
            isJumping = false;
    }

    private void LerpToPos()
    {
        if(canAbility)
        {
            gameObject.layer = 0;
            time += Time.deltaTime / (jumpWidth * yPos * 0.1f);

            transform.GetChild(0).transform.localPosition = Vector3.Lerp(orgPos, new Vector3(orgPos.x, orgPos.y + yPos * jumpHeight, orgPos.z), curve.Evaluate(time));
        
            if(time >= 1)
            {
                jumpAnim = false;
                isJumping = false;
                landed = true;
                movement.GetNewWayPoint(confusedJump);
                confusedJump = false;
                time = 0;
                movement.divideSpeed = 1f;
                orgFreeze = transform.position;
            }
        }
        else
            NewTarget();
    }

    private void WindUp()
    {
        if (canAbility)
        {
            time += Time.deltaTime;
            transform.position = orgFreeze;

            if (time >= 1)
            {
                time = 0;
                windingUp = false;
                jumpAnim = true;
            }
        }
        else
            NewTarget();
    }

    private void Landed()
    {
        if (canAbility)
        {
            time += Time.deltaTime;
            transform.position = orgFreeze;
            gameObject.layer = 14;

            if (time >= 1)
            {
                time = 0;
                landed = false;
                movement.usingAbility = false;
            }
        }
        else
            NewTarget();
    }

    private void NewTarget()
    {
        movement.NewTarget(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex].position);
        transform.GetChild(0).transform.localPosition = new Vector3(0, 0.5f, 0);
        isJumping = false;
        windingUp = false;
        landed = false;
        movement.divideSpeed = 1f;
        movement.usingAbility = false;
        time = 0f;
    }
}
