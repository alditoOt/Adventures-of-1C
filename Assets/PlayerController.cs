using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameObject[] players;

    private void Start()
    {
        LoadPlayers();
    }

    private void LoadPlayers()
    {
        players = FindObjectsOfType<PlayerMovement>().Select(p => p.gameObject).ToArray();
    }

    public void OnMove(InputValue value)
    {
        foreach (var player in players)
        {
            player.BroadcastMessage("OnMove", value);
        }
    }

    private void OnJump(InputValue value)
    {
        foreach (var player in players)
        {
            player.BroadcastMessage("OnJump", value);
        }
    }

    private void OnItemLeft()
    {
        foreach (var player in players)
        {
            player.BroadcastMessage("OnItemLeft");
        }
    }

    private void OnItemRight()
    {
        foreach (var player in players)
        {
            player.BroadcastMessage("OnItemRight");
        }
    }

    private void OnReset()
    {
        GameManager.Instance.ResetLevel();
    }
}