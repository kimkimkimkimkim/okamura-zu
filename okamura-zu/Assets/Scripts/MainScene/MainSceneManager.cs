using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainSceneManager : MonoBehaviour
{    
    //オブジェクト参照
    public GameObject slider_enemyHp;
    public GameObject txt_enemyHp;
    public GameObject txt_stageName;
    public GameObject txt_stageFraction; //ステージの分数のところ
    public GameObject img_enemy; //敵画像
    public GameObject damageTextPrefab;

    //変数宣言
    [System.NonSerialized]
    public int playerAttackPower; //プレイヤーの攻撃力
    [System.NonSerialized]
    public int enemyHp; //敵のHP
    private string nowStageNum; //現在のステージ

    void Start(){
        GetSaveData();
        SetEnemy();
        UpdateEnemyHp();
        UpdateStage();
    }

    void GetSaveData(){
        //セーブデータの取得
        playerAttackPower = SaveData.GetInt(SaveDataKeys.playerAttackPower,InitialValues.PLAYER_ATTACK_POWER);
        nowStageNum = SaveData.GetString(SaveDataKeys.maxStageNum,InitialValues.MAX_STAGE_NUM);
    }

    void SetEnemy(){
        enemyHp = Random.Range(8, 12 + 1);
        if(nowStageNum.Split('-')[2]=="5")enemyHp=Random.Range(18, 22 + 1); //BOSS
        slider_enemyHp.GetComponent<Slider>().maxValue = enemyHp; //スライダーの最大値
    }

    //味方から敵への攻撃
    public void Attack(int attackPower){
        if(attackPower == 0) attackPower = playerAttackPower; //タップでの攻撃

        enemyHp = (enemyHp <= attackPower)? 0 : enemyHp-attackPower;
        UpdateEnemyHp();
        DamageEffect(attackPower);
    }

    private void DamageEffect(int attack){
        GameObject damageText = (GameObject)Instantiate(damageTextPrefab);
        damageText.transform.SetParent(img_enemy.transform);
        damageText.transform.localPosition = new Vector3(0,0,0);
        damageText.transform.localScale = new Vector3(1,1,1);
        damageText.GetComponent<Text>().text = attack.ToString();

        //移動
        damageText.transform.DOLocalMove(
            new Vector3(0,220,0),
            1
        );
        //フェードアウト
        DOTween.ToAlpha(
            ()=>damageText.GetComponent<Text>().color,
            color => damageText.GetComponent<Text>().color = color,
            0f,
            1f
        ).OnComplete(()=>Destroy(damageText));
    }

    //敵のHPの更新
    private void UpdateEnemyHp(){
        slider_enemyHp.GetComponent<Slider>().value = enemyHp;
        txt_enemyHp.GetComponent<Text>().text = "やる気 " + enemyHp + "%";

        if(enemyHp == 0)NextStage();
    }

    private void NextStage(){
        int area = int.Parse(nowStageNum.Split('-')[1]);
        int stage = int.Parse(nowStageNum.Split('-')[2]);
        if(stage==5){
            nowStageNum = "1-"+(area+1).ToString()+"-1";
        }else{
            nowStageNum = "1-"+area.ToString()+"-"+(stage+1).ToString();
        }

        SaveData.SetString(SaveDataKeys.maxStageNum,nowStageNum);
        SaveData.Save();

        SetEnemy();
        UpdateEnemyHp();
        UpdateStage();
    }

    //ステージの更新
    private void UpdateStage(){
        string prefecture = "東京都";
        txt_stageName.GetComponent<Text>().text = prefecture + " " + nowStageNum.Split('-')[1];
        txt_stageFraction.GetComponent<Text>().text = (int.Parse(nowStageNum.Split('-')[2])-1) + "/4";
        if(nowStageNum.Split('-')[2]=="5")txt_stageFraction.GetComponent<Text>().text = "BOSS";
    }
}
