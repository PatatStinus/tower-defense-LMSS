using UnityEngine;
using TMPro;

public class ManageMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    public static int money = 1000;
    private bool maxMoneyReached;
    private float moneyCountdown;


    public static void GetMoney(int giveMoney) //Call ManageMoney.GetMoney(number) to give money to player
    {
        if (!ManaManager.lost)
        money += giveMoney;
    }

    public static void LoseMoney(int removeMoney) //Call ManageMoney.LoseMoney(number) to remove money from player
    {
        money -= removeMoney;
    }

    private void Start()
    {
        money = 9999;
    }

    private void Update()
    {
        moneyText.text = money.ToString();
        if(ManaManager.lost && !maxMoneyReached)
        {
            moneyCountdown += Time.deltaTime;
            money -= Mathf.RoundToInt(100 * Time.deltaTime * moneyCountdown);
            if (money <= -99999)
            {
                money = -99999;
                maxMoneyReached = true;
                moneyText.GetComponent<Animator>().Play("MoneyBlink");
            }
        }
    }
}
