using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{

    #region Center Unity SerializeField プロパティ
    [SerializeField]
    private Vector3 _Center;
    public Vector3 Center
    {
        get { return _Center; }
        set { _Center = value; }
    }
    #endregion


    #region TargetTweet Unity SerializeField プロパティ
    [SerializeField]
    private GameObject _TargetTweet;
    public GameObject TargetTweet
    {
        get { return _TargetTweet; }
        set { _TargetTweet = value; }
    }
    #endregion


    #region ShotSE Unity SerializeField プロパティ
    [SerializeField]
    private AudioSource _ShotSE;
    public AudioSource ShotSE
    {
        get { return _ShotSE; }
        set { _ShotSE = value; }
    }
    #endregion

    #region TimeElapsed Unity SerializeField プロパティ
    [SerializeField]
    private float _TimeElapsed;
    public float TimeElapsed
    {
        get { return _TimeElapsed; }
        set { _TimeElapsed = value; }
    }
    #endregion


    [SerializeField] private float maxShotDistance = 2000f;
    [SerializeField] private float intervalSecond = 2;
   
    // Use this for initialization
    void Start()
    {
        _Center = new Vector3((float)Screen.width / 2, (float)Screen.height / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (_TimeElapsed > 0) _TimeElapsed -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }



    public void Shoot()
    {
        if (_TimeElapsed <= 0)
        {
            _TimeElapsed = intervalSecond;

            ShotSE.Play();
            //            AudioSource.PlayClipAtPoint(ShotSE.clip, transform.position);

            var hit = new RaycastHit();
            var ray = Camera.main.ScreenPointToRay(_Center);

            Debug.DrawRay(ray.origin, ray.direction * 10, Color.cyan);

            // 何かにぶつかったら gameObject を取得
            if (Physics.Raycast(ray, out hit, maxShotDistance))
            {
                _TargetTweet = hit.collider.gameObject;
                if (_TargetTweet.tag != "Tweet") return;

                var tweetRoot = _TargetTweet.transform.root;

                var tb = tweetRoot.GetComponent<TweetBehaviour>();
                if (tb != null) tb.Hit();
            }
            else
            {
                _TargetTweet = null;
            }
        }

    }
}
