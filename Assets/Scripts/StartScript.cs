using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    public GameObject background;
    public GameObject susceptible;
    public GameObject infected;
    public GameObject hubA;
    public GameObject hubB;
    public GameObject hubC;

    /* Slider options */
    public int numInfected = 1;
    public int numSusceptible = 10;
    public float infectionRadius = 0.5f;
    public float infectionChance = 0.5f;
    private float recoveryTime = 10.0f;

    private bool spawned = false;
    
    /* List of all infected clones */
    private List<GameObject> infectedList = new List<GameObject>();
    // Wait 2s before infecting
    private float startTimer = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        /* Setup all slider listeners and initialize the value to the slider's starting value */
        Slider susceptibleSlider = GameObject.Find("SusceptibleSlider").GetComponent<Slider>();
        susceptibleSlider.onValueChanged.AddListener(updateNumSusceptible);
        updateNumSusceptible(susceptibleSlider.value);
        
        /* Setup all slider listeners and initialize the value to the slider's starting value */
        Slider infectedSlider = GameObject.Find("InfectedSlider").GetComponent<Slider>();
        infectedSlider.onValueChanged.AddListener(updateNumInfected);
        updateNumInfected(infectedSlider.value);

        /* Setup all slider listeners and initialize the value to the slider's starting value */
        Slider infectionRadius = GameObject.Find("InfectionRadiusSlider").GetComponent<Slider>();
        infectionRadius.onValueChanged.AddListener(updateInfectionRadius);
        updateInfectionRadius(infectionRadius.value);

        /* Setup all slider listeners and initialize the value to the slider's starting value */
        Slider infectionChance = GameObject.Find("InfectionChanceSlider").GetComponent<Slider>();
        infectionChance.onValueChanged.AddListener(updateInfectionChance);
        updateInfectionChance(infectionChance.value);
        
        /* Setup all slider listeners and initialize the value to the slider's starting value */
        Slider recoveryTime = GameObject.Find("RecoveryTimeSlider").GetComponent<Slider>();
        recoveryTime.onValueChanged.AddListener(updateRecoveryTime);
        updateRecoveryTime(recoveryTime.value);
    }

    private void spawnHubs()
    {
        Instantiate(hubB, new Vector3(-6.739f, 2.728f, 1.96835f), Quaternion.identity);
        Instantiate(hubC, new Vector3(-3.5f, 2.65f, 1.96835f), Quaternion.identity);
        Instantiate(hubB, new Vector3(4.33f, 2.76f, 1.96835f), Quaternion.identity);
        Instantiate(hubA, new Vector3(7.16f, 2.46f, 1.96835f), Quaternion.identity);
        Instantiate(hubC, new Vector3(-6.49f, -2.17f, 1.96835f), Quaternion.identity);
        Instantiate(hubA, new Vector3(-0.51f, -2.39f, 1.96835f), Quaternion.identity);
        Instantiate(hubA, new Vector3(2.11f, -2.36f, 1.96835f), Quaternion.identity);
        Instantiate(hubA, new Vector3(4.65f, -2.39f, 1.96835f), Quaternion.identity);
        Instantiate(hubA, new Vector3(7.16f, -2.43f, 1.96835f), Quaternion.identity);
    }

    private void spawnBackground()
    {
        Instantiate(background, new Vector3(-1.002354f, -0.113327f, 1.513513f), Quaternion.identity);
    }

    private void spawnSusceptible()
    {
        for (int i = 0; i < numSusceptible; i++)
        {
            Instantiate(susceptible, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
        }
    }

    /**
     * Spawns the infected and returns a list of infected
     */
    private List<GameObject> spawnInfected()
    {
        List<GameObject> infectedList = new List<GameObject>();
        for (int i = 0; i < numInfected; i++)
        {
            infectedList.Add(Instantiate(infected, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity));
            infectedList[i].gameObject.SendMessage("setStartingPosition");
            infectedList[i].gameObject.SendMessage("setInfectionRadius", this.infectionRadius);
            infectedList[i].gameObject.SendMessage("setInfectionChance", this.infectionChance);
            infectedList[i].gameObject.SendMessage("setRecoveryTime", this.recoveryTime);
        }

        return infectedList;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !spawned)
        {
            spawned = true;
            spawnBackground();
            spawnHubs();
            spawnSusceptible();
            infectedList = spawnInfected();
            /* Hide the starting text */
            GameObject.Find("StartText").SetActive(false);
            GameObject.Find("SliderCanvas").SetActive(false);
        }

        if (spawned)
        {
            if (startTimer > 0)
            {
                startTimer -= Time.deltaTime;
            }

            if (startTimer < 0)
            {
                /* Signal to the infected that they can start infecting */
                foreach (var infected in infectedList)
                {
                    if (infected != null)
                    {
                        infected.gameObject.SendMessage("setCanInfect", true);
                    }
                }
            }   
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

    public void updateInfectionRadius(float value)
    {
        this.infectionRadius = value;
        /* Update the radius vizualization in the main menu */
        GameObject.Find("SampleInfected").SendMessage("setInfectionRadius", value);
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
