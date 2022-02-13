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

    //毒キノコとの接触フラグ
    int flag = 0;

    //
    public GameManager gameManager;
    public BallController ballController;


    void Start()
    {
        Usagi = GameObject.Find ("Usagi");
        slider = GameObject.Find("Slider");
        flag = 0;
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

    void Timeset()
    {
        flag = 0;
        //Debug.Log("3秒後に実行された");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //りんごと接触
        if (other.CompareTag("Gold") && !gameManager.gameOver)
        {
            //体力を回復
            slider.GetComponent<SliderController>().crush(-50);


            SoundManager.Instance.PlaySound(SoundManager.Instance.hitGold);
            //gameManager.CheckAndUpdateValue();
            ParticleSystem particle = Instantiate(gameManager.hitGold, other.transform.position, Quaternion.identity) as ParticleSystem;
            var main = particle.main;
            main.startColor = other.gameObject.GetComponent<SpriteRenderer>().color;
            particle.Play();
            Destroy(particle.gameObject, 1f);
            Destroy(other.gameObject);
            gameManager.CreateTarget();
        }


        //毒キノコと接触
        if (other.gameObject.CompareTag("poison"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.usagi);
            ParticleSystem particle = Instantiate(gameManager.hitGold, other.transform.position, Quaternion.identity) as ParticleSystem;
            var main = particle.main;
            particle.Play();
            // Destroy(particle.gameObject, 1f);
            // Destroy(other.gameObject);
            //gameManager.CreateTarget();

            flag = 1;
            Invoke("Timeset", 3);
        }

    }
    
    void Update()
    {

        if(flag == 0){//通常
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
        }
        else if(flag == 1){//毒キノコと接触
            // 左矢印が押された時
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-0.05f, 0, 0); // 左に動かす
            }

            // 右矢印が押された時
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(0.05f, 0, 0); // 右に動かす
            }
            // 上矢印が押された時
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, 0.05f, 0); // 上に動かす
            }
            // 下矢印が押された時
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, -0.05f, 0); // 下に動かす
            }
        }
        // 左矢印が押された時
        /*if (Input.GetKey(KeyCode.LeftArrow))
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
        }*/

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