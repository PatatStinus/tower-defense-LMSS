using UnityEngine;
using TMPro;

public class ManageMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private static int money = 1000;


    public static void GetMoney(int giveMoney) //Call ManageMoney.GetMoney(number) to give money to player
    {
        money += giveMoney;
    }

    public static void LoseMoney(int removeMoney) //Call ManageMoney.LoseMoney(number) to remove money from player
    {
        money -= removeMoney;
    }

    private void Start()
    {
        money = 1000;
    }

    private void Update()
    {
        moneyText.text = money.ToString();
    }
}
