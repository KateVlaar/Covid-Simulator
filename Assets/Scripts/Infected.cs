using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : Person
{
    public double infectionRadius = 10.0;
    /* Chance out of 0.5 that a susceptible person inside of the infection radius will be infected on each tick */
    public double infectionChance = 1;
    /* Flags if we can infect or not */
    private bool canInfect = false;
	
	/* How often we check for infections in s */
	public double infTimer = 0.5;
public float fullRecoveryTime = 30.0f;
    public float recoveryTime = 30.0f;
    public GameObject recovered;
    
    private float startCooldown = 2.0f;

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
        filter.useLayerMask = true;
        filter.layerMask = LayerMask.GetMask("People");
    }

    // Update is called once per frame
    void Update()
    {
	    base.Update();
        
        /**
         * Only try to infect and tick down to recovery once canInfect is set
         */
        if (this.canInfect)
        {
	        this.infTimer -= Time.deltaTime;
			if(infTimer < 0)
			{
				this.GetComponent<Collider2D>().OverlapCollider(filter, inProximity);
	            foreach(Collider2D person in inProximity)
	            {
                    person.gameObject.SendMessage("Infection", this);
	            }
				infTimer = 0.5;
			}

        	recoveryTime -= Time.deltaTime;
            if (recoveryTime < 0)
            {
                GameObject obj = (GameObject)Instantiate(recovered, this.transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;
                Destroy(this.gameObject);
            }   
        }
    }

    public void setCanInfect(bool canInfect)
    {
        this.canInfect = canInfect;
    }

    /**
     * Only call base.Start() if we are spawning a new infected (not replacing a susceptible)
     */
    public void setStartingPosition()
    {
        base.Start();
    }

    public void setInfectionRadius(float radius)
    {
        this.infectionRadius = radius;
	    this.GetComponent<CircleCollider2D>().radius = radius;
        this.GetComponent<Light>().range = radius*2;
    }

    public void setInfectionChance(float chance)
    {
	    this.infectionChance = chance;
    }

    public void setRecoveryTime(float recoveryTime)
    {
	    this.recoveryTime = recoveryTime;
    }

    public void setOldPos(Vector3 pos)
    {
	    this.originalPosition = pos;
    }

    public void setHubStatus(bool inHub)
    {
        this.inHub = inHub;
    }

    public void Infection(Infected inf) {}
}
