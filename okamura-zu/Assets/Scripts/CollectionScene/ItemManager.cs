using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{   
    private GameObject collectionScreenManager;

    void Start(){
        collectionScreenManager = GameObject.Find("CollectionScreenManager");
    }

    public void TouchItem(){
        if(Input.GetMouseButton(0) == false) return;

        collectionScreenManager.GetComponent<CollectionSceneManager>().GetItem();
        Destroy(this.gameObject);
    }

    public void ClickItem(){
        collectionScreenManager.GetComponent<CollectionSceneManager>().GetItem();
        Destroy(this.gameObject);
    }
}
