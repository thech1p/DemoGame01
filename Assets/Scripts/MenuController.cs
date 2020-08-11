using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas mainCanvas;

    public Texture2D mainMenuBackground;

    public Texture2D mainMenuButton;

    private bool _testBool;


    void OnGUI()
    {
        /*
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mainMenuBackground);
        if (GUI.Button(new Rect(Screen.width/2-(Screen.width/20), Screen.height / 2-(Screen.height/20), Screen.width/10, Screen.height/10),
            "This is  a button"))
        {
            Debug.Log("That was a button");
        }
        */
    }
    
    void Start()
    {
        if (mainCanvas == null) { mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}