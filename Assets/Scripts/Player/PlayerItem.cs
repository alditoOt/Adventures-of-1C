using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public ItemData item;
    private Item _item;
    private PlayerIdentifier _id;
    private bool isUsing = false;

    private void Start()
    {
        _id = GetComponent<PlayerIdentifier>();
        _item = InitializeItem(item);
    }

    private Item InitializeItem(ItemData data)
    {
        if (data == null)
        {
            return null;
        }
        switch (data.Type)
        {
            case ItemType.MOVEMENT:
                return new MovementItem(data);

            default:
                return null;
        }
    }

    private void OnItemLeft()
    {
        if (_id.IsLeft)
        {
            UseItem();
        }
    }

    private void OnItemRight()
    {
        if (!_id.IsLeft)
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        if (!isUsing)
        {
            Debug.Log($"Using item from { (_id.IsLeft ? "left" : "right") } character");
            if (_item != null)
            {
                var other = FindObjectsOfType<PlayerIdentifier>().First(p => p.IsLeft != _id.IsLeft).gameObject;
                _item.Use(gameObject, other);
                isUsing = true;
                StartCoroutine(RestoreCoroutine());
            }
        }
    }

    private IEnumerator RestoreCoroutine()
    {
        yield return new WaitForSeconds(_item.data.Duration);
        var other = FindObjectsOfType<PlayerIdentifier>().First(p => p.IsLeft != _id.IsLeft).gameObject;
        Debug.Log($"Restoring item in { (_id.IsLeft ? "left" : "right") } character");
        _item.Restore(gameObject, other);
        isUsing = false;
    }
}