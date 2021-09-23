using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Spells : MonoBehaviour
{
    #region Spells
    [SerializeField] private RainbowRainSpell rainbowSpell;
    #endregion

    [SerializeField] private DecalProjector rangeProjector;
    private DecalProjector usedProjector;
    private Vector3 lastProjectorPos;
    private bool projecting;
    private float projectingRange;
    private int spawnSpellCode;

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
        }
    }
    public void RainbowRain()
    {
        SpawnProjector();
        projectingRange = rainbowSpell.f_RRSize;
        spawnSpellCode = 1;
    }
}
