// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TextureSwap : MonoBehaviour
// {   
//     public GameObject CustomLightEstimation;  ///set this in the inspector
//     public Texture NewTexture;
//     private RawImage img;

//     // Start is called before the first frame update
//     void Start()
//     {
//         GameObject go = GameObject.Find ("CustomLightEstimation");
//         CameraImageExample CameraImageExample= go.GetComponent <CameraImageExample> ();
    
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         img = (RawImage)CameraImageExample.m_Texture;
//         img.texture = (Texture)NewTexture;
//     }
// }
