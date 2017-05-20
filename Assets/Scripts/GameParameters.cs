using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameParametersKey
{
    AllTweetCount,
    DoFavorite,
}

public class GameParameters : ISaveData
{
    public int AllTweetCount { get; set; }
    public bool DoFavorite { get; set; }

    public GameParameters()
    {
        AllTweetCount = PlayerPrefs.GetInt(GameParametersKey.AllTweetCount.ToString(), 30);
        DoFavorite = PlayerPrefs.GetInt(GameParametersKey.DoFavorite.ToString(), 0) == 1;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(GameParametersKey.AllTweetCount.ToString(), AllTweetCount);

        int doFavoNum = 0;
        if (DoFavorite) doFavoNum = 1;
        PlayerPrefs.SetInt(GameParametersKey.DoFavorite.ToString(), doFavoNum);
    }
}
