using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectionSceneManager : MonoBehaviour
{   
    public GameObject itemPrefab;
    public GameObject canvasGame;
    public int maxItemCount = 10; //アイテムの最大数
    public float respawnTime = 5; //アイテムが発生する秒数

    private int currentItemCount = 0; //現在のアイテム数
    private DateTime lastDateTime; //前回アイテムを生成した時間

    void Start(){
        currentItemCount = 10;
        //初期オーブ生成
        for(int i=0;i<currentItemCount;i++){
            CreateItem();
        }

        //初期設定
        lastDateTime = DateTime.UtcNow;
    }

    // Update is called once per frame
    void Update(){
        if(currentItemCount < maxItemCount){
            TimeSpan timeSpan = DateTime.UtcNow - lastDateTime;

            if(timeSpan >= TimeSpan.FromSeconds(respawnTime)){
                while(timeSpan >= TimeSpan.FromSeconds(respawnTime)){
                    CreateNewItem();
                    timeSpan -= TimeSpan.FromSeconds(respawnTime);
                }
            }
        }
    }

    //新しいアイテムの生成
    public void CreateNewItem(){
        lastDateTime = DateTime.UtcNow;
        if(currentItemCount >= maxItemCount) return;

        CreateItem();
        currentItemCount++;
    }

    //アイテム生成
    public void CreateItem(){
        GameObject item = (GameObject)Instantiate(itemPrefab);
        item.transform.SetParent(canvasGame.transform, false);
        item.transform.localPosition = new Vector3(
            UnityEngine.Random.Range(-400.0f, 400.0f),
            UnityEngine.Random.Range(300.0f, -600.0f),
            0f);
    }

    //アイテム入手
    public void GetItem(){
        currentItemCount--;
    }
}
