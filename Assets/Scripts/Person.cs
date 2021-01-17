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
    }

    public void Infection(double chance) 
    {
        if(Random.Range(0f, 1f) < chance) {
            this.gameObject.AddComponent<Infected>();
            Destroy(this);
        }
    }
}
