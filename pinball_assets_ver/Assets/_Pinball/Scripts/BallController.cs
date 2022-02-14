using UnityEngine;
using System.Collections;
using SgLib;
using System.Collections.Generic;
using UnityEngine.UI;
//追加
//using UnityEngine.UI;

public class BallController : MonoBehaviour
{

    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    //Sliderを入れる
    GameObject slider;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position += (Random.value >= 0.5f) ? (new Vector3(0.2f, 0)) : (new Vector3(-0.2f, 0));
        gameObject.SetActive(true);
        slider = GameObject.Find("Slider");
        Time.timeScale = 1.0f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Dead") && !gameManager.gameOver) /*|| GetComponent<PlayerController>().slider.value <= 0*/)
        {
            //ダメージ量50
            slider.GetComponent<SliderController>().crush(155);
            SoundManager.Instance.PlaySound(SoundManager.Instance.eploring);
            gameManager.CheckGameOver(gameObject);
            Exploring();
        }

    }

    void Timeset()
    {
        Time.timeScale = 1.0f;
        //Debug.Log("3秒後に実行された");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //花と接触
        if (other.gameObject.CompareTag("flower"))
        {
            //弾けるエフェクトと色の指定
            ParticleSystem particle = Instantiate(gameManager.hitGold, other.transform.position, Quaternion.identity) as ParticleSystem;
            var main = particle.main;
            main.startColor = Color.magenta;
            //other.gameObject.SetActive(false);
            Time.timeScale = 1.5f;
            Invoke("Timeset", 2);
            //Time.timeScale = 1.0f;
        }

        //毒キノコと接触
        if (other.gameObject.CompareTag("poison"))
        {
            //other.gameObject.SetActive(false);
            Time.timeScale = 3.0f;
            Invoke("Timeset", 4);
        }


    }

    /// <summary>
    /// Handle when player die
    /// </summary>
    public void Exploring()
    {
        ParticleSystem particle = Instantiate(gameManager.die, transform.position, Quaternion.identity) as ParticleSystem;
        var main = particle.main;
        main.startColor = spriteRenderer.color;
        particle.Play();
        Destroy(particle.gameObject, 1f);
        Destroy(gameObject);
    }

}
