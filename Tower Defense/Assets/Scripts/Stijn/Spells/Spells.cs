using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Spells : MonoBehaviour
{
    private SpellParent[] spells;

    [SerializeField] protected GameObject allSpells;
    [SerializeField] protected DecalProjector rangeProjector;
    [SerializeField] protected LayerMask ignoreSpells;
    private DecalProjector usedProjector;
    private Vector3 lastProjectorPos;
    private bool projecting;
    private float projectingRange;
    private int spawnSpellCode;

    protected virtual void Start() //Get all spells
    {
        spells = allSpells.GetComponents<SpellParent>();
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
        
        GameObject spell = null;

        spell = Instantiate(allSpells);
        spell.GetComponents<SpellParent>()[spawnSpellCode].SpawnSpell(lastProjectorPos);
    }

    private void StopSpell()
    {
        projecting = false;
        Destroy(usedProjector.gameObject);
    }

    public void TypeSpell(int index)
    {
        SpawnProjector();
        spawnSpellCode = index;
        projectingRange = spells[index].size;
    }
}
