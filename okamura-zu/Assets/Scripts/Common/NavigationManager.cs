﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NavigationManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject background;
    public GameObject mainView;
    public GameObject collectionView;

    //変数宣言
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private float time_tap = 0; //タップしている時間
    private float time_criterion = 0.5f; //タップかどうかの基準時間
    private bool isTap = false; //画面に触れているかどうか
    private int index = 0;

    void Update(){
        Flick();
    }

    void FixedUpdate(){
        if(!isTap)return;
        time_tap += Time.deltaTime;
    }

    void Flick(){
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            isTap = true;
            touchStartPos = new Vector3(Input.mousePosition.x,
                                        Input.mousePosition.y,
                                        Input.mousePosition.z);
            }

        if (Input.GetKeyUp(KeyCode.Mouse0)){
            touchEndPos = new Vector3(Input.mousePosition.x,
                                    Input.mousePosition.y,
                                    Input.mousePosition.z);
            if(time_tap <= time_criterion){
                GetDirection();
            }
            time_tap = 0;
            isTap = false;
        }
    }

    void GetDirection(){
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;
        string Direction = "";

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX)){
        if (30 < directionX){
                //右向きにフリック
                Direction = "right";
            }else if (-30 > directionX){
                //左向きにフリック
                Direction = "left";
            }
        }else if (Mathf.Abs(directionX)<Mathf.Abs(directionY)){
            if (30 < directionY){
                //上向きにフリック
                Direction = "up";
            }else if (-30 > directionY){
                //下向きのフリック
                Direction = "down";
            }
        }else{
            //タッチを検出
            Direction = "touch";
        }

        switch (Direction){
        case "up":
            //上フリックされた時の処理
        　　 break;

        case "down":
            //下フリックされた時の処理
        　　　 break;

        case "right":
    　　　　　　//右フリックされた時の処理
            Debug.Log("right!!");
            MoveLeft();
        　　　break;

        case "left":
    　　　　　　//左フリックされた時の処理
            Debug.Log("left!!");
            MoveRight();
        　　　break;

        case "touch":
    　　　　　 //タッチされた時の処理
            break;
        default:
            break;
        }
    }

    public void MoveLeft(){
        if(index==-1)return;
        index--;
        background.transform.DOLocalMoveX(background.transform.localPosition.x + 370,1,false);
        mainView.transform.DOLocalMoveX(mainView.transform.localPosition.x + 1125,1,false);
        collectionView.transform.DOLocalMoveX(collectionView.transform.localPosition.x + 1125,1,false);
    }

    public void MoveRight(){
        if(index==1)return;
        index++;
        background.transform.DOLocalMoveX(background.transform.localPosition.x - 370,1,false);
        mainView.transform.DOLocalMoveX(mainView.transform.localPosition.x - 1125,1,false);
        collectionView.transform.DOLocalMoveX(collectionView.transform.localPosition.x - 1125,1,false);
    }

}
