using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public int level = 1; //レベル
    public string name = "プレイヤー"; //名前
    //public int plus = 0; //プラス値

    public PlayerData(){
        
    }

    public PlayerData(int level){
        this.level = level;
    }

    //攻撃力を返す
    public int GetAttackPower(){
        return (int)((double)10 * Math.Exp(0.0769 * this.level));
    }

    //次のレベルになるのに必要なりんごの数
    public int GetNextNeededApple(){
        return (int)((double)30 * Math.Exp(0.0769 * this.level));
    }

}
