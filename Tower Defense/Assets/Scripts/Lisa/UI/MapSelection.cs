using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public MainMenu mainMenu;
    public int index;

    public void SetMapIndex()
    {
        mainMenu.selectedLevelIndex = index;
    }
}
