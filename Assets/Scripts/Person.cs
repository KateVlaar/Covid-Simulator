using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Person : MonoBehaviour
{
    public GameObject infected;

    private bool isWearingMask = false;
    private bool isVaccinated = false;
    private double velocityChangeTime = 2.0f;

    private double enterHubTime = 2.0f;
    private double timeInHub = 3.0f;
    private Boolean inHub = false;
    private Vector3 originalPosition;

    private SpriteRenderer spriteRenderer = null;
    private float boundX = 0.0f;
    private float boundY = 0.0f;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    protected void Start()
    {
        setBoundaries();
        setStartingPosition();
        chooseNewRandomDelay();
    }

    /**
     * Calculates the boundaries which the player can move within
     */
    private void setBoundaries()
    {
        Camera cam = Camera.main;
        float cameraHeight = 2f * cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;

        spriteRenderer = GetComponent<SpriteRenderer>();
        boundY = cameraHeight / 2 - spriteRenderer.bounds.extents.y;
        boundX = cameraWidth / 2 - spriteRenderer.bounds.extents.x;
    }

    /**
     * Sets the starting position within the camera.
     */
    private void setStartingPosition()
    {
        this.transform.position = new Vector3(Random.Range(-boundX, boundX), Random.Range(-boundY, boundY),
            this.transform.position.z);
    }

    /**
     * Chooses a new delay for the next time to update the velocity
     */
    private void chooseNewRandomDelay()
    {
        this.velocityChangeTime = Random.Range(0.5f, 5.0f);
    }

    // Update is called once per frame
    protected void Update()
    {
        /* Update the velocity if necessary */
        this.velocityChangeTime -= Time.deltaTime;
        if (this.velocityChangeTime <= 0.0f)
        {
            rb.velocity = new Vector2(
                Random.Range(-1.0f, 1.0f),
                Random.Range(-1.0f, 1.0f)
            );
            this.chooseNewRandomDelay();
        }

        enterHub();
    }

    public void enterHub()
    {
        GameObject hub = GameObject.Find("Hub"); // Will need this for each hub

        float prob = Random.Range(0.0f, 1.0f);

        if (inHub)
        {
            this.timeInHub -= Time.deltaTime;
            if (this.timeInHub <= 0.0f)
            {
                if (prob <= 1.0f) // Guarantees they leave the hub - subject to change
                {
                    this.transform.position = this.originalPosition;
                    this.inHub = false;
                }
                this.timeInHub = 3.0f;
            }
        } 
        else
        {
            this.enterHubTime -= Time.deltaTime;
            if (this.enterHubTime <= 0.0f)
            {
                this.originalPosition = this.transform.position;
                if (prob <= 0.2f) // Probability they enter the hub - pretty high for testing
                {
                    SpriteRenderer hubSpriteRenderer = hub.GetComponent<SpriteRenderer>();
                    float x = hubSpriteRenderer.bounds.extents.x;
                    float y = hubSpriteRenderer.bounds.extents.y;
                    Vector3 pos = hubSpriteRenderer.transform.position;

                    /* Spawns sprites within the hub */
                    this.transform.position = new Vector3(Random.Range(pos.x-x, pos.x+x), Random.Range(pos.y - y, pos.y + y));
                    this.inHub = true;
                }
                this.enterHubTime = 2.0f;
            }
        }
    }

    public void Infection(Infected inf) 
    {
        double distFactor = Math.Pow(20, -(Vector3.Distance(this.transform.position, inf.transform.position) - 0.2));
        if(Random.Range(0f, 1f) < inf.infectionChance*distFactor) {
            GameObject obj = (GameObject)Instantiate(infected, this.transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;
            obj.gameObject.SendMessage("setCanInfect", true);
            /* Inherit all attributes from the infector */
            obj.gameObject.SendMessage("setInfectionRadius", inf.infectionRadius);
            obj.gameObject.SendMessage("setInfectionChance", inf.infectionChance);
            obj.gameObject.SendMessage("setRecoveryTime", inf.recoveryTime);
            Destroy(this.gameObject);
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
    }
}
