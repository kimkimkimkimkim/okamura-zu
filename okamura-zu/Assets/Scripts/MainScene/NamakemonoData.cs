using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NamakemonoData
{
    public int No; //図鑑番号
    public int level = 1; //レベル
    public string name; //名前
    public int plus = 0; //プラス値

    public NamakemonoData(){
        
    }

    public NamakemonoData(int No,string name){
        this.No = No;
        this.name = name;
    }

    //攻撃力を返す
    public int GetAttackPower(){
        return (int)((double)30 * Math.Exp(0.0769 * this.level));
    }

    //次のレベルになるのに必要なりんごの数
    public int GetNextNeededApple(){
        return (int)((double)30 * Math.Exp(0.0769 * this.level));
    }

}
