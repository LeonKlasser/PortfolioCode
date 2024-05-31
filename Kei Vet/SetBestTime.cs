using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetBestTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;
    [SerializeField] private TextMeshProUGUI text4;
    [SerializeField] private TextMeshProUGUI text5;

    private List<float> _times; 

    private void Start()
    {
        _times = new List<float>();

        if (PlayerPrefs.HasKey("BestTimeOne") || PlayerPrefs.GetFloat("BestTimeOne") <= 0.1f)
            _times.Add(PlayerPrefs.GetFloat("BestTimeOne"));
        else
            _times.Add(900f);
        if (PlayerPrefs.HasKey("BestTimeTwo") || PlayerPrefs.GetFloat("BestTimeTwo") <= 0.1f)
            _times.Add(PlayerPrefs.GetFloat("BestTimeTwo"));
        else
            _times.Add(900f);
        if (PlayerPrefs.HasKey("BestTimeThree") || PlayerPrefs.GetFloat("BestTimeThree") <= 0.1f)
            _times.Add(PlayerPrefs.GetFloat("BestTimeThree"));
        else
            _times.Add(900f);
        if (PlayerPrefs.HasKey("BestTimeFour") || PlayerPrefs.GetFloat("BestTimeFour") <= 0.1f)
            _times.Add(PlayerPrefs.GetFloat("BestTimeFour"));
        else
            _times.Add(900f);
        if (PlayerPrefs.HasKey("BestTimeFive") || PlayerPrefs.GetFloat("BestTimeFive") <= 0.1f)
            _times.Add(PlayerPrefs.GetFloat("BestTimeFive"));
        else
            _times.Add(900f);
        

        _times.Sort();



        SetTime(text1, _times[0]);
        SetTime(text2, _times[1]);
        SetTime(text3, _times[2]);
        SetTime(text4, _times[3]);
        SetTime(text5, _times[4]);
    }

    private void SetTime(TextMeshProUGUI text, float time)
    {
        if (time is 900f or < 0.1f)
        {
            text.text = "Time not set";
        }
        else
        {
            float _minutes = (int)(time / 60f) % 60;
            float _seconds = (int)(time % 60f);
            float _milliseconds = (int)(time * 1000f) % 1000;
            text.text = _minutes.ToString("00") + ":" + _seconds.ToString("00") + ":" + _milliseconds.ToString("00");
        }
    }
}
