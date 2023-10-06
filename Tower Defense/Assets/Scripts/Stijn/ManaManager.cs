using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private GameObject youLost;
    public static int enemiesLeft = 0;
    private static int mana = 1000;
    private static bool didLose;

    public delegate void Lost();
    public static event Lost OnLost;

    private void OnEnable()
    {
        OnLost += HasLost;
    }

    private void OnDisable()
    {
        OnLost -= HasLost;
    }

    private void HasLost()
    {
        didLose = true;
        mana = 0;
        Time.timeScale = 1;
        youLost.SetActive(true);
    }

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
            else if(!didLose)
                OnLost?.Invoke();
        }
    }

    private void Start()
    {
        mana = 1000;
    }

    private void Update()
    {
        manaText.text = mana.ToString();
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

[CanEditMultipleObjects, CustomEditor(typeof(ManaManager))]
public class ManaManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add Mana"))
            ManaManager.GetMana(100);
        if (GUILayout.Button("Remove Mana"))
            ManaManager.LoseMana(100);
    }
}
