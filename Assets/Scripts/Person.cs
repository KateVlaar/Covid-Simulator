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

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        setStartingPosition();
        chooseNewRandomDelay();
    }

    /**
     * Sets the starting position within the camera.
     */
    private void setStartingPosition()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float boundX = width / 2 - spriteRenderer.bounds.extents.x;
        float boundY = height / 2 - spriteRenderer.bounds.extents.y;

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
}
