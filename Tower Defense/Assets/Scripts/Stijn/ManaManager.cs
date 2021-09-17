using UnityEngine;
using TMPro;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaText;
    private static int mana;

    //List of mana things that still needs to be added
    //Mana after completing round (When wave system is made)
    //Mana used as ammunition for the towers (When towers are finished)
    //Mana used as currency for spells/towers etc. (When said things are finished)

    public static void GetMana(int giveMana)
    {
        mana += giveMana;
    }

    public static void LoseMana(int removeMana)
    {
        mana -= removeMana;
    }

    private void Update()
    {
        manaText.text = mana.ToString();
    } 
}
