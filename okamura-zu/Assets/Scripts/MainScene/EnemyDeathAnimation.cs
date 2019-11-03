using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnimation : MonoBehaviour
{       
    private GameObject mainSceneManager;

    void Start(){
        mainSceneManager = GameObject.Find("MainSceneManager");
    }

    public void EndAnimation(){
        this.GetComponent<Animator>().SetBool("isDefeat",false);
        mainSceneManager.GetComponent<MainSceneManager>().NextStage(1);
    }
}
