using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyItem : MonoBehaviour
{
    public float amountOfEnergy = 20f;
    public float baseEnergy = 20f;
    public bool isHaveEnergy = true;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (amountOfEnergy > 0)
        {
            isHaveEnergy = true;
        }
    }


   public void EnergyDecrease(float energy)
    {
        if (energy > amountOfEnergy)
        {
            amountOfEnergy = 0;
        }
        else
        {
            amountOfEnergy -= energy;
        }
        CheckEnergy();
    }

    public void TakeEnergy(float energy)
    {
        if (amountOfEnergy < baseEnergy && amountOfEnergy >= 0)
        {
            if (energy > amountOfEnergy)
            {
                amountOfEnergy = energy;
            }
            else
            {
                amountOfEnergy += energy;
            }
        }
        CheckEnergy();
    }

    void CheckEnergy()
    {
        if (amountOfEnergy <= 0)
        {
            isHaveEnergy = false;
        }
        else
        {
            isHaveEnergy = true;
        }

    }
}
