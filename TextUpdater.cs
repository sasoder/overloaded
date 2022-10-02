using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    public TextMeshPro textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox.text = "best: " + PlayerPrefs.GetInt("best") + "\npro best: " + PlayerPrefs.GetInt("bestHard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}