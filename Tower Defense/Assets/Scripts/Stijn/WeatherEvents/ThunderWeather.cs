using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderWeather : WeatherParent
{
    public delegate void Thunder();
    public static event Thunder onThunder;

    public delegate void StopThunder();
    public static event StopThunder onStopThunder;

    [SerializeField] private float thunderHeight;
    [SerializeField] private GameObject thunder;
    private Vector3 orgPos;
    private List<GameObject> allObjects = new List<GameObject>();
    private GameObject target;
    private List<GameObject> thunderEffect = new List<GameObject>();
    private bool stunned;

    private void Update()
    {
        if(!eventGoing) return;

        if (!WaveSystem.finishedWave)
            time += Time.deltaTime;

        if (time >= weatherTime)
        {
            Destroy(spawnedWeatherEffect);
            eventGoing = false;
            onStopThunder?.Invoke();
        }

        if(stunned && target != null)
        {
            target.transform.position = orgPos;
            //Stun tower shooting
        }
    }

    private void ChooseTarget()
    {
        for (int i = 0; i < allObjects.Count; i++)
        {
            if (allObjects[i] == null || allObjects[i].layer == 0)
            {
                allObjects.RemoveAt(i);
                i--;
            }
        }
        if (allObjects.Count != 0)
            target = allObjects[Random.Range(0, allObjects.Count)];
        else
            eventGoing = true;
    }

    public override void StartWeather(Transform enemies)
    {
        base.StartWeather(enemies);
        for (int i = 0; i < enemies.childCount; i++)
            allObjects.Add(enemies.GetChild(i).gameObject);
        SpawnThunder();
        onThunder?.Invoke();
    }

    private void SpawnThunder()
    {
        if(eventGoing)
        {
            ChooseTarget();
            thunderEffect.Add(Instantiate(thunder));
            thunderEffect.Add(Instantiate(thunder));
            thunderEffect.Add(Instantiate(thunder));

            Invoke(nameof(Stun), 1f);
            Invoke(nameof(DestroyObjects), 1.6f);
            Invoke(nameof(StopStun), 3f);
            Invoke(nameof(SpawnThunder), Random.Range(4f, 6f));
        }
    }

    private void StopStun()
    {
        stunned = false;
    }
    
    private void Stun()
    {
        if (target != null)
        {
            foreach (var thunder in thunderEffect)
                thunder.transform.position = new Vector3(target.transform.position.x, thunderHeight, target.transform.position.z);

            orgPos = target.transform.position;
        }
        stunned = true;
    }

    private void DestroyObjects()
    {
        foreach (var thunder in thunderEffect)
            Destroy(thunder);
        thunderEffect.Clear();
    }
}
