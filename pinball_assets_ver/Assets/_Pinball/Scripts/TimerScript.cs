 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class TimerScript : MonoBehaviour {

	private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
	private UIManager uIManager;

	public static bool firstLoad = true;
 
	[SerializeField]
	public int minute;
	[SerializeField]
	public float seconds;
	//　前のUpdateの時の秒数
	public float oldSeconds;
	//　タイマー表示用テキスト
	public Text timerText;

	int a = 0;
 
	void Start () {
		minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
		timerText = GetComponentInChildren<Text> ();

		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        //gameObject.SetActive(false);
		spriteRenderer = GetComponent<SpriteRenderer>();
		//gameObject.SetActive(true);

		
	}
 
	void Update () {
		if( a == 1){
			seconds += Time.deltaTime;
			if(seconds >= 60f) {
				minute++;
				seconds = seconds - 60;
			}
			//　値が変わった時だけテキストUIを更新
			if((int)seconds != (int)oldSeconds) {
				timerText.text = minute.ToString("00") + ":" + ((int) seconds).ToString ("00");
			}
			oldSeconds = seconds;
		}

		if(gameManager.gameOver)
		{
			// Debug.Log("GameOver");
			a = 0;
			//gameManager.gameOver = false;
		}
	}

	public void timer() {
		a = 1;
	}
}