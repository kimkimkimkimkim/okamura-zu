using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DataManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject itemPrefab;
    public GameObject coinArea;
    public List<GameObject> txtPoint;
    public List<GameObject> txtNowItemCount;
    public List<GameObject> txtMaxItemCount;
    public List<GameObject> txtItemGenerationSpeed; //生成速度テキスト
    public List<GameObject> txtItemUnitPrice; //アイテム単価
    public List<GameObject> txtItemNumberOfGenerationOnClick; //タップあたりの生成数

    //変数宣言
    [System.NonSerialized]
    public int possessedPoint = 0; //所持ポイント
    [System.NonSerialized]
    public int maxItemCount = 10; //アイテムの最大数
    [System.NonSerialized]
    public float respawnTime = 5; //アイテムが発生する秒数
    [System.NonSerialized]
    public float itemGenerationSpeed; //アイテム生成速度
    [System.NonSerialized]
    public int itemUnitPrice; //アイテム単価
    [System.NonSerialized]
    public int itemNumberOfGenerationOnClick; //タップあたりの生成数
    [System.NonSerialized]
    public int currentItemCount = 0; //現在のアイテム数
    [System.NonSerialized]
    public DateTime lastDateTime; //前回アイテムを生成した時間


    void Start(){
        lastDateTime = DateTime.UtcNow;
        GetSaveDate();
        InitialItemCreate();
        UpdatePointText();
        UpdateItemCountText();
        UpdateItemGenerationSpeedText();
        UpdateItemUnitPriceText();
        UpdateMaxItemCountText();
        UpdateItemNumberOfGenerationOnClickText();
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
        float _respawnTime = SaveData.GetFloat(SaveDataKeys.itemGenerationSpeed,InitialValues.ITEM_GENERATION_SPEED);
        float _itemGenerationSpeed = SaveData.GetFloat(SaveDataKeys.itemGenerationSpeed,InitialValues.ITEM_GENERATION_SPEED);
        int _itemUnitPrice = SaveData.GetInt(SaveDataKeys.itemUnitPrice,InitialValues.ITEM_UNITE_PRICE);
        int _itemNumberOfGenerationOnClick = SaveData.GetInt(SaveDataKeys.itemNumberOfGenerationOnClick,InitialValues.ITEM_NUMBER_OF_GENERATION_ON_CLICK);

        maxItemCount = _maxItemCount;
        currentItemCount = _nowItemCount;
        possessedPoint = _possessedPoint;
        lastDateTime = DateTime.FromBinary(binary);
        respawnTime = _respawnTime;
        itemGenerationSpeed = _itemGenerationSpeed;
        itemUnitPrice = _itemUnitPrice;
        itemNumberOfGenerationOnClick = _itemNumberOfGenerationOnClick;
    }

    //ポイントテキストの更新
    public void UpdatePointText(){
        foreach(GameObject t in txtPoint){
            t.GetComponent<Text>().text = possessedPoint.ToString();
        }

        //データ保存 possessedPoint
        SaveData.SetInt(SaveDataKeys.possessedPoint,possessedPoint);
        SaveData.Save();
    }

    //ItemCountテキストの更新
    public void UpdateItemCountText(){
        foreach(GameObject t in txtNowItemCount){
            t.GetComponent<Text>().text = currentItemCount.ToString();
        }
        foreach(GameObject t in txtMaxItemCount){
            t.GetComponent<Text>().text = maxItemCount.ToString();
        }

        //データ保存 nowItemCount,maxItemCount
        SaveData.SetInt(SaveDataKeys.nowItemCount,currentItemCount);
        SaveData.SetInt(SaveDataKeys.maxItemCount,maxItemCount);
        SaveData.Save();
    }

    /* ステータス */
    /*
    ・アイテム生成スピード
    ・アイテムの単価
    ・アイテムの最大生成数
    ・アイテムのタップ時生成数
     */
     
    //アイテム生成スピード
    public void LevelUp_ItemGenerationSpeed(float variation){
        //float value = SaveData.GetFloat(SaveDataKeys.itemGenerationSpeed,InitialValues.ITEM_GENERATION_SPEED);
        itemGenerationSpeed -= variation;
        itemGenerationSpeed = ((itemGenerationSpeed - variation) <= 0.2f)? 0.2f : itemGenerationSpeed - variation;
        //SaveData.SetFloat(SaveDataKeys.itemGenerationSpeed,value);
        //SaveData.Save();
        UpdateItemGenerationSpeedText();
    }

    public void UpdateItemGenerationSpeedText(){
        foreach(GameObject t in txtItemGenerationSpeed){
            t.GetComponent<Text>().text = "1個 / " + itemGenerationSpeed.ToString() + "s";
        }

        //データ保存
        SaveData.SetFloat(SaveDataKeys.itemGenerationSpeed,itemGenerationSpeed);
        SaveData.Save();
    }

    //アイテムの単価
    public void LevelUp_ItemUnitPrice(int variation){
        //int value = SaveData.GetInt(SaveDataKeys.itemUnitPrice,InitialValues.ITEM_UNITE_PRICE);
        itemUnitPrice += variation;
        //SaveData.SetInt(SaveDataKeys.itemUnitPrice,value);
        //SaveData.Save();
        UpdateItemUnitPriceText();
    }

    public void UpdateItemUnitPriceText(){
        foreach(GameObject t in txtItemUnitPrice){
            t.GetComponent<Text>().text = itemUnitPrice.ToString() + "円 / 1個";
        }

        //データ保存
        SaveData.SetInt(SaveDataKeys.itemUnitPrice,itemUnitPrice);
        SaveData.Save();
    }

    //アイテムの最大数
    public void LevelUp_MaxItemCount(int variation){
        //int value = SaveData.GetInt(SaveDataKeys.maxItemCount,InitialValues.MAX_ITEM_COUNT);
        maxItemCount += variation;
        //SaveData.SetInt(SaveDataKeys.maxItemCount,maxItemCount);
        //SaveData.Save();
        UpdateMaxItemCountText();
    }

    public void UpdateMaxItemCountText(){
        foreach(GameObject t in txtMaxItemCount){
            t.GetComponent<Text>().text = maxItemCount.ToString();
        }

        //データ保存
        SaveData.SetInt(SaveDataKeys.maxItemCount,maxItemCount);
        SaveData.Save();
    }

     //アイテムのタップ時生成数
    public void LevelUp_ItemNumberOfGenerationOnClick(int variation){
        //int value = SaveData.GetInt(SaveDataKeys.itemNumberOfGenerationOnClick,InitialValues.ITEM_NUMBER_OF_GENERATION_ON_CLICK);
        itemNumberOfGenerationOnClick += variation;
        //SaveData.SetInt(SaveDataKeys.itemNumberOfGenerationOnClick,itemNumberOfGenerationOnClick);
        //SaveData.Save();
        UpdateItemNumberOfGenerationOnClickText();
    }

    public void UpdateItemNumberOfGenerationOnClickText(){
        foreach(GameObject t in txtItemNumberOfGenerationOnClick){
            t.GetComponent<Text>().text = itemNumberOfGenerationOnClick.ToString();
        }
        
        //データ保存
        SaveData.SetInt(SaveDataKeys.itemNumberOfGenerationOnClick,itemNumberOfGenerationOnClick);
        SaveData.Save();
    }

    //新しいアイテムの生成
    public void CreateNewItem(){
        lastDateTime = DateTime.UtcNow;

        //データ保存　lastDateTime
        SaveData.SetString(SaveDataKeys.lastDateTime,lastDateTime.ToBinary().ToString());
        SaveData.Save();

        if(currentItemCount >= maxItemCount) return;
        
        currentItemCount++;
        CreateItem();
    }

    //クリックでの新しいアイテムの生成
    public void CreateNewItemOnClick(){

        if(currentItemCount >= maxItemCount) return;

        currentItemCount++;
        CreateItem();
    }

    //アイテム生成
    public void CreateItem(){
        GameObject item = (GameObject)Instantiate(itemPrefab);
        item.transform.SetParent(coinArea.transform, false);
        item.transform.localPosition = new Vector3(
            UnityEngine.Random.Range(-400.0f, 400.0f),
            UnityEngine.Random.Range(300.0f, -600.0f),
            0f);
        UpdateItemCountText();
    }

    //アイテム入手
    public void GetItem(){
        currentItemCount--;
        possessedPoint += itemUnitPrice;
        UpdatePointText();
        UpdateItemCountText();
    }
}
