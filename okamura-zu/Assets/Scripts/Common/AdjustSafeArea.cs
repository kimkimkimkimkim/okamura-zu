using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSafeArea : MonoBehaviour
{   
    //オブジェクト参照
    public GameObject mainScreen;
    public GameObject collectionScreen;
    public GameObject gachaScreen;
    public GameObject header;
    public GameObject headerSafeArea;
    public GameObject footer;
    public GameObject footerSafeArea;
    // Start is called before the first frame update
    void Start()
    {   
        float top = MyConfig.safeAreaTop + MyConfig.headerHeight;
        float bottom = MyConfig.safeAreaBottom + MyConfig.footerHeight;

        mainScreen.GetComponent<RectTransform>().offsetMin = new Vector2 (0,bottom);
        mainScreen.GetComponent<RectTransform>().offsetMax = new Vector2 (0,-top);

        float width = mainScreen.GetComponent<RectTransform>().rect.width;

        collectionScreen.GetComponent<RectTransform>().offsetMin = new Vector2 (-width,bottom);
        collectionScreen.GetComponent<RectTransform>().offsetMax = new Vector2 (-width,-top);
        gachaScreen.GetComponent<RectTransform>().offsetMin = new Vector2 (width,bottom);
        gachaScreen.GetComponent<RectTransform>().offsetMax = new Vector2 (width,-top);

        headerSafeArea.GetComponent<RectTransform>().offsetMin = new Vector2(0,-MyConfig.safeAreaTop);
        headerSafeArea.GetComponent<RectTransform>().offsetMax = new Vector2(0,0);
        header.GetComponent<RectTransform>().offsetMin = new Vector2 (0,-top);
        header.GetComponent<RectTransform>().offsetMax = new Vector2 (0,-MyConfig.safeAreaTop);
        footer.GetComponent<RectTransform>().offsetMin = new Vector2 (0,MyConfig.safeAreaBottom);
        footer.GetComponent<RectTransform>().offsetMax = new Vector2 (0,bottom);
        footerSafeArea.GetComponent<RectTransform>().offsetMin = new Vector2(0,0);
        footerSafeArea.GetComponent<RectTransform>().offsetMax = new Vector2(0,MyConfig.safeAreaBottom);
    }
}
