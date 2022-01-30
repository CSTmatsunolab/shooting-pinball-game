using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject Usagi;
    Rigidbody2D ridid2D;
    
    void Start()
    {
        Usagi = GameObject.Find ("Usagi");

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
}