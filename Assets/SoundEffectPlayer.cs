using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{

    public AudioSource combo;
    public AudioSource hit;
    public AudioSource miss;
    public AudioSource specialCombo;
    public int specialComboOccurence = 3;
    private int currCombos = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.HitNote += _ => hit.Play();
        GameManager.instance.MissedNote += _ => miss.Play();
        GameManager.instance.ComboHappened +=  comboCounter => {
            currCombos++;
            if (currCombos % specialComboOccurence == 0) specialCombo.Play();
            else combo.Play();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
