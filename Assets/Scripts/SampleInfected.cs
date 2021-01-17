using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Sample infected class to visualize infection radius in the main menu
 */
public class SampleInfected : MonoBehaviour
{
    public void setInfectionRadius(float radius)
    {
        this.GetComponent<CircleCollider2D>().radius = radius;
        this.GetComponent<Light>().range = radius*2;
    }
}
