using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Vector3 center;
    public GameObject targetTweet;
    public AudioSource ShotSE;

    [SerializeField] private float maxShotDistance = 2000f;

    [SerializeField] private float intervalSecond = 2;
    public float timeElapsed = 0;

    // Use this for initialization
    void Start()
    {
        center = new Vector3((float)Screen.width / 2, (float)Screen.height / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (timeElapsed > 0) timeElapsed -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }



    public void Shoot()
    {
        if (timeElapsed <= 0)
        {
            timeElapsed = intervalSecond;

            ShotSE.Play();
            //            AudioSource.PlayClipAtPoint(ShotSE.clip, transform.position);

            var hit = new RaycastHit();
            var ray = Camera.main.ScreenPointToRay(center);

            Debug.DrawRay(ray.origin, ray.direction * 10, Color.cyan);

            // 何かにぶつかったら gameObject を取得
            if (Physics.Raycast(ray, out hit, maxShotDistance))
            {
                targetTweet = hit.collider.gameObject;
                if (targetTweet.tag != "Tweet") return;

                var tweetRoot = targetTweet.transform.root;

                var tb = tweetRoot.GetComponent<TweetBehaviour>();
                if (tb != null) tb.Hit();
            }
            else
            {
                targetTweet = null;
            }
        }

    }
}
