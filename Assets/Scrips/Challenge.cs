using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Challenge : MonoBehaviour
{
    public Text Coin;
    public Text Time;
    private void Start()
    {
        Coin.text = "Total Coin: " + PlayerPrefs.GetInt("Coin");
        float times = PlayerPrefs.GetFloat("Time");
        string timestring = "";
        if (times < 10)
        {
            timestring = times.ToString("0");
        }
        else if (times < 100)
        {
            timestring = times.ToString("00");
        }
        else
        {
            timestring = times.ToString("000");
        }
        Time.text = "Time: " + timestring + " seconds";
    }
}