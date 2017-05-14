using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TwitterKeysKey
{
    ConsumerKey,
    ConsumerSecret,
    AccessToken,
    AccessTokenSecret,
}

public class TwitterKeys : ISaveData
{
    public TwitterKeys()
    {
        ConsumerKey = PlayerPrefs.GetString(TwitterKeysKey.ConsumerKey.ToString(), "");
        ConsumerSecret = PlayerPrefs.GetString(TwitterKeysKey.ConsumerSecret.ToString(), "");
        AccessToken = PlayerPrefs.GetString(TwitterKeysKey.AccessToken.ToString(), "");
        AccessTokenSecret = PlayerPrefs.GetString(TwitterKeysKey.AccessTokenSecret.ToString(), "");
    }

    public string ConsumerKey { get; set; }
    public string ConsumerSecret { get; set; }
    public string AccessToken { get; set; }
    public string AccessTokenSecret { get; set; }

    public List<string> GetKeyList()
    {
        var keyList = new List<string>();
        keyList.Add(ConsumerKey);
        keyList.Add(ConsumerSecret);
        keyList.Add(AccessToken);
        keyList.Add(AccessTokenSecret);
        return keyList;
    }

    public void Save()
    {
        PlayerPrefs.SetString(TwitterKeysKey.ConsumerKey.ToString(), ConsumerKey);
        PlayerPrefs.SetString(TwitterKeysKey.ConsumerSecret.ToString(), ConsumerSecret);
        PlayerPrefs.SetString(TwitterKeysKey.AccessToken.ToString(), AccessToken);
        PlayerPrefs.SetString(TwitterKeysKey.AccessTokenSecret.ToString(), AccessTokenSecret);
    }

}
