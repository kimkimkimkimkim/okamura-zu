using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using UnityEngine.UI;

public class HeaderFooterManager : MonoBehaviour
{   
    //オブジェクト参照
    public List<GameObject> footerButtons = new List<GameObject>();
    public GameObject navigationManager;
    public GameObject footerMenu;

    //変数宣言
    private float menuHeight = 727.7f;
    private GameObject isOpenMenuButton = null;

    // Start is called before the first frame update
    void Start()
    {
        InitialSetObserver();
        InitialSetActice();
    }

    void InitialSetObserver(){
        footerButtons[0].AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => OnPressFooterButton(footerButtons[0]));
        footerButtons[1].AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => OnPressFooterButton(footerButtons[1]));
        footerButtons[2].AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => OnPressFooterButton(footerButtons[2]));
    }

    void InitialSetActice(){
        footerMenu.SetActive(false);
    }

    void Move(int targetIndex){
        int index = navigationManager.GetComponent<NavigationManager>().index;
        if(index == targetIndex)return;

    }

    void OnPressFooterButton(GameObject footerBtn){
        if(isOpenMenuButton == footerBtn){
            //今開いてるメニューが押したフッターのメニューだったら閉じる
            CloseMenu();
            isOpenMenuButton = null;
        }else{
            //何も開いてないときは開く
            isOpenMenuButton = footerBtn;
            OpenMenu();
        }
    }

    void OpenMenu(){
        footerMenu.transform.localPosition = new Vector3(0,-menuHeight,0);
        footerMenu.SetActive(true);

        footerMenu.transform.DOLocalMove(
            new Vector3(0,100,0),
            0.3f
        );
    }

    void CloseMenu(){
        footerMenu.transform.DOLocalMove(
            new Vector3(0,-menuHeight,0),
            0.3f
        ).OnComplete(() => footerMenu.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
