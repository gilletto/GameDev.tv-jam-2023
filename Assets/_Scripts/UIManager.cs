using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] TMP_Text _death_text;
    [SerializeField] TMP_Text _gem_text;
    [SerializeField] TMP_Text _key_text;
    [SerializeField] TMP_Text _level_text;


    public void UpdateLevel(int value)
    {
        _level_text.text = "Level " + value;
    }
    public void UpdateDeaths(int amount) {
        _death_text.text = "Deaths: " + amount;
    
    }

    public void UpdateKey(bool value)
    {
        _key_text.text = "Key " + value;
    }

    public void UpdateGems(int amount)
    {
        _gem_text.text =  "Gems: " + amount;
    }
}
