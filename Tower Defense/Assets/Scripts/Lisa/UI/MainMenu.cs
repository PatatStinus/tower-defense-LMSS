using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <Author>
/// Lisa Werner
/// </Author>
public class MainMenu : MonoBehaviour
{
    [Header("Size-in Settings")]
    public GameObject titleGameObject;
    [SerializeField] private float timeBeforeSizeInTitle = 0.5f;

    public GameObject[] buttons;
    [SerializeField] private float timeBeforeSizeInButton = 1f;

    void Start()
    {
        titleGameObject.transform.localScale = Vector2.zero; // Set the size of the main menu to zero before it sizes in
        StartCoroutine(SizeIn(titleGameObject, timeBeforeSizeInTitle));

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.localScale = Vector2.zero;
            StartCoroutine(SizeIn(buttons[i], timeBeforeSizeInButton + i));
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
}
