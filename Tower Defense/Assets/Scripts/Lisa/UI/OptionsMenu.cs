using UnityEngine;

/// <Author>
/// Lisa Werner
/// </Author>
public class OptionsMenu : MonoBehaviour
{
    public CanvasGroup background;
    public Transform optionsBox;

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
}
