using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStrengthenDialogListComponent : MonoBehaviour
{   
    //オブジェクト参照
    public GameObject imgIcon;
    public GameObject txtName;
    public GameObject txtLevel;
    public GameObject txtNextNeededApple;

    //画像などのセットアップを行う
    public void SetUp(NamakemonoData nd){
        imgIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Monster/" + nd.No.ToString());
        txtName.GetComponent<Text>().text = nd.name;
        txtLevel.GetComponent<Text>().text = nd.level.ToString();
        txtNextNeededApple.GetComponent<Text>().text = nd.GetNextNeededApple().ToString();
    }
}
