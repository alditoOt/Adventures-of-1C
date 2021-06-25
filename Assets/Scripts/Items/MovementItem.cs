using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementItem : Item
{
    private PreviousValues _previousValues;

    public MovementItem(ItemData data) : base(data)
    { }

    private class PreviousValues
    {
        public float speed;
        public float jump;
        public float fallMultiplier;
        public float lowJumpMultiplier;

        public PreviousValues(PlayerMovement movement)
        {
            speed = movement.speed;
            jump = movement.jump;
            fallMultiplier = movement.fallMultiplier;
            lowJumpMultiplier = movement.lowJumpMultiplier;
        }
    }

    public override void Use(GameObject user, GameObject other)
    {
        var movement = user.GetComponent<PlayerMovement>();
        _previousValues = new PreviousValues(movement);
        movement.speed = data.speed >= 0 ? data.speed : movement.speed;
        movement.jump = data.jump >= 0 ? data.jump : movement.jump;
        movement.fallMultiplier = data.fallMultiplier >= 0 ? data.fallMultiplier : movement.fallMultiplier;
        movement.lowJumpMultiplier = data.lowJumpMultiplier >= 0 ? data.lowJumpMultiplier : movement.lowJumpMultiplier;
    }

    public override void Restore(GameObject user, GameObject other)
    {
        var movement = user.GetComponent<PlayerMovement>();
        movement.speed = _previousValues.speed;
        movement.jump = _previousValues.jump;
        movement.fallMultiplier = _previousValues.fallMultiplier;
        movement.lowJumpMultiplier = _previousValues.lowJumpMultiplier;
        _previousValues = null;
    }
}