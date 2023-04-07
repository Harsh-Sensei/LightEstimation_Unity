using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARcoreLightEstimation : MonoBehaviour
{

    public GameObject frameText;
    private ARCameraManager m_CameraManager;
    private int frame_num;

    private void Awake()
    {
        m_CameraManager = GetComponent<ARCameraManager>();
        frame_num = 0;
    }

    private void OnEnable()
    {
        m_CameraManager.frameReceived += OnFrameReceived;
    }

    private void OnDisable()
    {
        m_CameraManager.frameReceived -= OnFrameReceived;
    }

    private void OnFrameReceived(ARCameraFrameEventArgs args)
    {
        frame_num += 1;
        Debug.Log("Receiving frame : "  + frame_num);
        string s = "Frame Number : ";
        frameText.GetComponent<Text>().text = s + frame_num;
        SphericalHarmonicsL2? tmp_sh_check = args.lightEstimation.ambientSphericalHarmonics ;
        if(tmp_sh_check is null)
        {
            Debug.Log("Null value of spherical harmonics");
        }
        else
        {
            SphericalHarmonicsL2 tmp_sh = tmp_sh_check.Value;
            string s1 = "coeff 0, r=" + tmp_sh[0,0] +", g=" + tmp_sh[1,0] + ", b=" + tmp_sh[2,0];
            string s2 = "coeff 1, r=" + tmp_sh[0,1] +", g=" + tmp_sh[1,1] + ", b=" + tmp_sh[2,1];
            string s3 = "coeff 2, r=" + tmp_sh[0,2] +", g=" + tmp_sh[1,2] + ", b=" + tmp_sh[2,2];

            Debug.Log(s1);
            Debug.Log(s2);
            Debug.Log(s3);
            // Light directionalLight = new Light();
            
            // directionalLight.type = LightType.Directional;
            // directionalLight.color = args.lightEstimation.mainLightColor.Value;
            // directionalLight.intensity = 1.0f;
            // directionalLight.transform.rotation = Quaternion.LookRotation(args.lightEstimation.mainLightDirection.Value);
            
            // RenderSettings.sun = directionalLight;
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = tmp_sh;
        }

    }   
}