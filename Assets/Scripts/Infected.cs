using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : Person
{
    private double infectionRadius = 5.0;
    /* Chance out of 0.5 that a susceptible person inside of the infection radius will be infected on each tick */
    private double infectionChance = 0.5;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
