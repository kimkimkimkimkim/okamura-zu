using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialValues{

    //old
    static public int GACHA_COST = 10; //１回のガチャにかかるお金
    static public float ITEM_GENERATION_SPEED = 5; //コインの生成スピード [個]/[s]
    static public int ITEM_UNITE_PRICE = 1; //コインの単価 [円]/[個]
    static public int MAX_ITEM_COUNT = 10; //コインの最大数 [個]
    static public int ITEM_NUMBER_OF_GENERATION_ON_CLICK = 1; //タップ時のコイン生成数

    //new
    static public int PLAYER_ATTACK_POWER = 1; //プレイヤーの攻撃力
    static public string MAX_STAGE_NUM = "1-1-1"; //最高到達ステージ
    static public List<NamakemonoData> POSSESSED_NAMAKEMONO_LIST = new List<NamakemonoData>();
    static public PlayerData PLAYER = new PlayerData(); //プレイヤクラス
}

