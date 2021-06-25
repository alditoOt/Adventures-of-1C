using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCameraInitializer : MonoBehaviour
{
    public bool isLeft = false;
    private CinemachineVirtualCamera _vCam;
    private CinemachineConfiner _confiner;

    private void Awake()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _confiner = GetComponent<CinemachineConfiner>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        var players = FindObjectsOfType<PlayerIdentifier>();
        _vCam.Follow = players.First(p => p.IsLeft == isLeft).transform;
        var bounds = FindObjectsOfType<BoundsIdentifier>();
        _confiner.m_BoundingShape2D = bounds.First(p => p.IsLeft == isLeft).GetComponent<Collider2D>();
    }
}