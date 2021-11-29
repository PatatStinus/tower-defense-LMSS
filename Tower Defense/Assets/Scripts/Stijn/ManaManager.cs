using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private GameObject youLost;
    public static int enemiesLeft = 0;
    public static bool lost = false;
    private static int mana = 1000;

    //List of mana things that still needs to be added

    public static void GetMana(int giveMana) //Call ManaManager.GetMana(number) to give mana to player
    {
        mana += giveMana;
    }

    public static void LoseMana(int removeMana) //Call ManaManager.LoseMana(number) to remove mana from player
    {
        if(enemiesLeft > 0)
            enemiesLeft--; 
        else
        {
            if (mana - removeMana > 0)
                mana -= removeMana;
            else
            {
                lost = true;
                mana = 0;
            }
        }
    }

    private void Start()
    {
        mana = 100000;
        lost = false;
    }

    private void Update()
    {
        manaText.text = mana.ToString();

        if(lost)
        {
            Time.timeScale = 1;
            youLost.SetActive(true);
        }
    } 

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuGame()
    {
        SceneManager.LoadScene(0);
    }
}
