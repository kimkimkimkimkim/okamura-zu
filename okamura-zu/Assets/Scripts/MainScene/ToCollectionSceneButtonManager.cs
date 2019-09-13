using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToCollectionSceneButtonManager : MonoBehaviour
{
    public void OnClick(){
        FadeManager.Instance.LoadScene ("CollectionScene", 0.2f);
    }
}
