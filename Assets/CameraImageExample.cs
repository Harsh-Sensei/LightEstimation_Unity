using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearRegression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class CameraImageExample : MonoBehaviour
{
    public Texture2D m_Texture;
    public ARCameraManager cameraManager;

    //public Texture2DArray bases;

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
    public Texture2D basis16;

    public Texture2D targetbox;

    // public RenderTexture rendtexture;
    public Texture2D targettexture;


    public SphericalHarmonicsL2 sphericalHarmonics;
    private SphericalHarmonicsL2 whiteSH;
    //public RawImage rawImage;

    private string tempstring;
    public string FinalOutput;
    //public GameObject textDisplay;
    private float starttime;
    private float deltime;

    public float[,] coefficients;
    private int miplevel;
    private int texturedim;
    private int arraysize;
    private int numbasis;
    //public Texture2D destTex;
    //private float[,] BasisMat = new float[16,256*256];

    //private Matrix<float> BasisMat = Matrix<float>.Build.Dense(9,256*256);  
    //private float[] refbasis;
    private Matrix<float> BasisMat;  
    void BasisInit()
    {   
        //int arraysize = 256*256;
        miplevel=2;
        texturedim= (int) (Math.Pow(2,8-miplevel));
        arraysize = texturedim*texturedim;
        numbasis=16;
        BasisMat= Matrix<float>.Build.Dense(numbasis,texturedim*texturedim); 
        //Color[] tempcolor = basis0.GetPixels(0,0,256,256);
        Color[] tempcolor = basis0.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[0,j]=tempcolor[j].grayscale;
        }
        //tempcolor = basis1.GetPixels(0,0,256,256);
        tempcolor = basis1.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[1,j]=tempcolor[j].grayscale;
        }
        //tempcolor = basis2.GetPixels(0,0,256,256);
        tempcolor = basis2.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[2,j]=tempcolor[j].grayscale;
        }
        //tempcolor = basis3.GetPixels(0,0,256,256);
        tempcolor = basis3.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[3,j]=tempcolor[j].grayscale;
        }
        //tempcolor = basis4.GetPixels(0,0,256,256);
        tempcolor = basis4.GetPixels(miplevel);
        
        for(int j=0; j<arraysize;j++){
            BasisMat[4,j]=tempcolor[j].grayscale;
        }
        //tempcolor = basis5.GetPixels(0,0,256,256);
        tempcolor = basis5.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[5,j]=tempcolor[j].grayscale;
        }

        //tempcolor = basis6.GetPixels(0,0,256,256);
        tempcolor = basis6.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[6,j]=tempcolor[j].grayscale;
        }
        //tempcolor = basis7.GetPixels(0,0,256,256);
        tempcolor = basis7.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[7,j]=tempcolor[j].grayscale;
        }
        //tempcolor = basis8.GetPixels(0,0,256,256);
        tempcolor = basis8.GetPixels(miplevel);
        for(int j=0; j<arraysize;j++){
            BasisMat[8,j]=tempcolor[j].grayscale;
        }

        if (numbasis>9){
            //tempcolor = basis9.GetPixels(0,0,256,256);
            tempcolor = basis9.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[9,j]=tempcolor[j].grayscale;
            }
            //tempcolor = basis10.GetPixels(0,0,256,256);
            tempcolor = basis10.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[10,j]=tempcolor[j].grayscale;
            }
            //tempcolor = basis11.GetPixels(0,0,256,256);
            tempcolor = basis11.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[11,j]=tempcolor[j].grayscale;
            }
            //tempcolor = basis12.GetPixels(0,0,256,256);
            tempcolor = basis12.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[12,j]=tempcolor[j].grayscale;
            }
            
            //tempcolor = basis13.GetPixels(0,0,256,256);
            tempcolor = basis13.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[13,j]=tempcolor[j].grayscale;
            }
            //tempcolor = basis14.GetPixels(0,0,256,256);
            tempcolor = basis14.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[14,j]=tempcolor[j].grayscale;
            }
            //tempcolor = basis15.GetPixels(0,0,256,256);
            tempcolor = basis15.GetPixels(miplevel);
            for(int j=0; j<arraysize;j++){
                BasisMat[15,j]=tempcolor[j].grayscale;
            }
        }
        
        FinalOutput="wut";
    }


    

    public void OnEnable()
    {
        //cameraManager.frameReceived += OnCameraFrameReceived;
        BasisInit();
        coefficients = new float[3,numbasis];
        //targettexture = new Texture2D(608,1350);
        //destTex = new Texture2D(455, 455);

    }

    public void OnDisable()
    {
        //cameraManager.frameReceived -= OnCameraFrameReceived;
    }

    static float[] ColortoFloat(Color[] inp,int arraylength)
    {   
        //int arraylength=100;
        float[] outputarray = new float[arraylength];
        for(int i = 0; i < arraylength; i++){
            outputarray[i]=inp[i].grayscale;
        }
        return outputarray;
    }

    static float[] ColortoFloatR(Color[] inp,int arraylength)
    {   
        //int arraylength=100;
        float[] outputarray = new float[arraylength];
        for(int i = 0; i < arraylength; i++){
            outputarray[i]=inp[i].r;
        }
        return outputarray;
    }

    static float[] ColortoFloatG(Color[] inp,int arraylength)
    {   
        //int arraylength=100;
        float[] outputarray = new float[arraylength];
        for(int i = 0; i < arraylength; i++){
            outputarray[i]=inp[i].g;
        }
        return outputarray;
    }  
    static float[] ColortoFloatB(Color[] inp,int arraylength)
    {   
        //int arraylength=100;
        float[] outputarray = new float[arraylength];
        for(int i = 0; i < arraylength; i++){
            outputarray[i]=inp[i].b;
        }
        return outputarray;
    }

    Texture2D Resize(Texture2D texture2D,int targetX,int targetY)
    {
        RenderTexture rt=new RenderTexture(targetX, targetY,24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D,rt);
        Texture2D result=new Texture2D(targetX,targetY);
        result.ReadPixels(new Rect(0,0,targetX,targetY),0,0);
        result.Apply();
        return result;
    }

    static Color[] masktarget(float[] basis0, Color[] inp, int arraylength)
    {
        for(int i = 0; i < arraylength; i++)
        {
            if(basis0[i]==0)
            {
                inp[i]=  Color.black;
            }
        }
        return inp;
    }

 //  unsafe void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    public unsafe void ComputeCoeff()
    {   
        FinalOutput="Init";
        FinalOutput="Able to load Basis";
        starttime = Time.realtimeSinceStartup;
        


        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image)){
            FinalOutput="Unable to Get Image";
            return;
        }
        
        var conversionParams = new XRCpuImage.ConversionParams
        {
            // Get the entire image.
            inputRect = new RectInt(0, 0, image.width, image.height),

            // // Downsample by 2.
            // outputDimensions = new Vector2Int(image.width / 2, image.height / 2),
            outputDimensions = new Vector2Int(image.width, image.height),

            // Choose RGBA format.
            outputFormat = TextureFormat.RGBA32,

            // // Flip across the vertical axis (mirror image).
             transformation = XRCpuImage.Transformation.MirrorY
            // transformation = XRCpuImage.Transformation.None
        };

        // See how many bytes you need to store the final image.
        int size = image.GetConvertedDataSize(conversionParams);

        // Allocate a buffer to store the image.
        var buffer = new NativeArray<byte>(size, Allocator.Temp);

        // Extract the image data
        image.Convert(conversionParams, new IntPtr(buffer.GetUnsafePtr()), buffer.Length);

        // The image was converted to RGBA32 format and written into the provided buffer
        // so you can dispose of the XRCpuImage. You must do this or it will leak resources.
        image.Dispose();

        // At this point, you can process the image, pass it to a computer vision algorithm, etc.
        // In this example, you apply it to a texture to visualize it.

        // You've got the data; let's put it into a texture so you can visualize it.
        m_Texture = new Texture2D(
            conversionParams.outputDimensions.x,
            conversionParams.outputDimensions.y,
            conversionParams.outputFormat,
            false);

        m_Texture.LoadRawTextureData(buffer);
        m_Texture.Apply();
        
        // int x = Mathf.FloorToInt(m_Texture.x);
        // int y = Mathf.FloorToInt(m_Texture.y);
        // int width = Mathf.FloorToInt(m_Texture.width);
        // int height = Mathf.FloorToInt(m_Texture.height);
        //int sourceMipLevel = 0;
        

        // Done with your temporary data, so you can dispose it.
        buffer.Dispose();

        
    

        // Remember we want it to be of size 256x256 not 455x455
        // m_Texture.Resize(1080,2400);
        // int x =350;
        // int y=1162; //or 1238 from the other direction
        // int width = 455;
        // int height=455;
        // Scale by 256/455 = 0.5626

        //m_Texture.Resize(608,1350);

        // targettexture =Resize(m_Texture,608,1350);

        // //targettexture = rawImage.texture as Texture2D;

        // int x =197;
        // int y=696;//653/696
        // int width=256;
        // int height=256;

        //NEW
        // Remember we want it to be of size 256x256 not 455x455
        // m_Texture.Resize(640,480);
        // int x =330;
        // int y=317; //or 1238 from the other direction
        // int width = 117;
        // int height=117;
        // Scale by 256/117 = 2.188


        int oldwidth = m_Texture.width;
        int oldheight =m_Texture.height;


        //m_Texture.Resize(608,1350);

        targettexture =Resize(m_Texture,(int)(1400/Math.Pow(2,miplevel)),(int)(1050/Math.Pow(2,miplevel)));

        //targettexture = rawImage.texture as Texture2D;

        int x =(int)(730/Math.Pow(2,miplevel));
        int y=(int)(385/Math.Pow(2,miplevel));//653/696
        int width=(int)(256/Math.Pow(2,miplevel));
        int height=(int)(256/Math.Pow(2,miplevel));



        // Resize  
        Color[] inttarget = targettexture.GetPixels(x, y, width, height);
        // Mask
        float[] refbasis = ColortoFloat(basis0.GetPixels(miplevel),arraysize);
        inttarget = masktarget(refbasis,inttarget,arraysize);

        // FinalOutput="Loading camera Texture";
        targetbox = new Texture2D((int)(256/Math.Pow(2,miplevel)), (int)(256/Math.Pow(2,miplevel)));
        targetbox.SetPixels(inttarget,0);
        FinalOutput="I can set Pixels";
        targetbox.Apply();
        
        float[] targ = ColortoFloat(inttarget,arraysize);

        //FinalOutput="Almost Regression";
        //float[,] Atemp = BasisMat;
        var A = BasisMat.Transpose();
        
        // Color[] TempCol =basis1.GetPixels(0,0,256,256);
        // //FinalOutput = "Created TempCol";
        // targ = ColortoFloat(TempCol,arraysize);
       // FinalOutput="Created b";
        var b = Vector<float>.Build.Dense(targ);
        FinalOutput=" ";
        //deltime = Time.realtimeSinceStartup-starttime;
        //FinalOutput="Regression Started"+deltime.ToString()+"b dim"+b.Count.ToString()+"A dim"+A.ToMatrixString();
        starttime = Time.realtimeSinceStartup;
        var solx = MultipleRegression.QR(A,b);
        deltime = Time.realtimeSinceStartup-starttime;
        tempstring =" ";
        // for(int i=0;i<9;i++){
        //     tempstring+="B"+i.ToString()+" "+solx[i].ToString("F3");
        // } 
        FinalOutput="Time for Regression "+ deltime.ToString() +tempstring;
        //FinalOutput="Basis "+tempstring;
        //Colours
        FinalOutput+=tempstring;
        targ = ColortoFloatR(inttarget,arraysize);
        b = Vector<float>.Build.Dense(targ);
        solx = MultipleRegression.QR(A,b);
        for(int i=0;i<numbasis;i++){
            if(i<9){
                sphericalHarmonics[0,i]=solx[i]; 
                whiteSH[0,i]=0f;
            }
            coefficients[0,i]=solx[i]; 
        }
        targ = ColortoFloatG(inttarget,arraysize);
        b = Vector<float>.Build.Dense(targ);
        solx = MultipleRegression.QR(A,b);
        for(int i=0;i<numbasis;i++){
            if(i<9){
                sphericalHarmonics[1,i]=solx[i]; 
                whiteSH[1,i]=0f;
            }
            coefficients[1,i]=solx[i]; 
        }
        targ = ColortoFloatB(inttarget,arraysize);
        b = Vector<float>.Build.Dense(targ);
        solx = MultipleRegression.QR(A,b);
        for(int i=0;i<numbasis;i++){
            if(i<9){
                sphericalHarmonics[2,i]=solx[i]; 
                whiteSH[2,i]=0f;
            }
            coefficients[2,i]=solx[i]; 
        }
        whiteSH[0,0]=1f;
        whiteSH[1,0]=1f;
        whiteSH[2,0]=1f;
        FinalOutput= "Coefficients Set";
        RenderSettings.ambientMode = AmbientMode.Skybox;
        //RenderSettings.ambientProbe = sphericalHarmonics;
        FinalOutput= "Ambient light set";
        RenderSettings.ambientProbe = whiteSH;

        //FinalOutput="Time for Regression "+ deltime.ToString()+" Values "+solx[0].ToString()+" "+solx[1].ToString()+" "+solx[2].ToString()+" old width "+oldwidth.ToString()+" old height "+oldheight.ToString();
        
        //FinalOutput = "Values are"+solx[0].ToString()+solx[1].ToString()+solx[2].ToString()+" time "+deltime.ToString();
        // GameObject go = GameObject.Find ("CustomLightEstimation");
        // LinearLightEstimation LinearLightEstimation= go.GetComponent <LinearLightEstimation> ();
        // FinalOutput = LinearLightEstimation.FinalOutput;
        //textDisplay.GetComponent<Text>().text = "aaaa";
    }
}

