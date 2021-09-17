using UnityEngine;
using TMPro;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaText;
    public static int mana; //Public so it can be displayed on UI


    public static void GetMana(int giveMana)
    {
        mana += giveMana;
    }

    private void Update()
    {
        manaText.text = mana.ToString();
    } 
}
