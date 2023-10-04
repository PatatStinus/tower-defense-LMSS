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
    [SerializeField] private GameObject longThunder;
    private Vector3 orgPos;
    private List<GameObject> allObjects = new List<GameObject>();
    private GameObject target;
    private GameObject thunderEffect;
    private GameObject longThunderEffect;
    private bool eventGoing;
    private bool stunned;
    private float time;

    private void Update()
    {
        if (!WaveSystem.finishedWave && eventGoing)
            time += Time.deltaTime;

        if (eventGoing && time >= weatherTime)
        {
            Destroy(longThunderEffect);
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

    public void StartWeather(Transform enemies)
    {
        for (int i = 0; i < enemies.childCount; i++)
            allObjects.Add(enemies.GetChild(i).gameObject);
        time = 0;
        eventGoing = true;
        longThunderEffect = Instantiate(longThunder);
        SpawnThunder();
        onThunder?.Invoke();
    }

    private void SpawnThunder()
    {
        if(eventGoing)
        {
            ChooseTarget();
            if (target != null)
                thunder.transform.position = new Vector3(target.transform.position.x, thunderHeight, target.transform.position.z);
            thunderEffect = Instantiate(thunder);
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
            orgPos = target.transform.position;
        stunned = true;
    }

    private void DestroyObjects()
    {
        Destroy(thunderEffect);
    }
}
