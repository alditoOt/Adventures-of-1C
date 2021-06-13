using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.ChangeBackgroundMusic("Menu");
    }

    public void StartGame()
    {
        AudioManager.Instance.Play("UseItem");
        AudioManager.Instance.ChangeBackgroundMusic("Game");
        GameManager.Instance.StartGame();
    }

    public void Exit()
    {
        Application.Quit();
    }
}