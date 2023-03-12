using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class SpawnableManager : MonoBehaviour
{   
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    [SerializeField]
    GameObject spawnablePrefab;

    GameObject spawnedObject;

    private int spawncount;
    public Vector3 hitlocation;
    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        spawncount=0;
    }

    // Update is called once per frame
    void Update()
    {   
        // if (spawncount!=0) 
        //     return;

        if (Input.touchCount==0)
            return;
        
        Debug.Log("Touched");
        
        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began && spawncount==0)
            {
                SpawnPrefab(m_Hits[0].pose.position);
                spawncount+=1;
            }
            if(Input.GetTouch(0).phase == TouchPhase.Began && spawncount==1)
            {
                hitlocation = m_Hits[0].pose.position;
                spawncount+=1;
            }

            else if(Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
            {
                spawnedObject.transform.position = m_Hits[0].pose.position;
            }
            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnedObject = null;
            }
        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnedObject = Instantiate(spawnablePrefab, spawnPosition,new Quaternion(0,1,0,0));
    }
}
