using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemUI : MonoBehaviour
{
    public Image frontImage;
    public Image backImage;
    public Color playerColor;

    [Header("UI")]
    public Image border;

    public Image backGround;

    private void Start()
    {
        border.color = playerColor;
        backGround.color = new Color(playerColor.r, playerColor.g, playerColor.b, backGround.color.a);
    }

    public void SetImage(Sprite image, bool animate = false)
    {
        frontImage.enabled = true;
        backImage.enabled = true;
        frontImage.sprite = image;
        backImage.sprite = image;
        backImage.color = new Color(backImage.color.r, backImage.color.g, backImage.color.b, 0.3f);
        RestoreImage();
        if (animate)
        {
            frontImage.transform.localScale = Vector3.zero;
            frontImage.transform.DOScale(1f, 0.3f);
            backImage.transform.localScale = Vector3.zero;
            backImage.transform.DOScale(1f, 0.3f);
        }
    }

    public void ClearImage()
    {
        frontImage.enabled = false;
        backImage.enabled = false;
        frontImage.sprite = null;
        backImage.sprite = null;
    }

    public void HideImage(float duration)
    {
        frontImage.DOFillAmount(0, duration);
    }

    public void RestoreImage()
    {
        frontImage.fillAmount = 1;
    }
}