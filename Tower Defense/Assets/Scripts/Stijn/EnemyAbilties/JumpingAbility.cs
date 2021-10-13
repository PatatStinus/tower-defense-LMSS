using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingAbility : MonoBehaviour
{
    [SerializeField] private float jumpWidth;
    [SerializeField] private float jumpHeight;
    [SerializeField] private AnimationCurve curve;
    
    [HideInInspector] public bool canAbility = true;
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
        //To Do: If Frozen/Zapped don't jump
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
        if (movement != null && movement.i_waypoitIndex < EnemyPathMaking.t_Points[movement.pathIndex].Length - 3 && canAbility)
        {
            float xPos = Random.Range(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex].transform.position.x, EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex + 1].transform.position.x);
            float zPos = Random.Range(EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex].transform.position.z, EnemyPathMaking.t_Points[movement.pathIndex][movement.i_waypoitIndex + 1].transform.position.z);
            jumpTarget = new Vector3(xPos, transform.position.y, zPos);
            movement.NewTarget(jumpTarget);
            yPos = Vector3.Distance(transform.position, jumpTarget);
            orgPos = transform.GetChild(0).transform.localPosition;
            movement.usingAbility = true;
            movement.f_Speed *= 2f;
            windingUp = true;
            orgFreeze = transform.position;
        }
        else
            isJumping = false;
    }

    private void LerpToPos()
    {
        gameObject.layer = 0;
        time += Time.deltaTime / (jumpWidth * yPos * 0.1f);

        transform.GetChild(0).transform.localPosition = Vector3.Lerp(orgPos, new Vector3(orgPos.x, orgPos.y + yPos * jumpHeight, orgPos.z), curve.Evaluate(time));
        
        if(time >= 1)
        {
            jumpAnim = false;
            isJumping = false;
            landed = true;
            movement.GetNewWayPoint();
            time = 0;
            movement.f_Speed /= 2f;
            orgFreeze = transform.position;
        }
    }

    private void WindUp()
    {
        time += Time.deltaTime;
        transform.position = orgFreeze;
        
        if(time >= 1)
        {
            time = 0;
            windingUp = false;
            jumpAnim = true;
        }
    }

    private void Landed()
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
}
