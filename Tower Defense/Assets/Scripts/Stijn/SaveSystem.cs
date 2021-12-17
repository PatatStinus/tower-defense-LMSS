using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public ContinueGame gameData = new ContinueGame();

    string safefile;

    private void Start()
    {
        safefile = Application.persistentDataPath + "/ContinueGame.json";
    }

    public void SaveGame()
    {
        string data = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath + "/ContinueGame.json", data);
    }

    public void LoadGame()
    {
        string data = File.ReadAllText(safefile);
        ContinueGame saveData = JsonUtility.FromJson<ContinueGame>(data);
        gameData = saveData;
    }
}

[System.Serializable]
public class ContinueGame
{
    public int mapID;
    public int difficulty;
    public int wave;
    public List<Towers> towers = new List<Towers>();
}

[System.Serializable]
public class Towers
{
    public int id;
    public Vector3 pos;
    public int upgrades1, upgrades2, upgrades3;
}
