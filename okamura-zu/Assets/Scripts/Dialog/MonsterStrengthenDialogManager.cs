using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Runtime.Serialization.Json;

public class MonsterStrengthenDialogManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject btnCloseDialog;
    public GameObject contentContainer;
    public GameObject listComponentPrefab;

    //変数宣言
    private GameObject dialogManager;
    private List<NamakemonoData> possessedNamakemonoList = new List<NamakemonoData>();

    // Start is called before the first frame update
    void Start()
    {
        dialogManager = GameObject.Find("DialogManager");
        InitialSetObserver();
    }

    void OnEnable(){
        //スクロールビューの子オブジェクトを全て削除する
        foreach (Transform childTransform in contentContainer.transform)
        {
            GameObject.Destroy(childTransform.gameObject);
        }

        //所持モンスター取得
        possessedNamakemonoList = SaveData.GetList<NamakemonoData>(SaveDataKeys.possessedNamakemonoList,InitialValues.POSSESSED_NAMAKEMONO_LIST);
        //所持モンスターリスト表示
        foreach(NamakemonoData nd in possessedNamakemonoList){
            GameObject listComponent = (GameObject)Instantiate(listComponentPrefab);
            listComponent.transform.SetParent(contentContainer.transform);
            listComponent.transform.localScale = new Vector3(1,1,1);
            listComponent.GetComponent<MonsterStrengthenDialogListComponent>().SetUp(nd);
        }
    }

    void InitialSetObserver(){

        //ダイアログを閉じる
        btnCloseDialog.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                dialogManager.GetComponent<DialogManager>().CloseDialog(()=>{});
            });
    }
    

}
