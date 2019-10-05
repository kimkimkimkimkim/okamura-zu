using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewItemOnClickManager : MonoBehaviour
{   
    private const float clickTime = 0.5f; //クリックと判定する時間
    private bool isPointerDown = false; //タップしているかどうか
    private float time = 0; //タップしている時間
    public GameObject dataManager;

    private void FixedUpdate(){
        if(!isPointerDown) return;
        time += Time.deltaTime;
    }

    public void OnPointerDown(){
        isPointerDown = true;
    }

    public void OnPointerUp(){
        isPointerDown = false;
        if(time >= clickTime){
            time = 0;
            return;
        }
        //クリックと判定したのでアイテム生成
        dataManager.GetComponent<DataManager>().CreateNewItemOnClick();
    }
}
