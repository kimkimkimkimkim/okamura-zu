using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDataManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject txtPossessedPointInGacha; //ガチャ画面の所持ポイントテキスト

    public void UpdatePossessedPoint(int point){
        txtPossessedPointInGacha.GetComponent<Text>().text = point.ToString();
    }
}
