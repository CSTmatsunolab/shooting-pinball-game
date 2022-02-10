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
    public Slider slider;

    //
    public GameManager gameManager;
    public BallController ballController;


    void Start()
    {
        Usagi = GameObject.Find ("Usagi");

       
        //Sliderを満タンにする。
        slider.value = 1;
        //現在のHPを最大HPと同じに。
        currentHp = maxHp;
        //Debug.Log("Start currentHp : " + currentHp);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") )
        {
            //Debug.Log("bbb");
            isDamaged = true;
            SoundManager.Instance.PlaySound(SoundManager.Instance.usagi);
            StartCoroutine(OnDamage());

            //ダメージは1～100の中でランダムに決める。
            int damage = Random.Range(1, 100);
            Debug.Log("damage : " + damage);

            //現在のHPからダメージを引く
            currentHp = currentHp - damage;
            Debug.Log("After currentHp : " + currentHp);

            //最大HPにおける現在のHPをSliderに反映。
            //int同士の割り算は小数点以下は0になるので、
            //(float)をつけてfloatの変数として振舞わせる。
            slider.value = (float)currentHp / (float)maxHp;

            //if文追加
            if(slider.value <= 0){
                SoundManager.Instance.PlaySound(SoundManager.Instance.eploring);
                //gameManager.CheckGameOver(ball);
                //GetComponent<BallController>().Exploring();
            }
            
            Debug.Log("slider.value : " + slider.value);

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