using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_ScriptS : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 14)
        {
            Destroy(gameObject);
        }
    }
}
