using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractible : MonoBehaviour
{
    private Button button;
    private int activeCurses;

    private void Start()
    {
        button = this.gameObject.GetComponent<Button>();
    }

    private void OnEnable()
    {
        CurseEffect.onSpellCurse += DisableButtons;
        CurseEffect.onDisableSpellCurse += EnableButtons;
    }

    private void OnDisable()
    {
        CurseEffect.onSpellCurse -= DisableButtons;
        CurseEffect.onDisableSpellCurse -= EnableButtons;
    }

    private void DisableButtons()
    {
        activeCurses++;
        button.interactable = false;
    }

    private void EnableButtons()
    {
        activeCurses--;
        if(activeCurses <= 0)
        {
            button.interactable = true;
            activeCurses = 0;
        }
    }
}
