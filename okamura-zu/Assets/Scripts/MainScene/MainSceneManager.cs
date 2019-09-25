using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainSceneManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject namakemonoPrefab; //ナマケモノプレハブ
    public GameObject txtPossessedPoint; //所持ポイントテキスト
    public GameObject canvasGame; //ゲームキャンバス
    public GameObject txtItemGenerationSpeed; //生成速度テキスト
    public GameObject txtItemUnitPrice; //アイテム単価
    public GameObject txtMaxItemCount; //アイテム最大数
    public GameObject txtItemNumberOfGenerationOnClick; //タップあたりの生成数

    //変数宣言
    public int possessedPoint = 0; //所持ポイント
    private float scaleNamakemono = 0.2f; //ナマケモノのスケール
    private float itemGenerationSpeed; //アイテム生成速度
    private int itemUnitPrice; //アイテム単価
    private int maxItemCount; //最大生成数
    private int itemNumberOfGenerationOnClick; //タップあたりの生成数

    void Start(){
        GetSaveDate();
        UpdatePointText();
        UpdateItemGenerationSpeedText();
        UpdateItemUnitPriceText();
        UpdateMaxItemCountText();
        UpdateItemNumberOfGenerationOnClickText();
    }

    //セーブデータを取得
    private void GetSaveDate(){
        int _possessedPoint = SaveData.GetInt(SaveDataKeys.possessedPoint);
        float _itemGenerationSpeed = SaveData.GetFloat(SaveDataKeys.itemGenerationSpeed,InitialValues.ITEM_GENERATION_SPEED);
        int _itemUnitPrice = SaveData.GetInt(SaveDataKeys.itemUnitPrice,InitialValues.ITEM_UNITE_PRICE);
        int _maxItemCount = SaveData.GetInt(SaveDataKeys.maxItemCount,InitialValues.MAX_ITEM_COUNT);
        int _itemNumberOfGenerationOnClick = SaveData.GetInt(SaveDataKeys.itemNumberOfGenerationOnClick,InitialValues.ITEM_NUMBER_OF_GENERATION_ON_CLICK);


        possessedPoint = _possessedPoint;
        itemGenerationSpeed = _itemGenerationSpeed;
        itemUnitPrice = _itemUnitPrice;
        maxItemCount = _maxItemCount;
        itemNumberOfGenerationOnClick = _itemNumberOfGenerationOnClick;
    }

    //ポイントテキストの更新
    private void UpdatePointText(){
        txtPossessedPoint.GetComponent<Text>().text = possessedPoint.ToString();

        //データ保存
        SaveData.SetInt(SaveDataKeys.possessedPoint,possessedPoint);
        SaveData.Save();
    }

    //メインボタンクリック
    public void OnClickMainButton(){
        
        int gachaCost = SaveData.GetInt(SaveDataKeys.gachaCost,InitialValues.GACHA_COST);
        if(possessedPoint < gachaCost){
            //所持金が足りない
            return;
        }

        //所持金が足りてたらガチャを回す
        possessedPoint -= gachaCost;
        UpdatePointText();
        
        /*
        ・アイテム生成スピード
        ・アイテムの単価
        ・アイテムの最大生成数
        ・アイテムのタップ時生成数
        */
        /*
        itemUnitPrice(1)                    :60 
        itemMaxCount(1)                     :15  
        itemGenerationSpeed(0.1)            :10 
        itemUnitPrice(10)                   :7 
        itemMaxCount(5)                     :3 
        itemGenerationSpeed(0.3)            :4.949 
        itemNumberOfGenerationOnClick(1)    :0.05
        itemNumberOfGenerationOnClick(10)   :0.001
         */
        float p = Random.Range(0f,100f); 
        if(p < 60){
            LevelUp_ItemUnitPrice(1);
        }else if(p < 75){
            LevelUp_MaxItemCount(1);
        }else if(p < 85){
            LevelUp_ItemGenerationSpeed(0.1f);
        }else if(p < 93){
            LevelUp_ItemUnitPrice(10);
        }else if(p < 95){
            LevelUp_MaxItemCount(5);
        }else if(p < 99.949f){
            LevelUp_ItemGenerationSpeed(0.3f);
        }else if(p < 99.999f){
            LevelUp_ItemNumberOfGenerationOnClick(1);
        }else if(p <= 100){
            LevelUp_ItemNumberOfGenerationOnClick(10);
        }

        CreateNamakemono();
    }

    //ナマケモノを生成
    private void CreateNamakemono(){
        GameObject namakemono = (GameObject)Instantiate(namakemonoPrefab);
        namakemono.transform.SetParent(canvasGame.transform);
        namakemono.transform.localPosition = new Vector3(0,-600,0);
        namakemono.transform.localScale = new Vector3(scaleNamakemono,scaleNamakemono,1);

        
        Vector3 targetPos = new Vector3(
            UnityEngine.Random.Range(-400.0f, 400.0f),
            UnityEngine.Random.Range(0f, -500.0f),
            0f
        );
        namakemono.transform.DOLocalMove(
            targetPos,
            1
        );
    }

    /* ステータス */
    /*
    ・アイテム生成スピード
    ・アイテムの単価
    ・アイテムの最大生成数
    ・アイテムのタップ時生成数
     */
    //アイテム生成スピード
    private void LevelUp_ItemGenerationSpeed(float variation){
        //float value = SaveData.GetFloat(SaveDataKeys.itemGenerationSpeed,InitialValues.ITEM_GENERATION_SPEED);
        itemGenerationSpeed -= variation;
        itemGenerationSpeed = ((itemGenerationSpeed - variation) <= 0.2f)? 0.2f : itemGenerationSpeed - variation;
        //SaveData.SetFloat(SaveDataKeys.itemGenerationSpeed,value);
        //SaveData.Save();
        UpdateItemGenerationSpeedText();
    }

    private void UpdateItemGenerationSpeedText(){
        txtItemGenerationSpeed.GetComponent<Text>().text = itemGenerationSpeed.ToString();

        //データ保存
        SaveData.SetFloat(SaveDataKeys.itemGenerationSpeed,itemGenerationSpeed);
        SaveData.Save();
    }

    //アイテムの単価
    private void LevelUp_ItemUnitPrice(int variation){
        //int value = SaveData.GetInt(SaveDataKeys.itemUnitPrice,InitialValues.ITEM_UNITE_PRICE);
        itemUnitPrice += variation;
        //SaveData.SetInt(SaveDataKeys.itemUnitPrice,value);
        //SaveData.Save();
        UpdateItemUnitPriceText();
    }

    private void UpdateItemUnitPriceText(){
        txtItemUnitPrice.GetComponent<Text>().text = itemUnitPrice.ToString();

        //データ保存
        SaveData.SetInt(SaveDataKeys.itemUnitPrice,itemUnitPrice);
        SaveData.Save();
    }

    //アイテムの最大数
    private void LevelUp_MaxItemCount(int variation){
        //int value = SaveData.GetInt(SaveDataKeys.maxItemCount,InitialValues.MAX_ITEM_COUNT);
        maxItemCount += variation;
        //SaveData.SetInt(SaveDataKeys.maxItemCount,maxItemCount);
        //SaveData.Save();
        UpdateMaxItemCountText();
    }

    private void UpdateMaxItemCountText(){
        txtMaxItemCount.GetComponent<Text>().text = maxItemCount.ToString();

        //データ保存
        SaveData.SetInt(SaveDataKeys.maxItemCount,maxItemCount);
        SaveData.Save();
    }

    //アイテムのタップ時生成数
    private void LevelUp_ItemNumberOfGenerationOnClick(int variation){
        //int value = SaveData.GetInt(SaveDataKeys.itemNumberOfGenerationOnClick,InitialValues.ITEM_NUMBER_OF_GENERATION_ON_CLICK);
        itemNumberOfGenerationOnClick += variation;
        //SaveData.SetInt(SaveDataKeys.itemNumberOfGenerationOnClick,itemNumberOfGenerationOnClick);
        //SaveData.Save();
        UpdateItemNumberOfGenerationOnClickText();
    }

    private void UpdateItemNumberOfGenerationOnClickText(){
        txtItemNumberOfGenerationOnClick.GetComponent<Text>().text = itemNumberOfGenerationOnClick.ToString();

        //データ保存
        SaveData.SetInt(SaveDataKeys.itemNumberOfGenerationOnClick,itemNumberOfGenerationOnClick);
        SaveData.Save();
    }

    //リセット
    public void Reset(){
        SaveData.Clear();
        SaveData.Save();

        Start();
    }
}
