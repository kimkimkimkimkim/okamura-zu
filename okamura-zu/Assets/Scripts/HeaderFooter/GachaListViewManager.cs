using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GachaListViewManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject headerFooterManager;
    public GameObject normalGachaListComponent;

    // Start is called before the first frame update
    void Start()
    {
        InitialSetObserver();
    }

    void InitialSetObserver(){
        normalGachaListComponent.AddComponent<ObservableEventTrigger>()
            .OnPointerClickAsObservable()
            .Subscribe(_ => ToGachaDetailView());
    }

    void ToGachaDetailView(){
        headerFooterManager.GetComponent<HeaderFooterManager>().RenderGachaDetailView();
    }
}
