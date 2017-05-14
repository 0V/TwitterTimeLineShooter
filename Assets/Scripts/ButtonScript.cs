using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public void ClickContinue()
    {
        CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { SceneManager.LoadScene("GameScene"); });
    }

    public void ClickStart()
    {
        CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { SceneManager.LoadScene("GameScene"); });
    }

    public void ClickTitle()
    {
        CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { SceneManager.LoadScene("Title"); });
    }

    public void ClickSetting()
    {
        CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { SceneManager.LoadScene("Setting"); });
    }


    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; //標準モード
    }
}
