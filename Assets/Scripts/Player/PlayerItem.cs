using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    public ItemData item;
    public PlayerItemUI itemUI;
    private Item _item;
    private PlayerIdentifier _id;
    private bool isAvailable = false;
    private bool wasUsed = false;

    private void Start()
    {
        _id = GetComponent<PlayerIdentifier>();
        GameManager.Instance.LevelStarted.AddListener((left, right) =>
        {
            _item = InitializeItem(_id.IsLeft ? left : right);
        });
    }

    private Item InitializeItem(ItemData data)
    {
        if (data == null || itemUI == null)
        {
            return null;
        }
        isAvailable = true;
        itemUI.gameObject.SetActive(true);
        itemUI.SetImage(data.Icon);
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
        if (isAvailable)
        {
            Debug.Log($"Using item from { (_id.IsLeft ? "left" : "right") } character");
            if (_item != null)
            {
                var other = FindObjectsOfType<PlayerIdentifier>().First(p => p.IsLeft != _id.IsLeft).gameObject;

                AudioManager.Instance.Play("UseItem");
                _item.Use(gameObject, other);
                isAvailable = false;
                StartCoroutine(RestoreCoroutine());
            }
        }
    }

    private IEnumerator RestoreCoroutine()
    {
        itemUI.HideImage(_item.data.Duration);
        yield return new WaitForSeconds(_item.data.Duration);
        var other = FindObjectsOfType<PlayerIdentifier>().First(p => p.IsLeft != _id.IsLeft).gameObject;
        Debug.Log($"Restoring item in { (_id.IsLeft ? "left" : "right") } character");
        AudioManager.Instance.Play("RestoreItem");
        _item.Restore(gameObject, other);
        wasUsed = true;
    }
}