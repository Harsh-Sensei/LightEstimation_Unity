using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleHiding : MonoBehaviour
{
    public GameObject btn1;
    public GameObject btn2;
    public GameObject txt;
    public GameObject img;
    private bool currState = true;

    public void hideObjects()
    {
        currState = !currState;
        btn1.SetActive(currState);
        btn2.SetActive(currState);
        img.SetActive(currState);
        txt.SetActive(currState);
    }
}
