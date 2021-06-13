using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemUI : MonoBehaviour
{
    public ItemData[] items;
    public ItemUI itemUIPrefab;
    public Color neutralColor;
    public Color leftColor;
    public Color rightColor;

    [Header("UI")]
    public TMP_Text title;

    public GameObject canvasParent;
    public GameObject selectionParent;
    public GameObject completeParent;
    public GameObject selectContainer;
    public TMP_Text itemTitle;
    public TMP_Text itemDescription;
    public Button confirmButton;
    public Button backButton;
    public PlayerItemUI leftItem;
    public PlayerItemUI rightItem;

    private Image _image;
    private ItemUI[] _itemsUI;
    private int? _currentlySelectedIndex;
    private int? _selectedForL;
    private int? _selectedForR;

    private void Start()
    {
        _image = GetComponent<Image>();

        foreach (Transform child in selectContainer.transform)
        {
            Destroy(child.gameObject);
        }
        _itemsUI = new ItemUI[items.Length];
        backButton.gameObject.SetActive(false);
        confirmButton.interactable = false;
        for (var i = 0; i < items.Length; i++)
        {
            _itemsUI[i] = Instantiate(itemUIPrefab, selectContainer.transform);
            _itemsUI[i].StartValues(items[i], this, i);
        }

        ChangeToLeft();
    }

    public void SelectItem(int index)
    {
        for (var i = 0; i < _itemsUI.Length; i++)
        {
            if (i != index)
            {
                _itemsUI[i].Unselect();
            }
        }
        _currentlySelectedIndex = index;
        ShowSelected();
        confirmButton.interactable = true;
        if (!_selectedForL.HasValue)
        {
            leftItem.SetImage(items[_currentlySelectedIndex.Value].Icon, true);
        }
        else
        {
            rightItem.SetImage(items[_currentlySelectedIndex.Value].Icon, true);
        }
    }

    public void HoverItem(int index)
    {
        var item = items[index];
        itemTitle.text = item.Name;
        itemDescription.text = item.Description;
    }

    public void ShowSelected()
    {
        itemTitle.text = "";
        itemDescription.text = "";
        if (_currentlySelectedIndex.HasValue)
        {
            var selected = items[_currentlySelectedIndex.Value];
            itemTitle.text = selected.Name;
            itemDescription.text = selected.Description;
        }
    }

    public void Confirm()
    {
        if (!_selectedForL.HasValue)
        {
            _selectedForL = _currentlySelectedIndex;
            _itemsUI[_currentlySelectedIndex.Value].SetUnavailable();
            _currentlySelectedIndex = null;
            ChangeToRight();
        }
        else if (!_selectedForR.HasValue)
        {
            _selectedForR = _currentlySelectedIndex.Value;
            ChangeToLast();
        }
        else
        {
            GameManager.Instance.StartLevel(items[_selectedForL.Value], items[_selectedForR.Value]);
            var anim = canvasParent.GetComponent<Animator>();
            anim.ResetTrigger("End");
            anim.SetTrigger("End");
            StartCoroutine(HideCoroutine());
        }
    }

    private IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        canvasParent.SetActive(false);
    }

    public void Back()
    {
        if (_selectedForL.HasValue & !_selectedForR.HasValue)
        {
            _currentlySelectedIndex = _selectedForL;
            _selectedForL = null;

            _itemsUI[_currentlySelectedIndex.Value].SetAvailable();
            _itemsUI[_currentlySelectedIndex.Value].Select();
            rightItem.ClearImage();
            ChangeToLeft();
            confirmButton.interactable = true;
        }
        else if (_selectedForR.HasValue)
        {
            _currentlySelectedIndex = _selectedForR;
            _selectedForR = null;

            _itemsUI[_currentlySelectedIndex.Value].SetAvailable();
            _itemsUI[_currentlySelectedIndex.Value].Select();
            ChangeToRight();
            confirmButton.interactable = true;
        }
    }

    private void ChangeToLeft()
    {
        selectionParent.SetActive(true);
        completeParent.SetActive(false);
        ShowSelected();
        Color.RGBToHSV(leftColor, out var h, out var s, out var v);
        _image.DOColor(Color.HSVToRGB(h, s - 0.7f, v), 0.5f);
        title.text = $"SELECT YOUR <color=#{ColorUtility.ToHtmlStringRGB(leftColor)}>LEFT</color> ITEM";
        backButton.interactable = false;
        backButton.gameObject.SetActive(false);
    }

    private void ChangeToRight()
    {
        selectionParent.SetActive(true);
        completeParent.SetActive(false);
        ShowSelected();
        backButton.gameObject.SetActive(true);
        backButton.interactable = true;
        confirmButton.interactable = false;
        Color.RGBToHSV(rightColor, out var h, out var s, out var v);

        _image.DOColor(Color.HSVToRGB(h, s - 0.7f, v), 0.5f);
        title.text = $"SELECT YOUR <color=#{ColorUtility.ToHtmlStringRGB(rightColor)}>RIGHT</color> ITEM";
    }

    private void ChangeToLast()
    {
        _image.DOColor(neutralColor, 0.5f);
        completeParent.SetActive(true);
        selectionParent.SetActive(false);
    }
}