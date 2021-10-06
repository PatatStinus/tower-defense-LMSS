using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderWeather : MonoBehaviour
{
    public delegate void Thunder();
    public static event Thunder onThunder;

    public delegate void StopThunder();
    public static event StopThunder onStopThunder;

    [SerializeField] private float thunderHeight;
    [SerializeField] private float weatherTime;
    [SerializeField] private GameObject thunder;
    private List<GameObject> allObjects = new List<GameObject>();
    private GameObject target;
    private GameObject thunderEffect;
    private bool eventGoing;
    private float time;

    private void Start()
    {
        onStopThunder += DestroyObjects;
    }

    private void Update()
    {
        if (!WaveSystem.finishedWave && eventGoing)
            time += Time.deltaTime;

        if (eventGoing && time >= weatherTime)
        {
            onStopThunder();
            eventGoing = false;
        }
    }

    private void ChooseTarget()
    {
        target = allObjects[Random.Range(0, allObjects.Count)];
    }

    public void StartWeather(Transform enemies)
    {
        for (int i = 0; i < enemies.childCount; i++)
            allObjects.Add(enemies.GetChild(i).gameObject);
        time = 0;
        eventGoing = true;
        ChooseTarget();
        SpawnThunder();
        onThunder?.Invoke();
    }

    private void SpawnThunder()
    {
        thunderEffect = Instantiate(thunder);
        thunderEffect.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + thunderHeight, target.transform.position.z);
    }

    private void DestroyObjects()
    {
        Destroy(thunderEffect);
    }
}
