using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearRegression;
using UnityEngine.Rendering;

public class InterpolateTextures : MonoBehaviour
{

    public Texture2D basis0;
    public Texture2D basis1;
    public Texture2D basis2;
    public Texture2D basis3;
    public Texture2D basis4;
    public Texture2D basis5;
    public Texture2D basis6;
    public Texture2D basis7;
    public Texture2D basis8;
    public Texture2D basis9;
    public Texture2D basis10;
    public Texture2D basis11;
    public Texture2D basis12;
    public Texture2D basis13;
    public Texture2D basis14;
    public Texture2D basis15;

    public float[,] coefficients;
    public string FinalOutput;

    [SerializeField]
    private GameObject golight;
    private GameObject golocation;

    [SerializeField]
    private Renderer objectrenderer;
    private int numbasis;
    private int texturedim;
    private int arraysize;
    private int miplevel;
    private Color[,] BasisMat;  
    void BasisInit()
    {   
        //int arraysize = 256*256;
        miplevel=0;
        texturedim= (int) (Math.Pow(2,10-miplevel));
        arraysize = texturedim*texturedim;
        
        BasisMat= new Color[numbasis,texturedim*texturedim];
        //BasisMat= Matrix<float>.Build.Dense(16,texturedim*texturedim); 
        //Color[] tempcolor = basis0.GetPixels(0,0,256,256);

        Color[] tempcolor = basis0.GetPixels(miplevel);
        
        for(int j=0; j<arraysize;j++){
            BasisMat[0,j]=tempcolor[j];
        }
        //tempcolor = basis1.GetPixels(0,0,256,256);
        tempcolor = basis1.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[1,j]=tempcolor[j];
        }
        //tempcolor = basis2.GetPixels(0,0,256,256);
        tempcolor = basis2.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[2,j]=tempcolor[j];
        }
        //tempcolor = basis3.GetPixels(0,0,256,256);
        tempcolor = basis3.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[3,j]=tempcolor[j];
        }
        //tempcolor = basis4.GetPixels(0,0,256,256);
        tempcolor = basis4.GetPixels(miplevel);
        
        for(int j=0; j<arraysize;j++){
            BasisMat[4,j]=tempcolor[j];
        }
        //tempcolor = basis5.GetPixels(0,0,256,256);
        tempcolor = basis5.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[5,j]=tempcolor[j];
        }

        //tempcolor = basis6.GetPixels(0,0,256,256);
        tempcolor = basis6.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[6,j]=tempcolor[j];
        }
        //tempcolor = basis7.GetPixels(0,0,256,256);
        tempcolor = basis7.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[7,j]=tempcolor[j];
        }
        //tempcolor = basis8.GetPixels(0,0,256,256);
        tempcolor = basis8.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[8,j]=tempcolor[j];
        }
        if (numbasis>9){
            //tempcolor = basis9.GetPixels(0,0,256,256);
            tempcolor = basis9.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[9,j]=tempcolor[j];
            }
            //tempcolor = basis10.GetPixels(0,0,256,256);
            tempcolor = basis10.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[10,j]=tempcolor[j];
            }
            //tempcolor = basis11.GetPixels(0,0,256,256);
            tempcolor = basis11.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[11,j]=tempcolor[j];
            }
            //tempcolor = basis12.GetPixels(0,0,256,256);
            tempcolor = basis12.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[12,j]=tempcolor[j];
            }
            
            //tempcolor = basis13.GetPixels(0,0,256,256);
            tempcolor = basis13.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[13,j]=tempcolor[j];
            }
            //tempcolor = basis14.GetPixels(0,0,256,256);
            tempcolor = basis14.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[14,j]=tempcolor[j];
            }
            //tempcolor = basis15.GetPixels(0,0,256,256);
            tempcolor = basis15.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[15,j]=tempcolor[j];
            }

        }
        
        
    }

    // static float[] ColortoFloat(Color[] inp,int arraylength)
    // {   
    //     //int arraylength=100;
    //     float[] outputarray = new float[arraylength];
    //     for(int i = 0; i < arraylength; i++){
    //         outputarray[i]=inp[i].grayscale;
    //     }
    //     return outputarray;
    // }

    // Start is called before the first frame update
    void Start()
    {   
        FinalOutput="Start";
        numbasis=16;

        objectrenderer = GetComponent<Renderer>();
        objectrenderer.material.EnableKeyword ("_NORMALMAP");
        BasisInit();
        coefficients = new float[3,numbasis];
        FinalOutput="Initialized";
    }

    private float[] weights;
    private Color[] targettexturecolor;
    public Texture2D targettexture;
    public Texture2D normaltexture;
    // Update is called once per frame
    public void Interpolate()
    {      
        targettexturecolor = basis0.GetPixels(miplevel);
        
        FinalOutput="target texture initialized";
        //weights = new float[] {0.5f,0.1f,-0.2f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f};
        Color[] weights= new Color[numbasis];
        FinalOutput="weights initialized";
        for(int i=0; i<numbasis;i++){
            weights[i]= Color.red*coefficients[0,i]+Color.green*coefficients[1,i]+Color.blue*coefficients[2,i];
            weights[i][3]=1f;
        }
        FinalOutput="weight filled";
        for(int i =0; i<numbasis;i++){
            for(int j =0; j<arraysize;j++){
                targettexturecolor[j] = targettexturecolor[j]+weights[i]*BasisMat[i,j];
                targettexturecolor[j][3]=1f;
            }
        }
        FinalOutput="target texture color filled"+targettexture.width.ToString()+" "+targettexture.height.ToString();
        targettexture.SetPixels(targettexturecolor, miplevel);
        FinalOutput = "Almost Applied";
        targettexture.Apply();
        FinalOutput="target texture applied";
    }

    public void updateLight()
    {   
        FinalOutput="Function Called";
        golight = GameObject.Find ("CustomLightEstimation");
        CameraImageExample CameraImageExample= golight.GetComponent <CameraImageExample> ();
        targettexture = new Texture2D(
            1024,
            1024,
            TextureFormat.RGBA32,
            false);
        coefficients = CameraImageExample.coefficients;
        FinalOutput="Coefficients Loaded";
        Interpolate();
        FinalOutput="Interpolated";
        // objectrenderer.material.SetTexture("_MainTex", targettexture);
        // objectrenderer.material.SetTexture("_BumpMap", normaltexture);
        GetComponent<Renderer>().material.mainTexture = targettexture;

        golocation = GameObject.Find ("SpawnManager");
        SpawnableManager SpawnableManager = golocation.GetComponent <SpawnableManager> ();
        transform.position = SpawnableManager.hitlocation +  Vector3.left*0.2f;

        //Interpolate
    }
}
