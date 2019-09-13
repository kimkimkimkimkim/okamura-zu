using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject txtPossessedPoint; //所持ポイントテキスト

    //変数宣言
    public int possessedPoint = 0; //所持ポイント

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
}
