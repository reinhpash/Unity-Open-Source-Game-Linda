using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    private GameObject playerObj;
    public PlayerAbility player;
    public float playerTime;
    public static GameManager _instance;
    Random rnd;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code
            playerObj = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
            player = playerObj.GetComponent<PlayerAbility>();
            if (playerTime > 0)
            {
                TimerController.instance.elapsedTime += playerTime;
            }
            
        }
        else
        {
            Destroy(this);

        }
    }
}
