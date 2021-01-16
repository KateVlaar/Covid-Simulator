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
            Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
