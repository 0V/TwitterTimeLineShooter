using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    #region ConsumerKeyInput Unity SerializeField プロパティ
    [SerializeField]
    private InputField _ConsumerKeyInput;
    public InputField ConsumerKeyInput
    {
        get { return _ConsumerKeyInput; }
        set { _ConsumerKeyInput = value; }
    }
    #endregion

    #region ConsumerSecretInput Unity SerializeField プロパティ
    [SerializeField]
    private InputField _ConsumerSecretInput;
    public InputField ConsumerSecretInput
    {
        get { return _ConsumerSecretInput; }
        set
        {
            _ConsumerSecretInput = value;
        }
    }
    #endregion

    #region AccessTokenInput Unity SerializeField プロパティ
    [SerializeField]
    private InputField _AccessTokenInput;
    public InputField AccessTokenInput
    {
        get { return _AccessTokenInput; }
        set { _AccessTokenInput = value; }
    }
    #endregion

    #region AccessTokenSecretInput Unity SerializeField プロパティ
    [SerializeField]
    private InputField _AccessTokenSecretInput;
    public InputField AccessTokenSecretInput
    {
        get { return _AccessTokenSecretInput; }
        set { _AccessTokenSecretInput = value; }
    }
    #endregion

    #region TweetCountSlider Unity SerializeField プロパティ
    [SerializeField]
    private Slider _TweetCountSlider;
    public Slider TweetCountSlider
    {
        get { return _TweetCountSlider; }
        set { _TweetCountSlider = value; }
    }
    #endregion

    #region SliderCountText Unity SerializeField プロパティ
    [SerializeField]
    private Text _SliderCountText;
    public Text SliderCountText
    {
        get { return _SliderCountText; }
        set { _SliderCountText = value; }
    }
    #endregion

    #region Modal Unity SerializeField プロパティ
    [SerializeField]
    private ModalPanel _Modal;
    public ModalPanel Modal
    {
        get { return _Modal; }
        set { _Modal = value; }
    }
    #endregion

    #region FavoriteToggle Unity SerializeField プロパティ
    [SerializeField]
    private Toggle _FavoriteToggle;
    public Toggle FavoriteToggle
    {
        get { return _FavoriteToggle; }
        set { _FavoriteToggle = value; }
    }
    #endregion

    private List<InputField> inputList;

    private void Start()
    {
        InitInputField();
        InitGameParams();
        Modal = ModalPanel.Instance();
    }

    private void InitInputField()
    {
        inputList = new List<InputField>();

        inputList.Add(_ConsumerKeyInput);
        inputList.Add(_ConsumerSecretInput);
        inputList.Add(_AccessTokenInput);
        inputList.Add(_AccessTokenSecretInput);

        var keyList = SettingManager.Keys.GetKeyList();

        for (int i = 0; i < 4; i++)
        {
            inputList[i].text = keyList[i];
        }

        // focus
        inputList[0].ActivateInputField();
    }

    private void InitGameParams()
    {
        TweetCountSlider.value = SettingManager.GameParams.AllTweetCount / 10f;
        OnTweetCountSliderChanges();
        FavoriteToggle.isOn = SettingManager.GameParams.DoFavorite;
    }

    private void SaveKeys()
    {
        SettingManager.Keys.ConsumerKey = _ConsumerKeyInput.text;
        SettingManager.Keys.ConsumerSecret = _ConsumerSecretInput.text;
        SettingManager.Keys.AccessToken = _AccessTokenInput.text;
        SettingManager.Keys.AccessTokenSecret = _AccessTokenSecretInput.text;
        SettingManager.SaveTwitterKeys();
    }

    private void SaveParams()
    {
        SettingManager.GameParams.AllTweetCount = (int)TweetCountSlider.value * 10;
        SettingManager.SaveGameParams();
    }

    public void OnTweetCountSliderChanges()
    {
        SliderCountText.text = (TweetCountSlider.value * 10).ToString();
    }

    public void SaveAll()
    {
        SaveKeys();
        SaveParams();
    }

    public void ClickSave()
    {
        SaveAll();
    }

    public bool CheckChanged()
    {
        var keyList = SettingManager.Keys.GetKeyList();

        for (int i = 0; i < 4; i++)
        {
            if (inputList[i].text != keyList[i]) return true;
        }

        if (SettingManager.GameParams.AllTweetCount != (int)TweetCountSlider.value * 10) return true;

        if (SettingManager.GameParams.DoFavorite != FavoriteToggle.isOn) return true;

        return false;
    }

    public void ClickSettingTitle()
    {
        var isChanged = CheckChanged();

        if (isChanged)
        {
            Modal.MessageBox(null,
                 "設定は変更されています",
                 "変更を保存しますか？",
                 () =>
                 {
                     SaveAll();
                     CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { SceneManager.LoadScene("Title"); });
                 },
                 () =>
                 {
                     CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { SceneManager.LoadScene("Title"); });
                 },
                 () => { },
                 () => { },
                 false,
                 "YesNoCancel"
                 );
            /*
             * 
            // 0:ok（はい） 1:cancel(いいえ) 2:alt(キャンセル)
            var resultNum = UnityEditor.EditorUtility.DisplayDialogComplex(
                 "設定は変更されています",
                 "変更を保存しますか？",
                 "はい",
                 "いいえ",
                 "キャンセル"
                 );

            switch (resultNum)
            {
                case 0:
                    SaveAll();
                    break;
                case 1:
                    break;
                case 2:
                    return; // !!! RETURN !!!
                default:
                    break;
            }            */
        }
        else
        {
            CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { SceneManager.LoadScene("Title"); });

        }
    }
}
