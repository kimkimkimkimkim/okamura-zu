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
    public List<GameObject> footerButtonMenuList = new List<GameObject>();
    public GameObject navigationManager;
    public GameObject footerMenu;
    public GameObject txtTitle;
    public GameObject btnMenuResize;
    public GameObject btnMenuClose;

    //変数宣言
    private float defaultMenuHeight = 727.7f;
    private float menuHeight = 727.7f;
    private float wideMenuHeight = 1963;
    private int isOpenMenuButtonIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        InitialSetObserver();
        InitialSetActice();
    }

    void InitialSetObserver(){
        footerButtons[0].AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => OnPressFooterButton(0));
        footerButtons[1].AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => OnPressFooterButton(1));
        footerButtons[2].AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => OnPressFooterButton(2));

        btnMenuResize.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => ResizeMenu());
        btnMenuClose.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => CloseMenu());
    }

    void InitialSetActice(){
        footerMenu.SetActive(false);
    }

    void Move(int targetIndex){
        int index = navigationManager.GetComponent<NavigationManager>().index;
        if(index == targetIndex)return;

    }

    void OnPressFooterButton(int btnIndex){
        if(isOpenMenuButtonIndex == -1){
            //何も開いてないときは開く
            OpenMenu(btnIndex);
        }else if(isOpenMenuButtonIndex != btnIndex){
            //今押したフッターのメニューじゃないメニューが開いてたら、今押したフッターのメニューにスイッチ
            RenderMenu(btnIndex);
        }else{
            //今開いてるメニューが押したフッターのメニューだったら閉じる
            CloseMenu();
        }
    }

    void OpenMenu(int btnIndex){
        isOpenMenuButtonIndex = btnIndex;
        
        RenderMenu(isOpenMenuButtonIndex);
        footerMenu.transform.localPosition = new Vector3(0,-menuHeight,0);
        footerMenu.SetActive(true);

        footerMenu.transform.DOLocalMove(
            new Vector3(0,100,0),
            0.3f
        );
    }

    void CloseMenu(){
        isOpenMenuButtonIndex = -1;

        //全フッターアイコンを非表示
        foreach(GameObject fb in footerButtons){
            GameObject icon = fb.transform.Find("icon").gameObject;
            icon.GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
            icon.transform.GetChild(0).gameObject.SetActive(true);
        }

        footerMenu.transform.DOLocalMove(
            new Vector3(0,-menuHeight,0),
            0.3f
        ).OnComplete(() => {if(isOpenMenuButtonIndex==-1)footerMenu.SetActive(false);});
    }

    void ResizeMenu(){
        float beforeHeifht = menuHeight;

        if(menuHeight==defaultMenuHeight){
            //デフォルトなので大きくする
            Debug.Log("to wide:"+menuHeight.ToString());
            menuHeight = wideMenuHeight;
            btnMenuResize.transform.Find("icon").gameObject.GetComponent<RectTransform>().localRotation=Quaternion.Euler(0,0,180);
        }else{
            //大きいのでデフォルトのサイズに戻す
            Debug.Log("to default:"+menuHeight.ToString());
            menuHeight = defaultMenuHeight;
            btnMenuResize.transform.Find("icon").gameObject.GetComponent<RectTransform>().localRotation=Quaternion.Euler(0,0,0);
        }

        DOVirtual.Float(beforeHeifht,menuHeight,0.3f,value =>{
            footerMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0,value);
        });
    }

    void RenderMenu(int btnIndex){
        isOpenMenuButtonIndex = btnIndex;

        //全メニューを非表示
        foreach(GameObject menuView in footerButtonMenuList){
            menuView.SetActive(false);
        }
        foreach(GameObject fb in footerButtons){
            GameObject icon = fb.transform.Find("icon").gameObject;
            icon.GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
            icon.transform.GetChild(0).gameObject.SetActive(true);
        }

        //メニューのレンダー
        footerButtonMenuList[isOpenMenuButtonIndex].SetActive(true);
        //フッターアイコンの変更
        GameObject footerBtn = footerButtons[btnIndex].transform.Find("icon").gameObject;
        footerBtn.transform.GetChild(0).gameObject.SetActive(false);
        footerBtn.GetComponent<RectTransform>().localScale = new Vector3(1.5f,1.5f,1);

        //タイトルテキスト変更
        switch(isOpenMenuButtonIndex){
        case 0:
            txtTitle.GetComponent<Text>().text = "ナマケモノ一覧・強化";
            break;
        case 1:
            txtTitle.GetComponent<Text>().text = "怠け者一覧";
            break;
        case 2:
            txtTitle.GetComponent<Text>().text = "ガチャ一覧";
            break;
        default:
            break;
        }
    }
}
