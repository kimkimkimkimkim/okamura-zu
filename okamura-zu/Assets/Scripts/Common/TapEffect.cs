using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffect : MonoBehaviour
{   
    public Camera _camera;
    public GameObject tapEffect;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 1);
            tapEffect.transform.position = pos;
            tapEffect.GetComponent<ParticleSystem>().Emit(1);
        }
    }
}
