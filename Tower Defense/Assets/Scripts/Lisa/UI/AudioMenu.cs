using UnityEngine;
using UnityEngine.UI;

///<Author>
///Lisa Werner
///</Author>

public class AudioMenu : MonoBehaviour
{
    public GameObject icon;
    public Sprite mutedSprite;
    public Sprite originalSprite;

    private Slider slider;

    private void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void ValueChanged()
    {
        bool isMuted = icon.GetComponent<Image>().sprite == mutedSprite;

        if (slider.value < 1 && !isMuted)
        {
            icon.GetComponent<Image>().sprite = mutedSprite;
            return;
        }
        else
        {
            icon.GetComponent<Image>().sprite = originalSprite;
        }

    }
}
