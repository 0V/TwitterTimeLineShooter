using System.Collections;
using System.Collections.Generic;
using TwitterForUnity;
using UnityEngine;
using UnityEngine.UI;

public class TwitterManager : MonoBehaviour
{

    #region TweetTemplate Unity SerializeField プロパティ
    [SerializeField]
    private GameObject _TweetTemplate;
    public GameObject TweetTemplate
    {
        get { return _TweetTemplate; }
        set { _TweetTemplate = value; }
    }
    #endregion

//    [SerializeField] private int getTweetCount = 50;
    [SerializeField] private float minWaitSecond = 0.4f;
    [SerializeField] private float maxWaitSecond = 4f;
    [SerializeField] private float clearPropotion = 0.6f;
    private Client client;
    private Stream stream;
    private GameSystemScript gameSystem;
    private Queue<Tweet> timeLineQueue = new Queue<Tweet>();


    // Use this for initialization
    void Start()
    {
        gameSystem = (GameSystemScript)(GameObject.Find("GameSystem").GetComponent("GameSystemScript"));

        var oauth = new Oauth(
            SettingManager.Keys.ConsumerKey,
            SettingManager.Keys.ConsumerSecret,
            SettingManager.Keys.AccessToken,
            SettingManager.Keys.AccessTokenSecret);
        client = new Client(oauth);

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["count"] = (SettingManager.GameParams.AllTweetCount).ToString();
        StartCoroutine(ShowTimeLine("statuses/home_timeline", parameters, this.Callback));
    }

    void Callback(bool success, string response)
    {
        if (success)
        {
            var timeline = JsonUtility.FromJson<StatusesHomeTimelineResponse>(response);
            for (int i = 0; i < timeline.items.Length; i++)
            {
                timeLineQueue.Enqueue(timeline.items[i]);
            }
        }
        else
        {
            Debug.Log(response);
        }
    }

    private IEnumerator ShowTimeLine(string endpoint, Dictionary<string, string> parameters, TwitterCallback callback)
    {
        yield return client.Get(endpoint, parameters, callback);

        yield return new WaitForSeconds(3f);

        var tlCount = timeLineQueue.Count;
        gameSystem.SetAllCount(tlCount);
        gameSystem.SetClearCount((int)(tlCount * clearPropotion));
        gameSystem.GameStart();

        for (int i = 0; i < tlCount; i++)
        {
            var tweet = timeLineQueue.Dequeue();

            var tweetTemplateCopy = GameObject.Instantiate(TweetTemplate) as GameObject;
            var tb = tweetTemplateCopy.GetComponent<TweetBehaviour>();
            tb.SetTweet(tweet);

            tweetTemplateCopy.transform.position = GetRandomPosition();
            tweetTemplateCopy.transform.rotation = GetRandomRotation();


            var iconUrl = tweet.user.profile_image_url;


            var www = new WWW(iconUrl);

            // 画像ダウンロード完了を待機
            yield return www;

            // webサーバから取得した画像をRaw Imagで表示する
            tb.IconImage.texture = www.textureNonReadable;

            tweetTemplateCopy.SetActive(true);

            Debug.Log(i + ": " + tweet.text);

            yield return new WaitForSeconds(GetRandomWaitSecond());
        }
        Debug.Log("All tweet has been shown.");
    }

    private float GetRandomWaitSecond()
    {
        return Random.Range(minWaitSecond, maxWaitSecond);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-500, 500), Random.Range(400, 500), Random.Range(-500, 500));
    }

    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
    }
}
