using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Spells : MonoBehaviour
{
    #region Spells
    [SerializeField] private RainbowRainSpell rainbowSpell;
    #endregion

    [SerializeField] private DecalProjector rangeProjector;
    private Type spellType;
    private object callSpellFunction;
    private DecalProjector usedProjector;
    private Vector3 lastProjectorPos;
    private bool projecting;
    private float projectingRange;
    private string spawnSpellCode;

    private void Update()
    {
        if (projecting)
            ProjectDecal(projectingRange);

        if(projecting && Input.GetMouseButtonDown(0))
            StartSpell();
    }

    public void RainbowRain()
    {
        SpawnProjector();
        projectingRange = rainbowSpell.f_RRSize;
        spawnSpellCode = "SpawnRainbow";
        spellType = Type.GetType("RainbowRainSpell");
        callSpellFunction = Activator.CreateInstance(spellType);
    }

    private void SpawnProjector()
    {
        usedProjector = Instantiate(rangeProjector);
        projecting = true;
    }

    private void ProjectDecal(float radius)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            usedProjector.transform.position = new Vector3(hit.point.x, hit.point.y + 5, hit.point.z);
            usedProjector.size = new Vector3(radius, radius, radius);
        }
    }

    private void StartSpell()
    {
        projecting = false;
        usedProjector.transform.position = lastProjectorPos;
        object[] parameterFunction = new object[1];
        parameterFunction[0] = lastProjectorPos;
        MethodInfo mi = this.GetType().GetMethod(spawnSpellCode);
        mi.Invoke(rainbowSpell, parameterFunction);
        Destroy(usedProjector);
    }
}
