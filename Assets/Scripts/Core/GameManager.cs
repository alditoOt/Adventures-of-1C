using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public UnityEvent LevelCompleted;
    public UnityEvent<ItemData, ItemData> LevelStarted;

    private bool _leftComleted = false;
    private bool _rightComleted = false;

    private void Start()
    {
        if (LevelCompleted == null)
            LevelCompleted = new UnityEvent();
        if (LevelStarted == null)
            LevelStarted = new UnityEvent<ItemData, ItemData>();
    }

    public void StartLevel(ItemData left, ItemData right)
    {
        LevelStarted.Invoke(left, right);
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
            SceneOperator.Instance.LoadNextScene();
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

    public void ResetLevel()
    {
        SceneOperator.Instance.ResetScene();
    }
}