using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Person : MonoBehaviour
{
    private bool isWearingMask = false;
    private bool isVaccinated = false;
    private double velocityChangeTime = 2.0f;
    private double enterHubTime = 2.0f;
    private double timeInHub = 2.0f;
    private Boolean inHub = false;
    private Vector3 originalPosition;

    private SpriteRenderer spriteRenderer = null;
    private float boundX = 0.0f;
    private float boundY = 0.0f;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
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
    void Update()
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
/*        Debug.Log(gameObject.Find("Hub"));
        GameObject hub = gameObject.Find("Hub");*/

        if (inHub)
        {
            this.timeInHub -= Time.deltaTime;
            if (this.timeInHub <= 0.0f)
            {
                float prob = Random.Range(0.0f, 1.0f);
                if (prob <= 1.0f)
                {
                    this.transform.position = this.originalPosition;
                    this.inHub = false;
                }
                this.timeInHub = 2.0f;
            }
        } 
        else
        {
            this.enterHubTime -= Time.deltaTime;
            if (this.enterHubTime <= 0.0f)
            {

                this.originalPosition = this.transform.position;
                float prob = Random.Range(0.0f, 1.0f);
                if (prob <= 0.2f)
                {
                    // TODO: use real hub coordinates
                    this.transform.position = new Vector3(0, 0);
                    this.inHub = true;
                }
                this.enterHubTime = 2.0f;
            }
        }
    }

    public void Infection(double chance) 
    {
        if(Random.Range(0f, 1f) < chance) {
            this.gameObject.AddComponent<Infected>();
            Destroy(this);
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
    }
}
