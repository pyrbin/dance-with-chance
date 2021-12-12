using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Sprite ThreeLivesSprite;
    public Sprite TwoLivesSprite;
    public Sprite OneLifeSprite;

    public Image image;


    void Update()
    {
        if (GameManager.instance.CurrentHealth == 3) image.sprite = ThreeLivesSprite;
        if (GameManager.instance.CurrentHealth == 2) image.sprite = TwoLivesSprite;
        if (GameManager.instance.CurrentHealth == 1) image.sprite = OneLifeSprite;
    }
}
