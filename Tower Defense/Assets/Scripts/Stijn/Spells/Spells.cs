using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Spells : MonoBehaviour
{
    #region Spells
    private RainbowRainSpell rainbowSpell;
    private GameObject rainbowGameObject;
    private FreezeSpell freezeSpell;
    private GameObject freezeGameObject;
    private ConfusionSpell confuseSpell;
    private GameObject confuseGameObject;
    private PoisonLakeSpell poisonSpell;
    private GameObject poisonGameObject;
    private ZapSpell zapSpell;
    private GameObject zapGameObject;
    #endregion

    [SerializeField] private GameObject allSpells;
    [SerializeField] private DecalProjector rangeProjector;
    [SerializeField] private LayerMask ignoreSpells;
    private DecalProjector usedProjector;
    private Vector3 lastProjectorPos;
    private bool projecting;
    private float projectingRange;
    private int spawnSpellCode;

    private void Start() //Get all spells
    {
        rainbowSpell = allSpells.GetComponent<RainbowRainSpell>();
        freezeSpell = allSpells.GetComponent<FreezeSpell>();
        confuseSpell = allSpells.GetComponent<ConfusionSpell>();
        poisonSpell = allSpells.GetComponent<PoisonLakeSpell>();
        zapSpell = allSpells.GetComponent<ZapSpell>();
    }

    private void Update()
    {
        if (projecting) //Project indicator
            ProjectDecal(projectingRange);

        if (projecting && Input.GetMouseButtonDown(0)) //Activate spell
            StartSpell();

        if (projecting && Input.GetMouseButtonDown(1)) //De-active spell
            StopSpell();
    }

    private void SpawnProjector()
    {
        usedProjector = Instantiate(rangeProjector);
        projecting = true;
    }

    private void ProjectDecal(float radius)
    {
        radius *= 2;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 100f, ~ignoreSpells))
        {
            usedProjector.transform.position = hit.point;
            usedProjector.size = new Vector3(radius, radius, 5);
        }
    }

    private void StartSpell() //Spawn and activate spell
    {
        projecting = false;
        lastProjectorPos = usedProjector.transform.position;
        Destroy(usedProjector.gameObject);
        switch (spawnSpellCode) //Index of spell
        {
            case 1:
                rainbowGameObject = Instantiate(rainbowSpell.gameObject);
                rainbowGameObject.GetComponent<RainbowRainSpell>().SpawnRainbow(lastProjectorPos);
                break;
            case 2:
                freezeGameObject = Instantiate(freezeSpell.gameObject);
                freezeGameObject.GetComponent<FreezeSpell>().SpawnFreeze(lastProjectorPos);
                break;
            case 3:
                confuseGameObject = Instantiate(confuseSpell.gameObject);
                confuseGameObject.GetComponent<ConfusionSpell>().SpawnConfuse(lastProjectorPos);
                break;
            case 4:
                poisonGameObject = Instantiate(poisonSpell.gameObject);
                poisonGameObject.GetComponent<PoisonLakeSpell>().SpawnLake(lastProjectorPos);
                break;
            case 5:
                zapGameObject = Instantiate(zapSpell.gameObject);
                zapGameObject.GetComponent<ZapSpell>().SpawnZap(lastProjectorPos);
                break;
        }
    }

    private void StopSpell()
    {
        projecting = false;
        Destroy(usedProjector.gameObject);
    }

    public void RainbowRain()
    {
        SpawnProjector(); //Set size and index of spell
        projectingRange = rainbowSpell.size;
        spawnSpellCode = 1;
    }

    public void FreezeStop()
    {
        SpawnProjector();
        projectingRange = freezeSpell.size;
        spawnSpellCode = 2;
    }

    public void ConfusionSpell()
    {
        SpawnProjector();
        projectingRange = confuseSpell.size;
        spawnSpellCode = 3;
    }

    public void PoisonSpell()
    {
        SpawnProjector();
        projectingRange = poisonSpell.size;
        spawnSpellCode = 4;
    }

    public void ZapSpell()
    {
        SpawnProjector();
        projectingRange = zapSpell.size;
        spawnSpellCode = 5;
    }
}
