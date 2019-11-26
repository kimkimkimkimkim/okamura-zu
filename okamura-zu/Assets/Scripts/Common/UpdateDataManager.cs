using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDataManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject txtPossessedPointInGacha; //ガチャ画面の所持ポイントテキスト
    public GameObject allyAreaManager;

    public void UpdatePossessedPoint(int point){
        txtPossessedPointInGacha.GetComponent<Text>().text = point.ToString();
    }

    public void UpdatePossessedNamakemonoList(List<NamakemonoData> ndList){
        allyAreaManager.GetComponent<AllyAreaManager>().SetAlly(ndList);
    }
}
