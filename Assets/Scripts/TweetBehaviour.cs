using System.Collections;
using System.Collections.Generic;
using TwitterForUnity;
using UnityEngine;
using UnityEngine.UI;

public class TweetBehaviour : MonoBehaviour
{

    #region NameText Unity SerializeField プロパティ
    [SerializeField]
    private Text _NameText;
    public Text NameText
    {
        get { return _NameText; }
        set { _NameText = value; }
    }
    #endregion

    #region SNText Unity SerializeField プロパティ
    [SerializeField]
    private Text _SNText;
    public Text SNText
    {
        get { return _SNText; }
        set { _SNText = value; }
    }
    #endregion

    #region StatusText Unity SerializeField プロパティ
    [SerializeField]
    private Text _StatusText;
    public Text StatusText
    {
        get { return _StatusText; }
        set { _StatusText = value; }
    }
    #endregion

    #region IconImage Unity SerializeField プロパティ
    [SerializeField]
    private RawImage _IconImage;
    public RawImage IconImage
    {
        get { return _IconImage; }
        set { _IconImage = value; }
    }
    #endregion

    #region IsCounted Unity SerializeField プロパティ
    [SerializeField]
    private bool _IsCounted;
    public bool IsCounted
    {
        get { return _IsCounted; }
        set { _IsCounted = value; }
    }
    #endregion

    #region Sound Unity SerializeField プロパティ
    [SerializeField]
    private AudioSource _Sound;
    public AudioSource Sound
    {
        get { return _Sound; }
        set { _Sound = value; }
    }
    #endregion

    [SerializeField]private Color maskColor = new Color(0,0,1,0.4f);

    private GameSystemScript gameSystem;
    private Tweet tweet;

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
