using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_GumCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Sticky" || collision.gameObject.tag == "Sticky")
        {
            collision.gameObject.transform.parent = gameObject.transform;
            GetComponent<EnemyMovement>();
        }
    }
}
