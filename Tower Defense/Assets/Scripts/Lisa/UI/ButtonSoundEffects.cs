using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundEffects : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClicked;
    public AudioClip buttonHovered;

    public void PlayClickedSound() 
    {
        audioSource.PlayOneShot(buttonClicked);
    }
    public void PlayHoverSound() 
    {
        audioSource.PlayOneShot(buttonHovered);
    }

}
