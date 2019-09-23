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

    //変数宣言
    public int possessedPoint = 0; //所持ポイント
    private float scaleNamakemono = 0.2f; //ナマケモノのスケール

    void Start(){
        GetSaveDate();
        UpdatePointText();
    }

    //セーブデータを取得
    private void GetSaveDate(){
        //int _maxItemCount = SaveData.GetInt(SaveDataKeys.maxItemCount,10);
        int _possessedPoint = SaveData.GetInt(SaveDataKeys.possessedPoint);
        //string _lastDateTime = SaveData.GetString(SaveDataKeys.lastDateTime,DateTime.UtcNow.ToBinary().ToString());
        //long binary = Convert.ToInt64(_lastDateTime);

        //maxItemCount = _maxItemCount;
        possessedPoint = _possessedPoint;
        //lastDateTime = DateTime.FromBinary(binary);
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
        /*
        int gachaCost = SaveData.GetInt(SaveDataKeys.gachaCost,InitialValues.GACHA_COST);
        if(possessedPoint < gachaCost){
            //所持金が足りない
            return;
        }

        //所持金が足りてたらガチャを回す
        possessedPoint -= gachaCost;
        UpdatePointText();
        */
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
        int p = Random.Range(0,100); //0~99の整数
        if(p < 80){

        }else if(p < 85){

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
        float value = SaveData.GetFloat(SaveDataKeys.itemGenerationSpeed,InitialValues.ITEM_GENERATION_SPEED);
        value -= variation;
        SaveData.SetFloat(SaveDataKeys.itemGenerationSpeed,value);
        SaveData.Save();
    }

    //アイテムの単価
    private void LevelUp_ItemUnitPrice(int variation){
        int value = SaveData.GetInt(SaveDataKeys.itemUnitPrice,InitialValues.ITEM_UNITE_PRICE);
        value += variation;
        SaveData.SetInt(SaveDataKeys.itemUnitPrice,value);
        SaveData.Save();
    }

    //アイテムの最大数
    private void LevelUp_MaxItemCount(int variation){
        int value = SaveData.GetInt(SaveDataKeys.maxItemCount,InitialValues.MAX_ITEM_COUNT);
        value += variation;
        SaveData.SetInt(SaveDataKeys.maxItemCount,value);
        SaveData.Save();
    }

    //アイテムのタップ時生成数
    private void LevelUp_ItemNumberOfGenerationOnClick(int variation){
        int value = SaveData.GetInt(SaveDataKeys.itemNumberOfGenerationOnClick,InitialValues.ITEM_NUMBER_OF_GENERATION_ON_CLICK);
        value += variation;
        SaveData.SetInt(SaveDataKeys.itemNumberOfGenerationOnClick,value);
        SaveData.Save();
    }
}
