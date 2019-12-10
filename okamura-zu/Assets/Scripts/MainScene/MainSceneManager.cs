using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using System;

public class MainSceneManager : MonoBehaviour
{  
    //エクセルデータ
    public BossData bossData;
    public EnemyData enemyData;

    //オブジェクト参照
    public GameObject dialogManager;
    public GameObject slider_enemyHp;
    public GameObject slider_time;
    public GameObject txt_enemyHp;
    public GameObject txt_enemyName;
    public GameObject txt_stageName; //?-?-0,1,2,3,4(BOSS)
    public GameObject txt_stageFraction; //ステージの分数のところ
    public GameObject img_enemy; //敵画像
    public GameObject enemyDeathAnimation;
    public GameObject damageTextPrefab;
    public GameObject btn_toBossBattle;
    public GameObject btn_monsterOrganization; //モンスター編成ボタン
    public GameObject txtAddApple;
    public GameObject updateDataManager;

    //変数宣言
    //[System.NonSerialized]
    //public int playerAttackPower; //プレイヤーの攻撃力
    [System.NonSerialized]
    public int enemyHp; //敵のHP
    //private int 
    //private int playerLevel; //プレイヤーのレベル
    private PlayerData player;
    private string nowStageNum; //現在のステージ
    private bool canAttack = true; //タップ攻撃可能かどうか
    private bool prepareBossBattle; //ボス戦準備中かどうか

    void Start(){
        InitialSetObserver();
        GetSaveData();
        InitialSetActive();
        SetEnemy();
        UpdateEnemyHp();
        UpdateStage();
    }

    void GetSaveData(){
        //セーブデータの取得
        //playerAttackPower = SaveData.GetInt(SaveDataKeys.playerAttackPower,InitialValues.PLAYER_ATTACK_POWER);
        //playerLevel = SaveData.GetInt(SaveDataKeys.playerLevel,InitialValues.PLAYER_LEVEL);
        player = SaveData.GetClass<PlayerData>(SaveDataKeys.player,InitialValues.PLAYER);
        nowStageNum = SaveData.GetString(SaveDataKeys.maxStageNum,InitialValues.MAX_STAGE_NUM);
        prepareBossBattle = SaveData.GetBool(SaveDataKeys.prepareBossBattle,InitialValues.PREPARE_BOSS_BATTLE);
    }

    void InitialSetActive(){
        slider_time.SetActive(false);
        if(!prepareBossBattle)btn_toBossBattle.SetActive(false);
        else btn_toBossBattle.SetActive(true);
        enemyDeathAnimation.SetActive(false);
        txtAddApple.SetActive(false);
    }

