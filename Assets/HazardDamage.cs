using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("ripardo, reiniciar escena");
            AudioManager.Instance.Play("Death");
            GameManager.Instance.ResetLevel();
        }
    }
}