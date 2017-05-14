using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager
{
    private static TwitterKeys keys;
    public static TwitterKeys Keys
    {
        get { return keys ?? (keys = new TwitterKeys()); }
    }

    private static GameParameters gameParams;
    public static GameParameters GameParams
    {
        get { return gameParams ?? (gameParams = new GameParameters()); }
    }

    public static void SaveAll()
    {
        SaveTwitterKeys();
        SaveGameParams();
    }

    public static void SaveTwitterKeys()
    {
        keys.Save();
    }

    public static void SaveGameParams()
    {
        gameParams.Save();
    }
}
