using System.Collections;
using System.Collections.Generic;
using TwitterForUnity;
using UnityEngine;

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

    private void Callback(bool success, string response)
    {
        isRequesting = false;

        if (success)
        {
            var res = JsonUtility.FromJson<TweetUser>(response);
            CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); });
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
