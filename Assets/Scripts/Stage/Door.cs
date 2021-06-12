using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsLeft = false;
    private bool _completed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _completed = true;
            GameManager.Instance.CompletedDoor(IsLeft);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _completed = false;
            GameManager.Instance.ExitedDoor(IsLeft);
        }
    }
}