    void InitialSetObserver(){
        btn_toBossBattle.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                prepareBossBattle = false;
                SaveData.SetBool(SaveDataKeys.prepareBossBattle,prepareBossBattle);
                SaveData.Save();
                btn_toBossBattle.SetActive(false);
                NextStage(1);
            });

        btn_monsterOrganization.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                dialogManager.GetComponent<DialogManager>().OpenDialog(DialogType.MonsterStrengthen);
            });
    }


    //敵の初期設定
    void SetEnemy(){
        Sprite s;
        int enemyId;
        string enemyName;

        if(nowStageNum.Split('-')[2]=="5"){
            //BOSS
            int prefectureNum = int.Parse(nowStageNum.Split('-')[0]);
            int areaNum = int.Parse(nowStageNum.Split('-')[1]);
            //int stage;

            //enemyId = bossData.sheets[prefectureNum-1].list[areaNum-1].enemy_id;
            enemyId = 19;
            enemyHp = bossData.sheets[prefectureNum-1].list[areaNum-1].hp;
            slider_time.SetActive(true);
            s = Resources.Load<Sprite>("Image/Enemy/" + enemyId.ToString());
        }else{
            //通常
            int prefectureNum = int.Parse(nowStageNum.Split('-')[0]);
            int areaNum = int.Parse(nowStageNum.Split('-')[1]);
            int bossHp = bossData.sheets[prefectureNum-1].list[areaNum-1].hp;
            enemyHp = UnityEngine.Random.Range((int)(bossHp*0.6f), (int)(bossHp*0.8f));
            slider_time.SetActive(false);
            //enemyId = UnityEngine.Random.Range(1,18); //1~17の乱数(int)
            enemyId = 18;
            s = Resources.Load<Sprite>("Image/Enemy/"+enemyId.ToString());
        }
        slider_enemyHp.GetComponent<Slider>().maxValue = enemyHp; //スライダーの最大値
        img_enemy.GetComponent<Image>().sprite = s; //敵画像
        enemyName = enemyData.sheets[0].list[enemyId-1].name; //名前
        txt_enemyName.GetComponent<Text>().text = enemyName;
    }

    //ボス戦終了
    public void FinishBossBattle(){
        canAttack = false;
        if(enemyHp > 0){
            //BOSS戦失敗
            NextStage(-1);
            prepareBossBattle = true;
            SaveData.SetBool(SaveDataKeys.prepareBossBattle,prepareBossBattle);
            SaveData.Save();
            canAttack = true;
            btn_toBossBattle.SetActive(true);
        }
    }

    public void UpdatePlayer(PlayerData p){
        player = p;
    }

    //味方から敵への攻撃
    public void Attack(int attackPower = 0){
        if(!canAttack) return;
        if(attackPower == 0) {
            //プレイヤーの攻撃
            attackPower = player.GetAttackPower(); //タップでの攻撃
            DamageEffect(attackPower);
        }

        enemyHp = (enemyHp <= attackPower)? 0 : enemyHp-attackPower;
        UpdateEnemyHp();
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

        if(enemyHp == 0){
            EnemyDefeatAnimation();
            GetAppleWhenEnemyDefeat();
            //NextStage(1);
        }
    }

    //敵撃破時コイン取得
    private void GetAppleWhenEnemyDefeat(){
        //ゲットするりんごの量を決める
        int apple = UnityEngine.Random.Range(15,50);
        int possessedApple = SaveData.GetInt(SaveDataKeys.possessedPoint);
        possessedApple += apple;
        //データのUI反映
        updateDataManager.GetComponent<UpdateDataManager>().UpdatePossessedPoint(possessedApple);
        //データの保存
        SaveData.SetInt(SaveDataKeys.possessedPoint,possessedApple);
        SaveData.Save();
        //りんご取得アニメーション
        GetAppleAnimation(apple);
    }

    private void GetAppleAnimation(int apple){
        txtAddApple.GetComponent<Text>().text = "+ " + apple.ToString();
        txtAddApple.SetActive(true);

        Observable.Return(Unit.Default)
            .Delay(TimeSpan.FromMilliseconds(1000))
            .Subscribe(_ => txtAddApple.SetActive(false));

    }

    //敵撃破アニメーション
    private void EnemyDefeatAnimation(){
        canAttack = false;
        //img_enemy.GetComponent<Image>().color = new Color(255,255,255,0.5f);
        img_enemy.GetComponent<Image>()
            .DOFade(0.5f,0.5f)
            .SetEase(Ease.Linear);

        slider_time.SetActive(false);
        Observable.Return(Unit.Default)
            .Delay(TimeSpan.FromMilliseconds(300))
            .Subscribe(_ => {
                enemyDeathAnimation.SetActive(true);
                enemyDeathAnimation.GetComponent<Animator>().SetBool("isDefeat",true);
            });
    }

    public void NextStage(int plus){
        canAttack = true;
        enemyDeathAnimation.SetActive(false);
        img_enemy.GetComponent<Image>().color = new Color(255,255,255,1);

        if(!prepareBossBattle){
            //ボス戦準備中じゃないなら次のステージに行く
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
