using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        StopMovement();
    }

    void StopMovement()
    {
        if(transform.position.y <= -10f)
        {
            rb.velocity = new Vector2(0f, 0f);
            transform.position = new Vector3(transform.position.x, -10f, transform.position.z);
        }
    }
}
