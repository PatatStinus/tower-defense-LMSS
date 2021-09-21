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
    [SerializeField] private float timeBeforeSizeInTitle = 1f;
    public GameObject buttonGameObject;
    [SerializeField] private float timeBeforeSizeInButton = 2f;

    [SerializeField] private float timeBeforeSizeIn = 2f;

    // Start is called before the first frame update
    void Start()
    {
        titleGameObject.transform.localScale = Vector2.zero; // Set the size of the main menu to zero before it sizes in
        buttonGameObject.transform.localScale = Vector2.zero; // Set the size of the main menu to zero before it sizes in

        StartCoroutine(SizeIn(titleGameObject, timeBeforeSizeInTitle));
        StartCoroutine(SizeIn(buttonGameObject, timeBeforeSizeInButton));
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
