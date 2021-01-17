﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    public GameObject susceptible;
    public GameObject infected;
    /* Slider options */
    public int numInfected = 1;
    public int numSusceptible = 10;
    public float percentWearingMasks = 0.0f;
    public float infectionRadius = 5.0f;
    public float infectionChance = 0.5f;
    private float recoveryTime = 10.0f;

    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        // TODO:
        // - Hookup wearing masks
        // - Hookup infection radius
        // - Hookup infection chance
        // - hookup recovery time
        
        /* Setup all slider listeners and initialize the value to the slider's starting value */
        Slider susceptibleSlider = GameObject.Find("SusceptibleSlider").GetComponent<Slider>();
        susceptibleSlider.onValueChanged.AddListener(updateNumSusceptible);
        updateNumSusceptible(susceptibleSlider.value);
        
        /* Setup all slider listeners and initialize the value to the slider's starting value */
        Slider infectedSlider = GameObject.Find("InfectedSlider").GetComponent<Slider>();
        infectedSlider.onValueChanged.AddListener(updateNumInfected);
        updateNumInfected(infectedSlider.value);
        
        /* Setup all slider listeners and initialize the value to the slider's starting value */
        Slider percentWearingMasks = GameObject.Find("MaskSlider").GetComponent<Slider>();
        percentWearingMasks.onValueChanged.AddListener(updatePercentWearingMasks);
        updatePercentWearingMasks(percentWearingMasks.value);
        
        /* Setup all slider listeners and initialize the value to the slider's starting value */
        // TODO: Visualize this :)
        Slider infectionRadius = GameObject.Find("InfectionRadiusSlider").GetComponent<Slider>();
        infectionRadius.onValueChanged.AddListener(updateInfectionRadius);
        updateInfectionRadius(percentWearingMasks.value);
        
        /* Setup all slider listeners and initialize the value to the slider's starting value */
        Slider infectionChance = GameObject.Find("InfectionChanceSlider").GetComponent<Slider>();
        infectionChance.onValueChanged.AddListener(updateInfectionChance);
        updateInfectionChance(infectionChance.value);
        
        /* Setup all slider listeners and initialize the value to the slider's starting value */
        Slider recoveryTime = GameObject.Find("RecoveryTimeSlider").GetComponent<Slider>();
        recoveryTime.onValueChanged.AddListener(updateRecoveryTime);
        updateRecoveryTime(recoveryTime.value);
    }

    private void spawnSusceptible()
    {
        for (int i = 0; i < numSusceptible; i++)
        {
            Instantiate(susceptible, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    private void spawnInfected()
    {
        for (int i = 0; i < numInfected; i++)
        {
            Instantiate(infected, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !spawned)
        {
            spawned = true;
            spawnSusceptible();
            spawnInfected();
            /* Hide the starting text */
            GameObject.Find("StartText").SetActive(false);
            GameObject.Find("SliderCanvas").SetActive(false);
        }
    }

    /**
     * Slider listeners
     */
    public void updateNumSusceptible(float value)
    {
        this.numSusceptible = (int) value;
    }

    public void updateNumInfected(float value)
    {
        this.numInfected = (int) value;
    }

    public void updatePercentWearingMasks(float value)
    {
        this.percentWearingMasks = value;
    }

    public void updateInfectionRadius(float value)
    {
        this.infectionRadius = value;
    }

    public void updateInfectionChance(float value)
    {
        this.infectionChance = value;
    }

    public void updateRecoveryTime(float value)
    {
        this.recoveryTime = value;
    }
}
