using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonManager : MonoBehaviour
{   
    //変数宣言
    private GameObject collectionSceneManager;

    void Start(){
        collectionSceneManager = GameObject.Find("CollectionScreenManager");
    }
    public void OnClick(){
        //collectionSceneManager.GetComponent<CollectionSceneManager>().UpdateItemCountText();
        FadeManager.Instance.LoadScene ("MainScene", 0.2f);
    }
}
