using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager
{
    private static PlayFieldModel playFieldModel;

    public static void loadData()
    {
        if (File.Exists(Application.dataPath + "/data.json"))
        {
            string gameSaveString = File.ReadAllText(Application.dataPath + "/data.json");
            playFieldModel = JsonConvert.DeserializeObject<PlayFieldModel>(gameSaveString);
        }
    }

    public static PlayFieldModel GetPlayFieldModel()
    {
        return playFieldModel;
    }
}
