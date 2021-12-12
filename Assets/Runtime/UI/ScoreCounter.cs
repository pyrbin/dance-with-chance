using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string prefix = "Score: ";
    void Update()
    {
        text.SetText(prefix + GameManager.Score);
    }
}
