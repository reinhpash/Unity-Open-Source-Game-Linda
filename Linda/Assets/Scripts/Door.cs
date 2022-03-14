using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{
    Animator anim;
    public int energyNeedToOpen;
    private int CurrentEnergy;
    [SerializeField] EnergyItem energy;
    [SerializeField] TextMeshProUGUI energyText;
    private string doldurma = "/80";
    private bool bolumBittiMi = false;
    public bool isLastScene;
    enum States
    {
        Close,
        Open
    }
    private States state;
    // Start is called before the first frame update
    void Start()
    {
        state = States.Close;
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        energyText.text = energy.amountOfEnergy + doldurma;

        if (energy.amountOfEnergy == energy.baseEnergy)
        {
            state = States.Open;
            anim.SetTrigger("DoorOpening");
            StartCoroutine(delayer());
            bolumBittiMi = true;
            //this.StopAllCoroutines();
        }
        else
        {
            state = States.Close;
            anim.SetBool("isDoorOpen", false);
        }

        if (bolumBittiMi)
        {
            TimerController.instance.EndTimer();
            Invoke("LoadNextScene",1.5f);
        }
    }

    void LoadNextScene()
    {
        if (!isLastScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    IEnumerator delayer()
    {

        yield return new WaitForSeconds(.5f);
        Debug.Log("Opening");
        anim.SetBool("isDoorOpen", true);
        this.StopAllCoroutines();
    }

}
