using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameParametersKey
{
    AllTweetCount,
}

public class GameParameters : ISaveData
{
    public int AllTweetCount { get; set; }

    public GameParameters()
    {
        AllTweetCount = PlayerPrefs.GetInt(GameParametersKey.AllTweetCount.ToString(), 30);
    }

    public void Save()
    {
        PlayerPrefs.SetInt(GameParametersKey.AllTweetCount.ToString(), AllTweetCount);
    }
}
