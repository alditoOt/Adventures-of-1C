using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public ItemData data;

    public Item(ItemData data)
    {
        this.data = data;
    }

    public abstract void Use(GameObject user, GameObject other);

    public abstract void Restore(GameObject user, GameObject other);
}