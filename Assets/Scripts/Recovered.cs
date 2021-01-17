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

     public void setOldPos(Vector3 pos)
    {
	    this.originalPosition = pos;
    }

    public void setHub(bool inHub)
    {
        this.inHub = inHub;
    }

    public void setOutHubTimer(double i)
    {
        this.enterHubTime = i;
    }

    public void setInHub(double i)
    {
        this.timeInHub = i;
    }
}
