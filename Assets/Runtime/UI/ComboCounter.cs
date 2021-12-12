using TMPro;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    public TextMeshProUGUI Text;

    void Update()
    {
        Text.SetText($"{GameManager.instance.ComboCounter % GameManager.Settings.ComboTarget}/{GameManager.Settings.ComboTarget}");
    }
}
