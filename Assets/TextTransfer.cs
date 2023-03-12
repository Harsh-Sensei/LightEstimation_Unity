using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTransfer : MonoBehaviour
{
    private string FinalOutput;
    

    public GameObject textDisplay;

    public void displayresult()
    {   
        
        //LinearLightEstimation LinearLightEstimation= go.GetComponent <LinearLightEstimation> ();
        //FinalOutput = LinearLightEstimation.FinalOutput;
        GameObject go = GameObject.Find ("CustomLightEstimation");
        CameraImageExample CameraImageExample= go.GetComponent <CameraImageExample> ();
        FinalOutput = CameraImageExample.FinalOutput;
        // GameObject go = GameObject.Find ("marble_bust_01_4k");
        // InterpolateTextures InterpolateTextures= go.GetComponent <InterpolateTextures> ();
        // FinalOutput = InterpolateTextures.FinalOutput;
        textDisplay.GetComponent<Text>().text = FinalOutput;
    }
}
