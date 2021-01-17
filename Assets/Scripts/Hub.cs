using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
/*    public GameObject Hub;
*/    private SpriteRenderer spriteRenderer = null;
    private float boundX = 0.0f;
    private float boundY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
/*        Hub.name = "Hub";
*/
        spriteRenderer = GetComponent<SpriteRenderer>();
        boundY = spriteRenderer.bounds.extents.y;
        boundX = spriteRenderer.bounds.extents.x;
        Debug.Log(boundX);
        Debug.Log(boundY);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
