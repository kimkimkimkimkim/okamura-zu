using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class AppleData
{
    public DateTime createDateTime; //作成された日時
    public string strCreateDateTime; //作成された日時の
    public int remainingTime; //実るまでに必要な時間（秒）
    public int point; //収穫した時に取得できるポイント

    public AppleData(){
        createDateTime = DateTime.UtcNow;
        strCreateDateTime = createDateTime.ToBinary().ToString();
        remainingTime = 30;
        point = 10;
    }

    public AppleData(DateTime _createDateTime,int _remainingTime){
        createDateTime = _createDateTime;
        strCreateDateTime = createDateTime.ToBinary().ToString();
        remainingTime = _remainingTime;
    }

    //DateTime -> String
    public void ToStringFromDateTime(){
        strCreateDateTime = createDateTime.ToBinary().ToString();
    }

    //String -> DateTime
    public void ToDateTimeFromString(){
        createDateTime = DateTime.FromBinary(Convert.ToInt64(strCreateDateTime));
    }

    public DateTime GetCreateDateTime(){
        return DateTime.FromBinary(Convert.ToInt64(strCreateDateTime));
    }

}
