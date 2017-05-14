using System.Collections;
using System.Collections.Generic;
using TwitterForUnity;
using UnityEngine;
using UnityEngine.UI;

public class TweetBehaviour : MonoBehaviour
{
    public Text NameText;
    public Text SNText;
    public Text StatusText;
    public RawImage IconImage;
    public bool IsCounted = false;

    [SerializeField]private Color maskColor = new Color(0,0,1,0.4f);

    private GameSystemScript gameSystem;
    private Tweet tweet;

    public AudioSource Sound;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !IsCounted)
        {
            GameObject.Find("MissSE0").GetComponent<AudioSource>().Play();
            Miss();
        }
    }

    public void Start()
    {
        gameSystem = (GameSystemScript)(GameObject.Find("GameSystem").GetComponent("GameSystemScript"));
    }

    public void SetTweet(Tweet tw)
    {
        tweet = tw;
        TextUpdate(tw.user.name, tw.user.screen_name, tw.text);
    }

    public void TextUpdate(string name, string sn, string status)
    {
        NameText.text = name;
        SNText.text = "@" + sn;
        StatusText.text = status;
    }    

    public void Hit()
    {
        if (IsCounted) return;
        this.SendMessage("Explode");

        this.transform.root.FindChild("Canvas").GetComponent<Canvas>().enabled = false;
        IsCounted = true;
        gameSystem.AddFavCount();
        this.transform.Find("TweetBox").gameObject.SetActive(false);
    }

    public void Miss()
    {
        if (IsCounted) return;

        IsCounted = true;
        gameSystem.AddMissCount();
        this.transform.root.Find("Canvas").transform.Find("ImageBGMask").GetComponent<Image>().color = maskColor;
    }
}
