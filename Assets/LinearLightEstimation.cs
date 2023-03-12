using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;




public class LinearLightEstimation : MonoBehaviour
{   
    private float starttime;
    private float deltime;
    public string FinalOutput;
    //public TextAsset BasisRender;

    public void Awake(){
        // Load TableTennis Ball Basis Matrix
    }

    public void Compute()
    {   
        // Load captured image 
        starttime = Time.realtimeSinceStartup;
        Matrix<double> A = DenseMatrix.OfArray(new double[,] {
        {1,1,1,1},
        {1,2,3,4},
        {4,3,2,1}});
        Vector<double>[] nullspace = A.Kernel();
        Vector<double> result = (A * (2*nullspace[0] - 3*nullspace[1]));
        Vector3 solution = new Vector3( (float)result[0],  (float)result[1],  (float)result[2]);
        deltime = Time.realtimeSinceStartup-starttime;
        FinalOutput = solution.ToString();
        FinalOutput = FinalOutput +" time "+deltime.ToString();
        // verify: the following should be approximately (0,0,0)
        // Vector3 vector = 
    }
}
