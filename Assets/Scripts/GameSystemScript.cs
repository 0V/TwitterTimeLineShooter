using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystemScript : MonoBehaviour
{

    public Text AllCountText;
    public Text FavCountText;
    public Text MissCountText;
    public Text ClearCountText;
    public Text StartText;
    public Text ResultText;

    public int AllCount = -1;
    public int FavCount = 0;
    public int MissCount = 0;
    public int ClearCount = 0;

    public bool IsStarted = false;
    public bool IsEnded = false;


    #region Score Unity SerializeField プロパティ
    [SerializeField]
    private int _Score;
    public int Score
    {
        get { return _Score; }
        set { _Score = value; }
    }
    #endregion


    public void SetupText()
    {
        AllCountText.text = "準備中";
        FavCountText.text = "0";
        MissCountText.text = "0";
        ClearCountText.text = "準備中";
    }

    // Use this for initialization
    private void Start()
    {
        SetupText();
    }

    public void GameStart()
    {
        StartCoroutine(GoStartCoroutine());
    }


    // ゲームスタート
    private IEnumerator GoStartCoroutine()
    {
        int startFontSize = 100;
        int endFontSize = 700;
        float diffFontSize = endFontSize - startFontSize;
        //        yield return new WaitForSeconds(1f);
        int fontsize = startFontSize;
        StartText.fontSize = startFontSize;
        StartText.color = new Color(1, 1, 1, 1 - ((fontsize - startFontSize) / diffFontSize));
        StartText.text = "ゲームスタート！";

        for (; fontsize < endFontSize; fontsize += 5)
        {
            StartText.fontSize = fontsize;
            StartText.color = new Color(1, 1, 1, 1 - ((fontsize - startFontSize) / diffFontSize));
            yield return new WaitForSeconds(0.01f);
        }
        StartText.enabled = false;
        yield break;
    }

    public IEnumerator GoResultCoroutine()
    {
        if (IsEnded) yield break;
        IsEnded = true;

        ResultText.text = "最新のツイートまで読み終わった！";
        yield return new WaitForSeconds(2);
        ResultText.text = "最新のツイートまで読み終わった！\n ふぁぼった数 : ";
        yield return new WaitForSeconds(1);
        ResultText.text = "最新のツイートまで読み終わった！\n ふぁぼった数 : " + FavCount;
        yield return new WaitForSeconds(2);
        if (FavCount >= ClearCount)
        {
            SceneManager.LoadScene("Clear");// CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { SceneManager.LoadScene("Clear"); });
        }
        else
        {
            SceneManager.LoadScene("GameOver");
            //            CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { SceneManager.LoadScene("GameOver"); });
        }

        yield break;
    }

    private void FixedUpdate()
    {
        if (!IsEnded && AllCount == (FavCount + MissCount))
        {
            StartCoroutine(GoResultCoroutine());
        }
    }

    public void SetAllCount(int allCount)
    {
        IsStarted = true;
        AllCount = allCount;
        AllCountText.text = AllCount.ToString();
    }

    public void SetClearCount(int clearCount)
    {
        ClearCount = clearCount;
        ClearCountText.text = ClearCount.ToString();
    }


    public void AddAllCount(int add)
    {
        AllCount += add;
        AllCountText.text = AllCount.ToString();
    }

    public void AddFavCount(int add = 1)
    {
        FavCount += add;
        FavCountText.text = FavCount.ToString();
    }

    public void AddMissCount(int add = 1)
    {
        MissCount += add;
        MissCountText.text = MissCount.ToString();
    }
}
