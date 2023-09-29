using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingAbility : EnemyMovement
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
    private float yPos;
    private float timeA = 0;

    protected override void Start()
    {
        base.Start();
        canAbility = true;
    }

    protected override void Update()
    {
        base.Update();
        if(!isJumping)
        {
            Invoke(nameof(Jump), Random.Range(2f, 4f));
            transform.GetChild(0).transform.localPosition = new Vector3(0, 0.5f, 0);
            isJumping = true;
        }
        if (isJumping && jumpAnim)
            LerpToPos();

        if (windingUp)
            WindUp();
        if (landed)
            Landed();
    }

    private void Jump() //Needs better system
    {
        if (i_waypoitIndex < EnemyPathMaking.t_Points[pathIndex].Length - 1 && canAbility && !isConfused || isConfused && i_waypoitIndex > 1 && canAbility)
        {
            float xPos;
            float zPos;
            if(!isConfused)
            {
                xPos = Random.Range(EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].transform.position.x, EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex + 1].transform.position.x);
                zPos = Random.Range(EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].transform.position.z, EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex + 1].transform.position.z);
                confusedJump = false;
            }
            else
            {
                xPos = Random.Range(EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex - 1].transform.position.x, EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex - 2].transform.position.x);
                zPos = Random.Range(EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex - 1].transform.position.z, EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex - 2].transform.position.z);
                confusedJump = true;
            }
            jumpTarget = new Vector3(xPos, transform.position.y, zPos);
            NewTarget(jumpTarget);
            yPos = Vector3.Distance(transform.position, jumpTarget);
            orgPos = transform.GetChild(0).transform.localPosition;
            usingAbility = true;
            divideSpeed = .5f;
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
            timeA += Time.deltaTime / (jumpWidth * yPos * 0.1f);

            transform.GetChild(0).transform.localPosition = Vector3.Lerp(orgPos, new Vector3(orgPos.x, orgPos.y + yPos * jumpHeight, orgPos.z), curve.Evaluate(timeA));
        
            if(timeA >= 1)
            {
                jumpAnim = false;
                isJumping = false;
                landed = true;
                GetNewWayPoint(confusedJump);
                confusedJump = false;
                timeA = 0;
                divideSpeed = 1f;
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
            timeA += Time.deltaTime;
            transform.position = orgFreeze;

            if (timeA >= 1)
            {
                timeA = 0;
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
            timeA += Time.deltaTime;
            transform.position = orgFreeze;
            gameObject.layer = 14;

            if (timeA >= 1)
            {
                timeA = 0;
                landed = false;
                usingAbility = false;
            }
        }
        else
            NewTarget();
    }

    private void NewTarget()
    {
        NewTarget(EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position);
        transform.GetChild(0).transform.localPosition = new Vector3(0, 0.5f, 0);
        windingUp = false;
        landed = false;
        divideSpeed = 1f;
        usingAbility = false;
        timeA = 0f;
    }
}
