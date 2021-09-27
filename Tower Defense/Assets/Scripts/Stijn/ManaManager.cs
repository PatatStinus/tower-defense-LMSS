using UnityEngine;
using TMPro;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaText;
    private static int mana = 1000;
    private static bool lost = false;

    //List of mana things that still needs to be added
    //Mana after completing round (When wave system is made)
    //Mana used as ammunition for the towers (When towers are finished)
    //Mana used as currency for spells/towers etc. (When said things are finished)

    public static void GetMana(int giveMana) //Call ManaManager.GetMana(number) to give mana to player
    {
        mana += giveMana;
    }

    public static void LoseMana(int removeMana) //Call ManaManager.RemoveMana(number) to remove mana from player
    {
        if (mana - removeMana >= 0)
            mana -= removeMana;
        else
            lost = true;
    }

    private void Start()
    {
        mana = 1000;
    }

    private void Update()
    {
        manaText.text = mana.ToString();

        if(lost)
        {
            //DEFEAT
        }
    } 
}
