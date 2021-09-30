using System;
using UnityEngine;

/// <Author>
/// Lisa Werner
/// </Author>
public class EnemyAttack : MonoBehaviour
{
    public static event EventHandler OnEnemyReachedLastCheckpoint;

    [SerializeField] private bool hasReachedUnicor = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage">Ammount of damage.</param>
    /// <param name="target">The target of the damage</param>
    private static void DoDamage(int target, int damage)
    {
        target -= damage;
        Debug.Log("Doing damage...");
    }

}
