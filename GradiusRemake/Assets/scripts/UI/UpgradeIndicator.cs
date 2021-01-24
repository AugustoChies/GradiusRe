using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeIndicator : MonoBehaviour
{
    public int currentImageId;
    public Sprite[] sprites;
    protected Image myImage;
    void Awake()
    {
        myImage = this.GetComponent<Image>();
    }

    public void ChangeGraphic(int id)
    {
        currentImageId = id;
        myImage.sprite = sprites[id];        
    }
}
