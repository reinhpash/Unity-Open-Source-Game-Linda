using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    [SerializeField] private EnergyItem energy;
    [SerializeField]private Sprite defaultSprite,energySprite;
    SpriteRenderer sr;

    enum States
    {
        defaultState,
        energyState
    }
    private States state;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (energy.isHaveEnergy)
        {
            sr.sprite = defaultSprite;
            state = States.defaultState;
        }
        else if (true)
        {
            sr.sprite = energySprite;
            state = States.energyState;
        }
    }
}
