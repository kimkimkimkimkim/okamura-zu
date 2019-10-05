using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{   
    private GameObject dataManager;

    void Start(){
        dataManager = GameObject.Find("DataManager");
    }

    public void TouchItem(){
        if(Input.GetMouseButton(0) == false) return;

        dataManager.GetComponent<DataManager>().GetItem();
        Destroy(this.gameObject);
    }

    public void ClickItem(){
        dataManager.GetComponent<DataManager>().GetItem();
        Destroy(this.gameObject);
    }
}
