using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossBattleTimer : MonoBehaviour
{   
    //変数宣言
    private GameObject mainSceneManager;
    private const float time_bossBattle = 10; //ボスバトルの時間
    private float time; //タイマーの時間
    private bool isEnable = false; //タイマーが動くか動かないかのフラグ

    void Start(){
        mainSceneManager = GameObject.Find("MainSceneManager");
    }

    //タイマースタート
    void OnEnable(){
        this.GetComponent<Slider>().maxValue = time_bossBattle;
        this.GetComponent<Slider>().value = time_bossBattle;
        time = time_bossBattle;
        isEnable = true;
    }

    void OnDisable(){
        isEnable = false;
    }

    void FixedUpdate(){
        if(!isEnable)return;
        time -= Time.deltaTime;
        this.GetComponent<Slider>().value = time;
        CheckTimer();
    }

    void CheckTimer(){
        if(time > 0) return;
        //終了
        isEnable = false;
        mainSceneManager.GetComponent<MainSceneManager>().FinishBossBattle();
    }
}