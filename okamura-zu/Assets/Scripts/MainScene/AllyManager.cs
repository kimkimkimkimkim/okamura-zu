using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyManager : MonoBehaviour
{
    //オブジェクト参照

    //変数宣言
    private GameObject mainSceneManager;
    private int attackPower = 1;
    //private float attackRate = 1f; //一秒間に何回攻撃するか
    private float attackInterval = 1f; //攻撃と攻撃の間隔[s]
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        mainSceneManager = GameObject.Find("MainSceneManager");
    }

    void FixedUpdate(){
        time += Time.deltaTime;
        if(time>=attackInterval){
            //攻撃
            mainSceneManager.GetComponent<MainSceneManager>().Attack(attackPower);
            time = 0;
        }
    }

}
