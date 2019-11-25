using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CannotSingleGachaDialogManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject btnBuy;
    public GameObject btnCloseDialog;

    //変数宣言
    private GameObject dialogManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogManager = GameObject.Find("DialogManager");
        InitialSetObserver();
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
