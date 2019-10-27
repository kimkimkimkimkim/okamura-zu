using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using UnityEngine.UI;

public class GachaSceneManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject btn_displaySingleGachaPopup;
    public GameObject popupView;
    public GameObject popup;
    public GameObject btn_gacha;
    public GameObject fadeView;
    public GameObject gachaView;
    public GameObject navigationManager;
    public GameObject img_gachaItem;
    public GameObject txt_tap;
    public GameObject explanationView;

    //変数宣言
    private const float TIME_FADE = 1.5f;
    private bool canGoDetail = false;
    private Vector3 iniPos_gachaItem;

    void Start(){
        iniPos_gachaItem = img_gachaItem.transform.localPosition;
        InitialSetActive();
        InitialSetObserver();

    }

    private void InitialSetActive(){
        popupView.SetActive(false);
        gachaView.SetActive(false);
        fadeView.SetActive(false);
        explanationView.SetActive(false);
    }
    
    
    private void InitialSetObserver(){
        btn_displaySingleGachaPopup.GetComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(pointerEventData => popupView.SetActive(true));

        popupView.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => popupView.SetActive(false));

        popup.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => {});

        btn_gacha.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => DisplayGachaView());

        gachaView.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => { 
                if(!canGoDetail){
                    txt_tap.SetActive(false);
                    img_gachaItem.transform
                        .DOLocalMoveY(-512,1.5f)
                        .SetEase(Ease.InCirc)
                        .OnComplete(()=>{ 
                            canGoDetail = true;
                        });
                }else{
                    explanationView.SetActive(true);
                }   
            });
        
        explanationView.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => BackGachaDialog());
        
    }

    private void DisplayGachaView(){
        navigationManager.GetComponent<NavigationManager>().canMove = false;
        popup.SetActive(false);

        fadeView.SetActive(true);
        fadeView.GetComponent<Image>()
            .DOFade(1,TIME_FADE)
            .OnComplete(() => {
                gachaView.SetActive(true);
                fadeView.GetComponent<Image>().DOFade(0,TIME_FADE).OnComplete(()=>fadeView.SetActive(false));
            });
    }

    private void BackGachaDialog(){
        navigationManager.GetComponent<NavigationManager>().canMove = true;
        canGoDetail = false;

        fadeView.SetActive(true);
        fadeView.GetComponent<Image>()
            .DOFade(1,TIME_FADE)
            .OnComplete(() => {
                gachaView.SetActive(false);
                popup.SetActive(true);
                explanationView.SetActive(false);
                txt_tap.SetActive(true);
                img_gachaItem.transform.localPosition = iniPos_gachaItem;
                fadeView.GetComponent<Image>().DOFade(0,TIME_FADE).OnComplete(()=>fadeView.SetActive(false));
            });

    }
}
