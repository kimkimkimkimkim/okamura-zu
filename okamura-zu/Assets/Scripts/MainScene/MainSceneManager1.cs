using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainSceneManager1 : MonoBehaviour
{
    //オブジェクト参照
    public GameObject namakemonoPrefab; //ナマケモノプレハブ
    public GameObject txtPossessedPoint; //所持ポイントテキスト
    public GameObject canvasGame; //ゲームキャンバス
    public GameObject txtItemGenerationSpeed; //生成速度テキスト
    public GameObject txtItemUnitPrice; //アイテム単価
    public GameObject txtMaxItemCount; //アイテム最大数
    public GameObject txtItemNumberOfGenerationOnClick; //タップあたりの生成数
    public GameObject namakemonoArea; //ナマケモノを生成するところ
    public GameObject dataManager; //データマネージャー

    //変数宣言
    private float scaleNamakemono = 0.2f; //ナマケモノのスケール

    //メインボタンクリック
    public void OnClickMainButton(){
        
        return;
        int gachaCost = SaveData.GetInt(SaveDataKeys.gachaCost,InitialValues.GACHA_COST);
        if(dataManager.GetComponent<DataManager>().possessedPoint < gachaCost){
            //所持金が足りない
            return;
        }

        //所持金が足りてたらガチャを回す
        dataManager.GetComponent<DataManager>().possessedPoint -= gachaCost;
        dataManager.GetComponent<DataManager>().UpdatePointText();
        
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
            dataManager.GetComponent<DataManager>().LevelUp_ItemUnitPrice(1);
        }else if(p < 75){
            dataManager.GetComponent<DataManager>().LevelUp_MaxItemCount(1);
        }else if(p < 85){
            dataManager.GetComponent<DataManager>().LevelUp_ItemGenerationSpeed(0.1f);
        }else if(p < 93){
            dataManager.GetComponent<DataManager>().LevelUp_ItemUnitPrice(10);
        }else if(p < 95){
            dataManager.GetComponent<DataManager>().LevelUp_MaxItemCount(5);
        }else if(p < 99.949f){
            dataManager.GetComponent<DataManager>().LevelUp_ItemGenerationSpeed(0.3f);
        }else if(p < 99.999f){
            dataManager.GetComponent<DataManager>().LevelUp_ItemNumberOfGenerationOnClick(1);
        }else if(p <= 100){
            dataManager.GetComponent<DataManager>().LevelUp_ItemNumberOfGenerationOnClick(10);
        }

        CreateNamakemono();
    }

    //ナマケモノを生成
    private void CreateNamakemono(){
        GameObject namakemono = (GameObject)Instantiate(namakemonoPrefab);
        namakemono.transform.SetParent(namakemonoArea.transform);
        namakemono.transform.localPosition = new Vector3(0,-600,0);
        namakemono.transform.localScale = new Vector3(scaleNamakemono,scaleNamakemono,1);

        
        Vector3 targetPos = new Vector3(
            UnityEngine.Random.Range(-450.0f, 450.0f),
            UnityEngine.Random.Range(300f, -600.0f),
            0f
        );
        namakemono.transform.DOLocalMove(
            targetPos,
            1
        );
    }

    
/* 
    //リセット
    public void Reset(){
        SaveData.Clear();
        SaveData.Save();

        Start();
    }
    */
}
