using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string prefix = "Score: ";

    // Start is called before the first frame update
    void Start()
    {
        SetScore(GameManager.instance.Score);
        GameManager.instance.ScoreAdded += score => {
            SetScore(score);
        };
    }

    void SetScore(int score) {
        text.SetText(prefix + score);
    }


}
