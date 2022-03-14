using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerAbility : MonoBehaviour
{
    public float currentEnergy;
    public float maxEnergy = 80f;
    public float currentHealth;
    public float maxHealth = 100f;

    private bool absorptionEnergy = false;
    private bool giveEnergy = false;

    [SerializeField] private float energyCost = 20f;

    [SerializeField] private ParticleSystem inEffect;
    [SerializeField] private ParticleSystem outEffect;

    [SerializeField] private GameObject effect;

    private bool canAbsorbEnergy;

    public bool isPlayerDied = false;

    public Canvas DeathCanvas;

    void Start()
    {
        //currentEnergy = maxEnergy;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Enerji();
        PlayerDie();
    }

    void Enerji()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            absorptionEnergy = true;
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            absorptionEnergy = false;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            giveEnergy = true;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            giveEnergy = false;
        }
    }

    void UIHandler()
    {
        //Enerji ve can gösterim iþlemleri
    }

    void EnergyAbsorption(float amountOfEnergy)
    {
        currentEnergy += amountOfEnergy;
    }

    void EnergyDecrease(float amountOfEnergy)
    {
        currentEnergy -= amountOfEnergy;
    }

    void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.EnergyItem_TAG))
        {
            var obj = collision.gameObject.GetComponent<EnergyItem>();
            Debug.Log("Show Button");
            effect.SetActive(true);
            if (obj.amountOfEnergy <= 0)
            {
                obj.isHaveEnergy = false;
            }
            else
            {
                obj.isHaveEnergy = true;
            }

        }


    }

    private void PlayerDie()
    {
        if (currentEnergy > 80)
        {
            isPlayerDied = true;
        }

        if (isPlayerDied)
        {
            Time.timeScale = 0.2f;
            DeathCanvas.transform.LeanMoveLocalY(0,0.2f);
            StartCoroutine(reloadDelayer());
        }
    }

    IEnumerator reloadDelayer()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        var obj = collision.gameObject.GetComponent<EnergyItem>();
        if (absorptionEnergy && obj.isHaveEnergy)
        {
            Instantiate(inEffect, this.transform.position,Quaternion.identity);
            SoundManager.instance.PlayFX(SoundManager.instance.Absorptionfx);
            EnergyAbsorption(obj.amountOfEnergy);
            obj.EnergyDecrease(obj.amountOfEnergy);
        }

        if (giveEnergy)
        {
            float value = currentEnergy - obj.baseEnergy;
            if (obj.amountOfEnergy < obj.baseEnergy && value >= 0)
            {
                Instantiate(outEffect, this.transform.position, Quaternion.identity);
                SoundManager.instance.PlayFX(SoundManager.instance.Deabsorptionfx);
                EnergyDecrease(obj.baseEnergy);
                obj.TakeEnergy(obj.baseEnergy);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.EnergyItem_TAG))
        {
            effect.SetActive(false);

        }
    }


}
