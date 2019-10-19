using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

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
    [System.NonSerialized]
    public int index = 0;
    private float time_navigation = 0.5f; //画面遷移移動時間
    private bool canMove = true; //画面遷移できるかどうか

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
        if(!canMove)return;
        //遷移スタート
        canMove = false;
        index--;
        background.transform.DOLocalMoveX(background.transform.localPosition.x + 1125,time_navigation,false);
        mainView.transform.DOLocalMoveX(mainView.transform.localPosition.x + 1125,time_navigation,false);
        collectionView.transform.DOLocalMoveX(collectionView.transform.localPosition.x + 1125,time_navigation,false);
        StartCoroutine(DelayMethod(time_navigation + 0.1f, () => { canMove = true; }));
    }

    public void MoveRight(){
        if(index==1)return;
        if(!canMove)return;
        //遷移スタート
        canMove = false;
        index++;
        background.transform.DOLocalMoveX(background.transform.localPosition.x - 1125,time_navigation,false);
        mainView.transform.DOLocalMoveX(mainView.transform.localPosition.x - 1125,time_navigation,false);
        collectionView.transform.DOLocalMoveX(collectionView.transform.localPosition.x - 1125,time_navigation,false);
        StartCoroutine(DelayMethod(time_navigation + 0.1f, () => { canMove = true; }));
    }

    /// <summary>
    /// 渡された処理を指定時間後に実行する
    /// </summary>
    /// <param name="waitTime">遅延時間[ミリ秒]</param>
    /// <param name="action">実行したい処理</param>
    /// <returns></returns>
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

}
