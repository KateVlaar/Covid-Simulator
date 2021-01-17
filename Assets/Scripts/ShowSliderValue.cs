using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderValue : MonoBehaviour
{
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void textUpdate(float value)
    {
        text.text = value.ToString();
    }
}
