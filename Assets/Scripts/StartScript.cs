using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    public GameObject susceptible;
    public GameObject infected;
    /* Slider options */
    public int numInfected = 1;
    public int numSusceptible = 10;
    public int numSusceptibleWithMasks = 0;
    public float infectionRadius = 5.0f;
    public float infectionChance = 0.5f;

    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void spawnSusceptible()
    {
        for (int i = 0; i < numSusceptible; i++)
        {
            // TODO: Replace obj with susceptible object
            Instantiate(susceptible, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    private void spawnInfected()
    {
        for (int i = 0; i < numInfected; i++)
        {
            // TODO: Replace obj with infected object
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
        }
    }
}
