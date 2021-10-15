using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Projectile_Script : MonoBehaviour
{
    public Transform Target;

    public Transform turret;

    [SerializeField] GameObject[] paintList = new GameObject[5];

    private GameObject spawnObject;

    public float damage;

    private void Start()
    {

    }

    private void Update()
    {
        if (Target == null)
        {
            Target = transform;
            Destroy(gameObject);
        }
        Vector3 direction = Target.position - transform.position;
        transform.Translate(direction.normalized * 0.02f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            switch (Random.Range(1, 5))
            {
                case 0:
                    spawnObject = Instantiate(paintList[0], transform.position, Quaternion.FromToRotation(Vector3.up, turret.position));
                    break;
                case 1:
                    spawnObject = Instantiate(paintList[1], transform.position, Quaternion.FromToRotation(Vector3.up, turret.position));
                    break;
                case 2:
                    spawnObject = Instantiate(paintList[2], transform.position, Quaternion.FromToRotation(Vector3.up, turret.position));
                    break;
                case 3:
                    spawnObject = Instantiate(paintList[3], transform.position, Quaternion.FromToRotation(Vector3.up, turret.position));
                    break;
                case 4:
                    spawnObject = Instantiate(paintList[4], transform.position, Quaternion.FromToRotation(Vector3.up, turret.position));
                    break;
                default:
                    break;
            }
            if (Random.Range(0, 1) == 0)
            {
                spawnObject.transform.position = new Vector3(spawnObject.transform.position.x + Random.Range(0f, 0.2f),
                    spawnObject.transform.position.y + Random.Range(0f, 0.3f), spawnObject.transform.position.z + Random.Range(0f, 0.2f));
            }
            else
            {
                spawnObject.transform.position = new Vector3(spawnObject.transform.position.x - Random.Range(0f, 0.2f),
                    spawnObject.transform.position.y - Random.Range(0f, 0.3f), spawnObject.transform.position.z - Random.Range(0f, 0.2f));
            }
            spawnObject.transform.parent = Target.transform;

            other.GetComponent<EnemyHealth>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
