using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    public MainMenu mainMenu;
    public int difficulty;

    public void SetMapDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }
}
