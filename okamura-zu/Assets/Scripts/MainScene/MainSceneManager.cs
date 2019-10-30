using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

public class MainSceneManager : MonoBehaviour
{    
    //オブジェクト参照
    public GameObject slider_enemyHp;
    public GameObject slider_time;
    public GameObject txt_enemyHp;
    public GameObject txt_stageName; //?-?-0,1,2,3,4(BOSS)
    public GameObject txt_stageFraction; //ステージの分数のところ
    public GameObject img_enemy; //敵画像
    public GameObject damageTextPrefab;
    public GameObject btn_toBossBattle;

    //変数宣言
    [System.NonSerialized]
    public int playerAttackPower; //プレイヤーの攻撃力
    [System.NonSerialized]
    public int enemyHp; //敵のHP
    private string nowStageNum; //現在のステージ
    private bool canAttack = true; //タップ攻撃可能かどうか
    private bool prepareBossBattle = false; //ボス戦準備中かどうか

    void Start(){
        InitialSetActive();
        InitialSetObserver();
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

    void InitialSetActive(){
        slider_time.SetActive(false);
        btn_toBossBattle.SetActive(false);
    }

    void InitialSetObserver(){
        btn_toBossBattle.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                prepareBossBattle = false;
                btn_toBossBattle.SetActive(false);
                NextStage(1);
            });
    }


    //敵の初期設定
    void SetEnemy(){
        enemyHp = Random.Range(8, 12 + 1);
        slider_time.SetActive(false);
        if(nowStageNum.Split('-')[2]=="5"){
            enemyHp=Random.Range(18, 22 + 1); //BOSS
            slider_time.SetActive(true);
        }
        slider_enemyHp.GetComponent<Slider>().maxValue = enemyHp; //スライダーの最大値
    }

    //ボス戦終了
    public void FinishBossBattle(){
        canAttack = false;
        if(enemyHp > 0){
            //BOSS戦失敗
            NextStage(-1);
            prepareBossBattle = true;
            canAttack = true;
            btn_toBossBattle.SetActive(true);
        }
    }

    //味方から敵への攻撃
    public void Attack(int attackPower){
        if(!canAttack) return;
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

        if(enemyHp == 0)NextStage(1);
    }

    private void NextStage(int plus){
        if(!prepareBossBattle){
            //ボス戦準備中なら次のステージに行かない
            int area = int.Parse(nowStageNum.Split('-')[1]);
            int stage = int.Parse(nowStageNum.Split('-')[2]);
            if(stage==5){
                if(plus > 0){
                    //正
                    nowStageNum = "1-"+(area+1).ToString()+"-1";
                }else{
                    //負
                    nowStageNum = "1-"+area.ToString()+"-"+(stage+plus).ToString();
                }
            }else{
                nowStageNum = "1-"+area.ToString()+"-"+(stage+plus).ToString();
            }

            SaveData.SetString(SaveDataKeys.maxStageNum,nowStageNum);
            SaveData.Save();
        }

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
