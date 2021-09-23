using UnityEngine;
using UnityEngine.UI;

/// <Author>
/// Lisa Werner
/// </Author>
public class OptionsMenu : MonoBehaviour
{
    [Header("Options UI")]
    public CanvasGroup background;
    public Transform optionsBox;

    [Header("Audio")]
    public Button button;
    public Sprite audioEnabled;
    public Sprite audioDisabled;
    [SerializeField] private bool audioOn = true;

    [SerializeField] private float time = 0.5f;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        background.alpha = 0;
        background.LeanAlpha(1, time);

        optionsBox.localPosition = new Vector2(0, -Screen.height);
        optionsBox.LeanMoveLocalY(0, time).setEaseOutExpo().delay = 0.1f;
    }
    public void CloseOptions()
    {
        background.LeanAlpha(0, 0.5f);
        optionsBox.LeanMoveLocalY(-Screen.height, time).setEaseInExpo().setOnComplete(() => { gameObject.SetActive(false); });
    }

    public void ToggleAudio()
    {
        audioOn = !audioOn;
        if (!audioOn)
        {
            button.image.sprite = audioDisabled;
        }
        else
        {
            button.image.sprite = audioEnabled;
        }
    }
}
