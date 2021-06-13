using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Sprite buttonNormal;
    public Sprite buttonSelected;
    public Sprite buttonHover;

    private bool _selected;
    private ItemData _data;
    private SelectItemUI _parent;
    private int _index;
    private bool _unavailable;

    private Image _buttonImage;

    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        _buttonImage.sprite = buttonNormal;
        _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 0f);
    }

    public void SetUnavailable()
    {
        _unavailable = true;
        _buttonImage.sprite = buttonNormal;
        _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 0f);
        GetComponent<CanvasGroup>().alpha = 0.3f;
    }

    public void SetAvailable()
    {
        _unavailable = false;
        GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void StartValues(ItemData data, SelectItemUI parent, int index)
    {
        _data = data;
        icon.sprite = _data.Icon;
        _parent = parent;
        _index = index;
    }

    public void Select()
    {
        if (_unavailable)
        {
            return;
        }
        _selected = true;
        _buttonImage.sprite = buttonSelected;
        _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 1f);
        _parent.SelectItem(_index);
    }

    public void Unselect()
    {
        if (_unavailable)
        {
            return;
        }
        _selected = false;
        _buttonImage.sprite = buttonNormal;
        _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_unavailable)
        {
            return;
        }
        _parent.HoverItem(_index);
        if (!_selected)
        {
            _buttonImage.sprite = buttonHover;
            _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_unavailable)
        {
            return;
        }
        _parent.ShowSelected();
        _buttonImage.sprite = _selected ? buttonSelected : buttonNormal;
        if (!_selected)
        {
            _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 0f);
        }
        else
        {
            _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 1f);
        }
    }
}