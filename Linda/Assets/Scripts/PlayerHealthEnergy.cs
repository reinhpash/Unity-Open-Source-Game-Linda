using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthEnergy : MonoBehaviour
{
    [SerializeField] private PlayerAbility player;

    [SerializeField] private Image HealthBar;
    [SerializeField] private Image EnergyBar;
    [SerializeField] private TextMeshProUGUI energyText;
    string doldurma = "/80";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnergyBar.fillAmount = player.currentEnergy / 100f;
        energyText.text = player.currentEnergy + doldurma;
    }
}
