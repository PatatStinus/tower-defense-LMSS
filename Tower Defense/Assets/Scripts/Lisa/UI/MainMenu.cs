using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <Author>
/// Lisa Werner
/// </Author>
public class MainMenu : MonoBehaviour
{
    [Header("Size-in Settings")]
    public GameObject titleGameObject;
    [SerializeField] private float timeTitle = 0.5f; //Takes this amount of time to scale the title

    public GameObject[] buttons;
    [SerializeField] private float timeButton = 1f; //Takes this amount of time to scale the button

    public GameObject optionsMenu;

    void Start()
    {
        titleGameObject.transform.localScale = Vector2.zero; // Set the size of the main menu to zero before it sizes in
        StartCoroutine(SizeIn(titleGameObject, timeTitle));

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.localScale = Vector2.zero;
            StartCoroutine(SizeIn(buttons[i], timeButton + i));
        }
    }

    private IEnumerator SizeIn(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        go.transform.LeanScale(Vector2.one, time);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options(Transform optionsButton)
    {
        if (optionsButton.transform.localScale.x >= 1 && transform.localScale.y >= 1)
        {
            //Enabele the options menu
            optionsMenu.SetActive(true);
        }
    }
}
