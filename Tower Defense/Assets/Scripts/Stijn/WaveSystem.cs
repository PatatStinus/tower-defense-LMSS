using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private List<TypeOfEnemy> waves;
}

[System.Serializable]
public class TypeOfEnemy
{
    public GameObject enemy { get; set; }
    public float timeFromLastSpawn { get; set; }
}