using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public abstract class Database
{
    protected TextAsset textData;

    protected string saveData;
    public string path;

    public abstract void InitData();

    public void Save()
    {
        DataUpdate();
        File.WriteAllText(path, saveData);
    }

    public abstract void Load();

    public abstract void DataUpdate();

    public void ResetData()
    {
        File.Delete(path);
    }
}
