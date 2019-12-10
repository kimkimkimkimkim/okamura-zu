using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Linq;

public class GachaSceneManager : MonoBehaviour
{
    //オブジェクト参照
    public NamakemonoExcelData namakemonoExcelData;
    public GameObject btn_displaySingleGachaPopup;
    //public GameObject popupView;
    //public GameObject popup;
    public GameObject btn_gacha;
    public GameObject fadeView;
    public GameObject gachaView;
    public GameObject navigationManager;
    public GameObject img_gachaItem;
    public GameObject imgGachaItemInExplanation;
    public GameObject txt_tap;
    public GameObject explanationView;
    public GameObject txtPossessedPoint;
    public List<Sprite> gachaItemImageList = new List<Sprite>();

    //変数宣言
    private GameObject updateDataManager;
    private GameObject dialogManager;
    private const float TIME_FADE = 1.5f;
    private bool canGoDetail = false;
    private Vector3 iniPos_gachaItem;
    private int gachaCost = 100;
    private int allMonsterCount = 2; //モンスターの総数

    void Start(){
        //SaveData.Remove(SaveDataKeys.possessedNamakemonoList);
        updateDataManager = GameObject.Find("UpdateDataManager");
        dialogManager = GameObject.Find("DialogManager");
        iniPos_gachaItem = img_gachaItem.transform.localPosition;
        InitialSetData();
        InitialSetActive();
        InitialSetObserver();
    }

    private void InitialSetData(){
        txtPossessedPoint.GetComponent<Text>().text = SaveData.GetInt(SaveDataKeys.possessedPoint).ToString();
    }

    private void InitialSetActive(){
        //popupView.SetActive(false);
        gachaView.SetActive(false);
        fadeView.SetActive(false);
        explanationView.SetActive(false);
    }
    
    
    private void InitialSetObserver(){
        btn_displaySingleGachaPopup.GetComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => OnClickGachaBtn());

        btn_gacha.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => OnClickGachaBtn());

        gachaView.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => { 
                if(!canGoDetail){
                    //ガチャアイテムが落ちてくる
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

    private void OnClickGachaBtn(){
        int possessedPoint = SaveData.GetInt(SaveDataKeys.possessedPoint);
        if(possessedPoint < gachaCost){
            //所持金が足りないので所持金足りないよダイアログをだす
            dialogManager.GetComponent<DialogManager>().OpenDialog(DialogType.CannotSingleGacha);
        }else{
            //シングルガチャダイアログをだす
            dialogManager.GetComponent<DialogManager>().OpenDialog(DialogType.SingleGacha);
            //DisplayGachaView();

        }
    }

    //ガチャで出るアイテムを決める
    /*
        レア度アップ : "rare"
        速度アップ : "speed"
        モンスター : "1" or "2" or ... (モンスターはモンスター番号)
    */
    private string DecideGachaItem(){
        int r = UnityEngine.Random.Range(3,4); //1~3の整数
        if(r==1){
            return "rare";
        }else if(r==2){
            return "speed";
        }else{
            return UnityEngine.Random.Range(1,allMonsterCount + 1).ToString();
        }
    }

    //ガチャアイテムを画像に反映
    private void UpdateGachaItemImage(string gachaItem){
        Sprite s;
        //Resources.Load<Sprite>("Image/Enemy/" + enemyId.ToString());
        if(gachaItem=="rare"){
            s = gachaItemImageList[0];
        }else if(gachaItem=="speed"){
            s = gachaItemImageList[1];
        }else{
            s = Resources.Load<Sprite>("Image/Monster/" + gachaItem.ToString());
        }

        img_gachaItem.GetComponent<Image>().sprite = s;
        imgGachaItemInExplanation.GetComponent<Image>().sprite = s;
    }

    //ガチャで取得したアイテムを保存
    private void UpdateGachaItemData(string gachaItem){
        //SaveData.Remove(SaveDataKeys.possessedNamakemonoList);
        if(gachaItem=="rare"){

        }else if(gachaItem=="speed"){
            
        }else{
            //モンスター
            List<NamakemonoData> possessedNamakemonoList 
                = SaveData.GetList<NamakemonoData>(SaveDataKeys.possessedNamakemonoList,InitialValues.POSSESSED_NAMAKEMONO_LIST);
            if(possessedNamakemonoList.Any(nd => nd.No == int.Parse(gachaItem))){
                //ゲットしたナマケモノを持っていればプラス値増加
                for(int i=0;i<possessedNamakemonoList.Count;i++){
                    if(possessedNamakemonoList[i].No == int.Parse(gachaItem)){
                        possessedNamakemonoList[i].plus = possessedNamakemonoList[i].plus + 1;
                    }
                }
            }else{
                //ゲットしたナマケモノを持っていなければリストに追加し保存
                string name = namakemonoExcelData.sheets[0].list[int.Parse(gachaItem)-1].Name;
                NamakemonoData nd = new NamakemonoData(int.Parse(gachaItem),name);
                possessedNamakemonoList.Add(nd);
                
            }
            //ナマケモノリストを保存
            SaveData.SetList<NamakemonoData>(SaveDataKeys.possessedNamakemonoList,possessedNamakemonoList);
            SaveData.Save();
            //新しいナマケモノリストを反映
            GameObject udMane = GameObject.Find("UpdateDataManager");
            udMane.GetComponent<UpdateDataManager>().UpdatePossessedNamakemonoList(possessedNamakemonoList);
        }
    }

    //ガチャ処理はここで全部やる
    public void DisplayGachaView(){

        //りんご消費
        /*
        int possessedPoint = SaveData.GetInt(SaveDataKeys.possessedPoint);
        possessedPoint -= gachaCost;
        SaveData.SetInt(SaveDataKeys.possessedPoint,possessedPoint);
        SaveData.Save();
        updateDataManager.GetComponent<UpdateDataManager>().UpdatePossessedPoint(possessedPoint);
*/
        //navigationManager.GetComponent<NavigationManager>().canMove = false;
        //popup.SetActive(false);
        //出るアイテムを決定し画像に反映
        string gachaItem = DecideGachaItem();
        UpdateGachaItemImage(gachaItem);
        UpdateGachaItemData(gachaItem);

        //ガチャ画面表示
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
                //popup.SetActive(true);
                explanationView.SetActive(false);
                txt_tap.SetActive(true);
                img_gachaItem.transform.localPosition = iniPos_gachaItem;
                fadeView.GetComponent<Image>().DOFade(0,TIME_FADE).OnComplete(()=>fadeView.SetActive(false));
            });

    }
}
