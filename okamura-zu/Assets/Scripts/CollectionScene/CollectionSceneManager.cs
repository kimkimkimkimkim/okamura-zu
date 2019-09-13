using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectionSceneManager : MonoBehaviour
{   
    //オブジェクト参照
    public GameObject itemPrefab;
    public GameObject canvasGame;
    public GameObject txtPoint;
    public GameObject txtNowItemCount;
    public GameObject txtMaxItemCount;

    //変数宣言
    public int possessedPoint = 0; //所持ポイント
    public int acquiredPoint = 1; //獲得ポイント
    public int maxItemCount = 10; //アイテムの最大数
    public float respawnTime = 5; //アイテムが発生する秒数

    private int currentItemCount = 0; //現在のアイテム数
    private DateTime lastDateTime; //前回アイテムを生成した時間

    void Start(){
        lastDateTime = DateTime.UtcNow;
        GetSaveDate();
        InitialItemCreate();
        UpdatePointText();
        UpdateItemCountText();
    }

    // Update is called once per frame
    void Update(){
        if(currentItemCount < maxItemCount){
            TimeSpan timeSpan = DateTime.UtcNow - lastDateTime;

            if(timeSpan >= TimeSpan.FromSeconds(respawnTime)){
                while(timeSpan >= TimeSpan.FromSeconds(respawnTime)){
                    CreateNewItem();
                    UpdateItemCountText();
                    timeSpan -= TimeSpan.FromSeconds(respawnTime);
                }
            }
        }
    }

    //最初にアイテムを生成
    private void InitialItemCreate(){
        for(int i=0;i<currentItemCount;i++){
            CreateItem();
        }
    }

    //セーブデータを取得
    private void GetSaveDate(){
        int _maxItemCount = SaveData.GetInt(SaveDataKeys.maxItemCount,10);
        int _nowItemCount = SaveData.GetInt(SaveDataKeys.nowItemCount);
        int _possessedPoint = SaveData.GetInt(SaveDataKeys.possessedPoint);
        string _lastDateTime = SaveData.GetString(SaveDataKeys.lastDateTime,DateTime.UtcNow.ToBinary().ToString());
        long binary = Convert.ToInt64(_lastDateTime);

        maxItemCount = _maxItemCount;
        currentItemCount = _nowItemCount;
        possessedPoint = _possessedPoint;
        lastDateTime = DateTime.FromBinary(binary);
    }

    //ポイントテキストの更新
    private void UpdatePointText(){
        txtPoint.GetComponent<Text>().text = possessedPoint.ToString();

        //データ保存 possessedPoint
        SaveData.SetInt(SaveDataKeys.possessedPoint,possessedPoint);
        SaveData.Save();
    }

    //ItemCountテキストの更新
    public void UpdateItemCountText(){
        txtNowItemCount.GetComponent<Text>().text = currentItemCount.ToString();
        txtMaxItemCount.GetComponent<Text>().text = maxItemCount.ToString();
        Debug.Log("SAVE!!!!!");

        //データ保存 nowItemCount,maxItemCount
        SaveData.SetInt(SaveDataKeys.nowItemCount,currentItemCount);
        SaveData.SetInt(SaveDataKeys.maxItemCount,maxItemCount);
        SaveData.Save();
    }

    //新しいアイテムの生成
    public void CreateNewItem(){
        lastDateTime = DateTime.UtcNow;

        //データ保存　lastDateTime
        SaveData.SetString(SaveDataKeys.lastDateTime,lastDateTime.ToBinary().ToString());
        SaveData.Save();

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
        possessedPoint += acquiredPoint;
        UpdatePointText();
        UpdateItemCountText();
    }
}
