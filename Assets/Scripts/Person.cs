using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Person : MonoBehaviour
{
    private bool isWearingMask = false;
    private bool isVaccinated = false;

    public Rigidbody2D rb;
    public float radius = .0f;

    // Start is called before the first frame update
    void Start()
    {
        setStartingPosition();
    }

    private void setStartingPosition()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float boundX = width / 2 - spriteRenderer.bounds.extents.x;
        float boundY = height / 2 - spriteRenderer.bounds.extents.y;

        this.transform.position = new Vector3(Random.Range(boundX, boundX), Random.Range(boundY, boundY),
            this.transform.position.z);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(
                Random.Range(-1 * radius, radius),
                Random.Range(-1 * radius, radius)
                );
        }
    }
}
