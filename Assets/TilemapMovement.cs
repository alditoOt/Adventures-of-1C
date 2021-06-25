using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float endPosition = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        StopMovement();
    }

    public void StartMovement(bool buttonPress)
    {
        if(!buttonPress)
        {
            transform.position = new Vector3(0f, 0f, 0f);
            rb.velocity = new Vector2(0f, 0f);
        }
    }
    void StopMovement()
    {
        if(transform.position.y <= endPosition)
        {
            rb.velocity = new Vector2(0f, 0f);
            transform.position = new Vector3(transform.position.x, endPosition, transform.position.z);
        }
    }
}
