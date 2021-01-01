using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeIndicator : MonoBehaviour
{
    public Sprite[] sprites;
    protected Image myImage;
    // Start is called before the first frame update
    void Awake()
    {
        myImage = this.GetComponent<Image>();
    }

    public void ChangeGraphic(int id)
    {
        myImage.sprite = sprites[id];
    }
}
