using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class DialogManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject dialogBack;
    public GameObject dialog;
    public GameObject btn_close;
    public GameObject btn_confirm;

    void Start(){
        InitialSetActive();
        InitialSetObserver();
    }

    void InitialSetObserver(){

        btn_close.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                CloseDialog();
            });

        btn_confirm.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                CloseDialog();
            });
    }

    void InitialSetActive(){
        dialogBack.SetActive(false);
        dialog.SetActive(false);
    }

    public void OpenDialog(){
        dialogBack.SetActive(true);

        //ダイアログのアニメーション
        dialog.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
        dialog.SetActive(true);
        dialog.transform.DOScale(
            Vector3.one,
            0.5f
        ).SetEase(Ease.OutBack);
    }

    public void CloseDialog(){
        //ダイアログのアニメーション
        dialog.transform
            .DOScale(Vector3.zero,0.4f)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                dialog.SetActive(false);
                dialogBack.SetActive(false);
            });
        ;
    }
    
}
