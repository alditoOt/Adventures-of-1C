using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    private void OnReset()
    {
        GameManager.Instance.ResetLevel();
    }
}