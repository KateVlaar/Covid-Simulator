using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obj;
    public int numberOfPeople = 10;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfPeople; i++)
        {
            Instantiate(obj, setStartingPosition(), Quaternion.identity);
        }
    }

    private Vector3 setStartingPosition()
    {
        Camera cam = Camera.main;
        float height = cam.orthographicSize;
        float width = height * cam.aspect / 2.0f;

        return new Vector3(
                Random.Range(-1 * height, height),
                Random.Range(-1 * width, width),
                0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
