using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerDatabase : Database
{
    public PlayerData Player;

    public override void InitData()
    {
        textData = Resources.Load<TextAsset>("Database/Player_Database");

        string[] line = textData.text.Substring(0, textData.text.Length - 1).Split('\t');

        Player = new PlayerData(line[0], int.Parse(line[1]));

        path = Application.persistentDataPath + "/Player.txt";

        Load();
    }

    public override void Load()
    {
        if (false == File.Exists(path))
        {
            string[] line = textData.text.Substring(0, textData.text.Length - 1).Split('\t');

            SetPlayer(line[0], int.Parse(line[1]));
            return;
        }

        string jdata = File.ReadAllText(path);

        Player = JsonUtility.FromJson<PlayerData>(jdata);
    }

    public override void DataUpdate()
    {
        saveData = JsonUtility.ToJson(Player);
    }

    public void SetPlayer(string name, int gold)
    {
        Player.PlayerName = name;
        Player.Gold = gold;

        Save();
        Load();

        if (null == FarmManager.Instance)
        {
            return;
        }
        FarmManager.Instance._playerName.text = Player.PlayerName;
        FarmManager.Instance._goldText.text = Player.Gold.ToString();
    }

    public void GetGold(int gold)
    {
        Player.Gold += gold;

        Save();
        Load();

        if (null == FarmManager.Instance)
        {
            return;
        }

        FarmManager.Instance._goldText.text = Player.Gold.ToString();
    }
}
