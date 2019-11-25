using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public delegate void CloseDialogCallBack();

public enum DialogType {
    MonsterStrengthen,
    CannotSingleGacha,
    SingleGacha,
}

public class DialogManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject dialogBack;
    public GameObject dialogMonsterStrengthen;
    public GameObject dialogCannotSingleGacha;
    public GameObject dialogSingleGacha;

    //変数宣言
    private GameObject dialog;

    void Start(){
        InitialSetActive();
    }

    void InitialSetActive(){
        dialogBack.SetActive(false);
        // 子オブジェクトを全て取得する
        foreach (Transform childTransform in dialogBack.transform)
        {
            childTransform.gameObject.SetActive(false);
        }
    }

    public void OpenDialog(DialogType dt){

        switch(dt){
        case DialogType.MonsterStrengthen:
            dialog = dialogMonsterStrengthen;
            break;
        case DialogType.SingleGacha:
            dialog = dialogSingleGacha;
            break;
        case DialogType.CannotSingleGacha:
            dialog = dialogCannotSingleGacha;
            break;
        default:
            return;
        }

        dialogBack.SetActive(true);

        //ダイアログのアニメーション
        dialog.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
        dialog.SetActive(true);
        dialog.transform.DOScale(
            Vector3.one,
            0.5f
        ).SetEase(Ease.OutBack);
    }

    public void CloseDialog(CloseDialogCallBack c){

        //ダイアログのアニメーション
        dialog.transform
            .DOScale(Vector3.zero,0.4f)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                dialog.SetActive(false);
                dialogBack.SetActive(false);
                c();
            });
        ;
    }
    
}
