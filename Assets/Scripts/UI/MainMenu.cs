﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void Exit()
    {
        Application.Quit();
    }
}