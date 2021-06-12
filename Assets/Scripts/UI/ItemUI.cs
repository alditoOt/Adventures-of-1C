using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image frontImage;
    public Image backImage;

    public void SetImage(Sprite image)
    {
        frontImage.enabled = true;
        backImage.enabled = true;
        frontImage.sprite = image;
        backImage.sprite = image;
        backImage.color = new Color(backImage.color.r, backImage.color.g, backImage.color.b, 0.3f);
        RestoreImage();
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