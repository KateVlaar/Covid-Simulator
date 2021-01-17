using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovered : Person
{
    SpriteRenderer m_SpriteRenderer;
    ContactFilter2D filter = new ContactFilter2D();
    List<Collider2D> inProximity = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //Set the GameObject's Color quickly to a set Color
        m_SpriteRenderer.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void Infection(Infected inf) { }
}
