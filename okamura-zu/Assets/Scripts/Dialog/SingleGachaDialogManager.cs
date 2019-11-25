using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class SingleGachaDialogManager : MonoBehaviour
{   

    //オブジェクト参照
    public GameObject btnSingleGacha;
    public GameObject btnCloseDialog;

    //変数宣言
    private GameObject dialogManager;
    private GameObject gachaSceneManager;

    // Start is called before the first frame update
    void Start()
    {   
        dialogManager = GameObject.Find("DialogManager");
        gachaSceneManager = GameObject.Find("GachaSceneManager");
        InitialSetObserver();
    }

    void InitialSetObserver(){

        //シングルガチャボタン
        btnSingleGacha.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                dialogManager.GetComponent<DialogManager>().CloseDialog(()=>{
                    gachaSceneManager.GetComponent<GachaSceneManager>().DisplayGachaView();
                });
            });

        //ダイアログを閉じる
        btnCloseDialog.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                dialogManager.GetComponent<DialogManager>().CloseDialog(()=>{});
            });
    }
    
}
