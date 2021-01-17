using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    int inf, rec, sus;
    // only set done to true
    bool readyToEnd, doneRendering;
    public GameObject restartText;
    public GameObject endStatistics;
    // Start is called before the first frame update
    void Start()
    {
        readyToEnd = false;
        doneRendering = false;
    }
    
    string buildStatistics(int inf, int rec, int sus)
    {
        /*
        return "Total Infected: " + rec +
            "\nNot Infected: " + sus;
            */
        double percentage = (double)rec / ((double)rec + (double)sus);
        return System.String.Format("{0:0.00}% were infected", percentage*100);
    }


    // Update is called once per frame
    void Update()
    {
        inf = GameObject.FindObjectsOfType(typeof(Infected)).Length;
        rec = GameObject.FindObjectsOfType(typeof(Recovered)).Length;
        sus = GameObject.FindObjectsOfType(typeof(Person)).Length - inf - rec;
        
        if (inf == 0 && readyToEnd && !doneRendering)
        {
            doneRendering = true;
            Instantiate(restartText);
            Instantiate(endStatistics).GetComponent<TextMesh>().text = buildStatistics(inf, rec, sus);
        }
        if (inf == 0 && readyToEnd && doneRendering && Input.GetKeyDown(KeyCode.Space))
        {
            // reloads the screen after the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (inf > 0)
        {
            // the infecteds have spawned so we're ready to end
            readyToEnd = true;
        }
    }
}
