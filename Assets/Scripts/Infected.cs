using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : Person
{
    public double infectionRadius = 10.0;
    /* Chance out of 0.5 that a susceptible person inside of the infection radius will be infected on each tick */
    public double infectionChance = 0.1;

    public double infTimer = 0.5;

    public float recoveryTime = 30.0f;
    public GameObject recovered;

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
        float delta = Time.deltaTime;
        this.infTimer -= delta;
        if(infTimer < 0) 
        {
            this.GetComponent<Collider2D>().OverlapCollider(filter, inProximity);
            foreach(Collider2D person in inProximity)
            {
                person.gameObject.SendMessage("Infection", this);
            }
            infTimer = 0.5;
        }
        base.Update();

        recoveryTime -= delta;
        if (recoveryTime < 0)
        {
            GameObject obj = (GameObject)Instantiate(recovered, this.transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;
            Destroy(this.gameObject);
        }
    }

    public void Infection(Infected inf) {}
}
