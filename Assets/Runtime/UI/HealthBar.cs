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

    // Start is called before the first frame update
    void Start()
    {
        SetHealth(GameManager.instance.Health);
        GameManager.instance.MissedNote += Note => {
            SetHealth(GameManager.instance.CurrentHealth);
        };
    }

    void SetHealth(int health) {
        if (health == 3) image.sprite = ThreeLivesSprite;
        if (health == 2) image.sprite = TwoLivesSprite;
        if (health == 1) image.sprite = OneLifeSprite;
    }


}
