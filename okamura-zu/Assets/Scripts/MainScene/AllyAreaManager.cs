using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyAreaManager : MonoBehaviour
{   
    private List<NamakemonoData> possessedNamakemonoList = new List<NamakemonoData>();
    // Start is called before the first frame update
    void Start()
    {      
        //SaveData.Remove(SaveDataKeys.possessedNamakemonoList);
        possessedNamakemonoList = SaveData.GetList(SaveDataKeys.possessedNamakemonoList,InitialValues.POSSESSED_NAMAKEMONO_LIST);
        SetAlly(possessedNamakemonoList);
    }

    public void SetAlly(List<NamakemonoData> ndList){
        Hide();

        //表示
        for(int i=0;i<ndList.Count;i++){
            GameObject ally = this.transform.GetChild(i).GetChild(0).gameObject;
            ally.SetActive(true);
            ally.GetComponent<AllyManager>().Setting(ndList[i]);
        }
    }

    private void Hide(){
        //allyを全非表示
        foreach (Transform childTransform in this.transform)
        {
            childTransform.gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
            childTransform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
