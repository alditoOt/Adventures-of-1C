using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class ItemData : ScriptableObject
{
    public ItemType Type;
    public string Name;
    public string Description;
    public Sprite Icon;
    public float Duration;

    [Header("Movement Attributes")]
    public float speed = -1;

    public float jump = -1;
    public float fallMultiplier = -1;
    public float lowJumpMultiplier = -1;
}

public enum ItemType
{
    MOVEMENT
}