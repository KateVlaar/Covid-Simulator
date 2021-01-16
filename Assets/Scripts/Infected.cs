using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : Person
{
    private double infectionRadius = 5.0;
    /* Chance out of 0.5 that a susceptible person inside of the infection radius will be infected on each tick */
    private double infectionChance = 0.01;

    SpriteRenderer m_SpriteRenderer;
    ContactFilter2D filter = new ContactFilter2D();
    List<Collider2D> inProximity = new List<Collider2D>();
    
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //Set the GameObject's Color quickly to a set Color
        m_SpriteRenderer.color = Color.red;
        filter.NoFilter();
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Collider2D>().OverlapCollider(filter, inProximity);
        foreach(Collider2D person in inProximity)
        {
            person.gameObject.SendMessage("Infection", infectionChance);
        }
    }

    public void Infection(double chance) {}
}
