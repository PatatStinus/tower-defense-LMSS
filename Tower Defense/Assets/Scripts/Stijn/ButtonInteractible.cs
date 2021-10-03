using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractible : MonoBehaviour
{
    [SerializeField] private Spells spells;
    private Button button;

    private void Start()
    {
        button = this.gameObject.GetComponent<Button>();
    }

    private void Update()
    {
        button.interactable = spells.enabled;
    }
}
