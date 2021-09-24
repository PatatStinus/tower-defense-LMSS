using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Spells : MonoBehaviour
{
    #region Spells
    private RainbowRainSpell rainbowSpell;
    private FreezeSpell freezeSpell;
    private ConfusionSpell confuseSpell;
    #endregion

    [SerializeField] private GameObject allSpells;
    [SerializeField] private DecalProjector rangeProjector;
    private DecalProjector usedProjector;
    private Vector3 lastProjectorPos;
    private bool projecting;
    private float projectingRange;
    private int spawnSpellCode;

    private void Start()
    {
        rainbowSpell = allSpells.GetComponent<RainbowRainSpell>();
        freezeSpell = allSpells.GetComponent<FreezeSpell>();
        confuseSpell = allSpells.GetComponent<ConfusionSpell>();
    }

    private void Update()
    {
        if (projecting)
            ProjectDecal(projectingRange);

        if(projecting && Input.GetMouseButtonDown(0))
            StartSpell();
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
        if(Physics.Raycast(ray, out hit))
        {
            usedProjector.transform.position = new Vector3(hit.point.x, usedProjector.transform.position.y, hit.point.z);
            usedProjector.size = new Vector3(radius, radius, 5);
        }
    }

    private void StartSpell()
    {
        projecting = false;
        lastProjectorPos = usedProjector.transform.position;
        Destroy(usedProjector.gameObject);
        switch (spawnSpellCode)
        {
            case 1:
                rainbowSpell.SpawnRainbow(lastProjectorPos);
                break;
            case 2:
                freezeSpell.SpawnFreeze(lastProjectorPos);
                break;
            case 3:
                confuseSpell.SpawnConfuse(lastProjectorPos);
                break;
        }
    }
    public void RainbowRain()
    {
        SpawnProjector();
        projectingRange = rainbowSpell.f_RRSize;
        spawnSpellCode = 1;
    }

    public void FreezeStop()
    {
        SpawnProjector();
        projectingRange = freezeSpell.f_FSSize;
        spawnSpellCode = 2;
    }

    public void ConfusionSpell()
    {
        SpawnProjector();
        projectingRange = confuseSpell.f_CSSize;
        spawnSpellCode = 3;
    }
}
