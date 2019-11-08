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
    public GameObject footer;
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

        header.GetComponent<RectTransform>().offsetMin = new Vector2 (0,-top);
        header.GetComponent<RectTransform>().offsetMax = new Vector2 (0,0);
        footer.GetComponent<RectTransform>().offsetMin = new Vector2 (0,0);
        footer.GetComponent<RectTransform>().offsetMax = new Vector2 (0,bottom);
    }
}
