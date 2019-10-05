using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationButton : MonoBehaviour
{   
    private GameObject navigationManager;

    private void Start(){
        navigationManager = GameObject.Find("NavigationManager");
    }

    public void MoveRight(){
        navigationManager.GetComponent<NavigationManager>().MoveRight();
    }

    public void MoveLeft(){
        navigationManager.GetComponent<NavigationManager>().MoveLeft();
    }
}
