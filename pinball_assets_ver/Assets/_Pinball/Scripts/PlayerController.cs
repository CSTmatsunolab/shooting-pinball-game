using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SgLib;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameObject Usagi;
    GameObject ball;
    
    // ダメージ判定フラグ
    private bool isDamaged = false;

    //最大HPと現在のHP。
    int maxHp = 155;
    int currentHp;
    //Sliderを入れる
    GameObject slider;


    //
    public GameManager gameManager;
    public BallController ballController;


    void Start()
    {
        Usagi = GameObject.Find ("Usagi");
        slider = GameObject.Find("Slider");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") )
        {
            //Debug.Log("bbb");
            isDamaged = true;
            SoundManager.Instance.PlaySound(SoundManager.Instance.usagi);
            StartCoroutine(OnDamage());
            //ダメージ量50
            slider.GetComponent<SliderController>().crush(50);

            //if文追加
            //if(slider.value <= 0){
            //    SoundManager.Instance.PlaySound(SoundManager.Instance.eploring);
                //gameManager.CheckGameOver(ball);
                //GetComponent<BallController>().Exploring();
            //}
            
            //Debug.Log("slider.value : " + slider.value);

        }
    }
    
    void Update()
    {
        // 左矢印が押された時
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-0.1f, 0, 0); // 左に動かす
        }

        // 右矢印が押された時
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.1f, 0, 0); // 右に動かす
        }
        // 上矢印が押された時
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0.1f, 0); // 上に動かす
        }
        // 下矢印が押された時
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -0.1f, 0); // 下に動かす
        }

        // うさぎの移動範囲を制限する
        Usagi.transform.position = (new Vector3(Mathf.Clamp(Usagi.transform.position.x, -1.6f, 1.6f),Mathf.Clamp(Usagi.transform.position.y, -3, 10),Usagi.transform.position.z));
    }

    void FixedUpdate () {
		//ダメージを受けた時の処理
		if(isDamaged){
			float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
			gameObject.GetComponent<SpriteRenderer> ().color =  new Color(1f,1f,1f,level);

		}
	}

    public IEnumerator OnDamage() {

        yield return new WaitForSeconds(3.0f);
            
        // 通常状態に戻す
        isDamaged = false;
        gameObject.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 1f);

    }
}