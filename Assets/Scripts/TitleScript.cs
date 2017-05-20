using System.Collections;
using System.Collections.Generic;
using TwitterForUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{

    #region Modal Unity SerializeField プロパティ
    [SerializeField]
    private ModalPanel _Modal;
    public ModalPanel Modal
    {
        get { return _Modal; }
        set { _Modal = value; }
    }
    #endregion

    [SerializeField]
    private Slider loadSlider;
    [SerializeField]
    private GameObject loadingObject;

    private bool isRequesting = false;

    private void Start()
    {
        Modal = ModalPanel.Instance();
    }

    public void ClickTitleStart()
    {
        if (isRequesting) return;
        isRequesting = true;
        var oauth = new Oauth(
            SettingManager.Keys.ConsumerKey,
            SettingManager.Keys.ConsumerSecret,
            SettingManager.Keys.AccessToken,
            SettingManager.Keys.AccessTokenSecret);
        var client = new Client(oauth);

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["include_email"] = "false";
        StartCoroutine(client.Get("account/verify_credentials", parameters, this.Callback));
    }

    private IEnumerator GoStart()
    {
        loadingObject.SetActive(true);
        var async = SceneManager.LoadSceneAsync("GameScene");
        while (!async.isDone)
        {
            yield return loadSlider.value = async.progress;
        }

//        CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { SceneManager.LoadScene("GameScene"); });
    }

    private void Callback(bool success, string response)
    {
        isRequesting = false;

        if (success)
        {
            StartCoroutine(GoStart());
        }
        else
        {
            /*            UnityEditor.EditorUtility.DisplayDialog(
                             "Twitterにアクセスできません",
                             "ネットワークがつながっているか、設定で正しいキーとトークンが入力されているかを確認してください。",
                             "OK"
                             );
              */

            Modal.MessageBox(null,
                 "Twitterにアクセスできません",
                 "ネットワークがつながっているか、設定で正しいキーとトークンが入力されているかを確認してください。",
                 () => { },
                 () => { },
                 () => { },
                 () => { },
                 false,
                 "Ok"
                 );
        }

    }
}
