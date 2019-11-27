using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class MonsterStrengthenDialogListComponent : MonoBehaviour
{   
    //オブジェクト参照
    public GameObject imgIcon;
    public GameObject txtName;
    public GameObject txtLevel;
    public GameObject txtNextNeededApple;
    public GameObject txtAttack;
    public GameObject btnLevelUp;

    //変数宣言
    private NamakemonoData namakemonoData;
    private int neededApple;
    private GameObject updateDataManager;

    void Start(){
        InitialSetObserver();
        updateDataManager = GameObject.Find("UpdateDataManager");
    }

    void InitialSetObserver(){
        btnLevelUp.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => OnPressLevelUpBtn());
    }

    void OnPressLevelUpBtn(){
        Debug.Log("levelup!");
        int apple = SaveData.GetInt(SaveDataKeys.possessedPoint);
        if(apple < namakemonoData.GetNextNeededApple()){
            //所持金が足りない
            return;
        }

        //お金消費
        apple -= namakemonoData.GetNextNeededApple();

        //レベルあげる
        List<NamakemonoData> ndList = SaveData.GetList(SaveDataKeys.possessedNamakemonoList,InitialValues.POSSESSED_NAMAKEMONO_LIST);
        foreach(NamakemonoData nd in ndList){
            if(nd.No == namakemonoData.No){
                nd.level = nd.level + 1;
                namakemonoData = nd;
            }
        }

        //保存
        SaveData.SetInt(SaveDataKeys.possessedPoint,apple);
        SaveData.SetList(SaveDataKeys.possessedNamakemonoList,ndList);
        SaveData.Save();

        //更新
        SetUp(namakemonoData);
        updateDataManager.GetComponent<UpdateDataManager>().UpdatePossessedNamakemonoList(ndList);
        updateDataManager.GetComponent<UpdateDataManager>().UpdatePossessedPoint(apple);

    }

    //画像などのセットアップを行う
    public void SetUp(NamakemonoData nd){
        namakemonoData = nd;
        imgIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Monster/" + nd.No.ToString());
        txtName.GetComponent<Text>().text = nd.name;
        txtLevel.GetComponent<Text>().text = nd.level.ToString();
        txtNextNeededApple.GetComponent<Text>().text = nd.GetNextNeededApple().ToString();
        txtAttack.GetComponent<Text>().text = "ATK:"+nd.GetAttackPower().ToString();
    }
}
