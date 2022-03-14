using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Detector : MonoBehaviour
{
    GameManager manager;
    float winTime;

    public TextMeshProUGUI timeCounter;
    private TimeSpan timePlaying;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager != null)
        {
            winTime = manager.playerTime;
            timePlaying = TimeSpan.FromSeconds(winTime);
            string timePlayingStr = "Süre: " + timePlaying.ToString(@"mm\:ss");
            Debug.Log(timePlaying.Minutes +" "+ timePlaying.Seconds);
            timeCounter.text = timePlayingStr;
        }
    }
}
