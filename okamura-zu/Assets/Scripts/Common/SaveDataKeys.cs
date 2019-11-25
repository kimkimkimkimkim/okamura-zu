using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataKeys{

    //old
    static public string maxItemCount = "maxItemCount"; //アイテムの最大生成数
    static public string nowItemCount = "nowItemCount"; //現在のアイテム生成数
    static public string lastDateTime = "lastDateTime"; //最終アイテム生成時刻
    static public string gachaCost = "gachaCost"; //１回のガチャにかかるお金
    static public string itemGenerationSpeed = "itemGenerationSpeed"; //アイテム生成スピード
    static public string itemUnitPrice = "itemUnitPrice"; //アイテム単価
    static public string itemNumberOfGenerationOnClick = "itemNumberOfGenerationOnClick"; //アイテムのタップ時生成数
    
    //new
    static public string appleData = "appleData"; //りんごデータ : class
    static public string playerAttackPower = "playerAttackPower"; //プレイヤーの攻撃力 : int
    static public string maxStageNum = "maxStageNum"; //ステージの最高到達点 (ex: "1-1-2") : string
    static public string possessedPoint = "possessedPoint"; //所持ポイント : int
    static public string possessedNamakemonoList = "possessedNamakemonoList"; //所持ナマケモノリスト : List<NamakemonoData>
}

