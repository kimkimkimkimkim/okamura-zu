using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AllyManager : MonoBehaviour
{
    //オブジェクト参照

    //変数宣言
    private GameObject mainSceneManager;
    private int attackPower = 1;
    //private float attackRate = 1f; //一秒間に何回攻撃するか
    private float attackInterval = 1.5f; //攻撃と攻撃の間隔[s]
    private float time = 0;
    private GameObject attackEffect;
    private Vector3 attackEffectDefaultPosition;
    private GameObject attackEffectDestination;
    private Vector3 attackEffectDestinationPosition;

    // Start is called before the first frame update
    void Start()
    {
        time = UnityEngine.Random.Range(0,attackInterval);
        mainSceneManager = GameObject.Find("MainSceneManager");
        attackEffect = this.transform.GetChild(0).gameObject;
        attackEffect.GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value, 1.0f);
        attackEffectDefaultPosition = attackEffect.transform.localPosition;
        attackEffectDestination = this.transform.parent.GetChild(1).gameObject;
        SetAttackEffectDestinationPosition();
    }

    //設定
    public void Setting(NamakemonoData nd){
        //画像の設定
        Sprite s = Resources.Load<Sprite>("Image/Monster/" + nd.No.ToString());
        this.GetComponent<Image>().sprite = s;
        //攻撃力
        attackPower = nd.GetAttackPower();
    }

    void SetAttackEffectDestinationPosition(){
        attackEffectDestinationPosition = attackEffectDestination.transform.localPosition;
        if(this.transform.localRotation.y==1)attackEffectDestinationPosition = new Vector3(
            -attackEffectDestinationPosition.x,
            attackEffectDestinationPosition.y,
            attackEffectDestinationPosition.z
        );
    }

    void FixedUpdate(){
        time += Time.deltaTime;
        if(time>=attackInterval){
            //攻撃
            StartAttackAnimation();
            time = 0;
        }
    }

    void StartAttackAnimation(){
        attackEffect.SetActive(true);
        attackEffect.transform
        .DOLocalMove(
            attackEffectDestinationPosition,
            1)
        .OnComplete(()=>{
            attackEffect.transform.localPosition = attackEffectDefaultPosition;
            attackEffect.SetActive(false);
            CalculateDamage();
        });
    }

    void CalculateDamage(){
        mainSceneManager.GetComponent<MainSceneManager>().Attack(attackPower);
    }

}
