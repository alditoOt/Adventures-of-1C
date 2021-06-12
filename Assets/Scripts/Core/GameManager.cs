using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public UnityEvent LevelCompleted;

    private bool _leftComleted = false;
    private bool _rightComleted = false;

    private void Start()
    {
        if (LevelCompleted == null)
            LevelCompleted = new UnityEvent();
    }

    public void CompletedDoor(bool left)
    {
        if (left)
        {
            _leftComleted = true;
        }
        else
        {
            _rightComleted = true;
        }

        if (_leftComleted && _rightComleted)
        {
            LevelCompleted.Invoke();
            Debug.Log("Finished");
        }
    }

    public void ExitedDoor(bool left)
    {
        if (left)
        {
            _leftComleted = false;
        }
        else
        {
            _rightComleted = false;
        }
    }
}