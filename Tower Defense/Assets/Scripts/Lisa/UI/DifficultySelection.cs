using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    public MainMenu mainMenu;
    public string difficulty;

    public void SetMapDifficulty()
    {
        mainMenu.selectedDifficultyIndex = difficulty;
    }
}
