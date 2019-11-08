using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using UnityEngine.UI;

public class HeaderFooterManager : MonoBehaviour
{   
    //オブジェクト参照
    public List<GameObject> footerButtons = new List<GameObject>();
    public GameObject navigationManager;

    // Start is called before the first frame update
    void Start()
    {
        InitialSetObserver();
    }

    void InitialSetObserver(){
        footerButtons[0].AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => navigationManager.GetComponent<NavigationManager>().ToMoveIndex(-1));
        footerButtons[1].AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => navigationManager.GetComponent<NavigationManager>().ToMoveIndex(0));
        footerButtons[2].AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => navigationManager.GetComponent<NavigationManager>().ToMoveIndex(1));
    }

    void Move(int targetIndex){
        int index = navigationManager.GetComponent<NavigationManager>().index;
        if(index == targetIndex)return;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
