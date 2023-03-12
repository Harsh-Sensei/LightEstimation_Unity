using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTransfer : MonoBehaviour
{
    public GameObject RawImage;
    private Texture2D Texture;
    // Update is called once per frame
    public void changeimage ()
    {
        GameObject go = GameObject.Find ("CustomLightEstimation");
        //LinearLightEstimation LinearLightEstimation= go.GetComponent <LinearLightEstimation> ();
        //FinalOutput = LinearLightEstimation.FinalOutput;

        CameraImageExample CameraImageExample= go.GetComponent <CameraImageExample> ();
        Texture = CameraImageExample.targetbox;
        RawImage.GetComponent<RawImage>().texture = Texture;
    }
}
