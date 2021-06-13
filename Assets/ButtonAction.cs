using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] private GameObject go;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public bool pressed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boulder"))
        {
            go.SetActive(true);
            anim.SetBool("Pressed", true);
        }
    }
}
