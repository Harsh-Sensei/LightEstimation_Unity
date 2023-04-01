using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespositionObject : MonoBehaviour
{
    public GameObject txt;
    public GameObject spawnControl;
    public SpawnableManager spawnScript;

    // Start is called before the first frame update
    void Start()
    {
        spawnScript = spawnControl.GetComponent<SpawnableManager>();
    }
    public void allowReposition()
    {
        txt.GetComponent<Text>().text = "Touch to reposition";
        spawnScript.can_reposition = true;
    }
}
