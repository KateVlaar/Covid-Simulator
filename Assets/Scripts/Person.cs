using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Person : MonoBehaviour
{
    private bool isWearingMask = false;
    private bool isVaccinated = false;

    // Start is called before the first frame update
    void Start()
    {
        setStartingPosition();
    }

    private void setStartingPosition()
    {
        this.transform.position = new Vector3(Random.Range(0, Screen.width), this.transform.position.y,
            Random.Range(0, Screen.height));
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
