using TMPro;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    public TextMeshProUGUI Text;

    void Update()
    {
        Text.SetText($"{GameManager.ComboCounter}/{GameManager.ComboRound(GameManager.ComboCounter)}");
    }
}
