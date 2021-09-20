using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public List<TypeOfEnemy> waves; //huilen
}

public class TypeOfEnemy
{
    public GameObject enemy { get; set; }
    public float timeFromLastSpawn { get; set; }
}