using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class AppleData
{
    public DateTime createDateTime; //作成された日時
    public int remainingTime; //実るまでに必要な時間（秒）

    public AppleData(){
        createDateTime = DateTime.UtcNow;
        remainingTime = 30;
    }

    public AppleData(DateTime _createDateTime,int _remainingTime){
        createDateTime = _createDateTime;
        remainingTime = _remainingTime;
    }

}
