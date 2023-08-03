using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    [SerializeField] private GameObject hourIndicatorPrefab;
    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand;
    [SerializeField] private Transform secondHand;

    void Start()
    {
        for(int i = 0; i<12;  i++) {
            var inst = GameObject.Instantiate(hourIndicatorPrefab);
            inst.transform.Rotate(0, i * 30f, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        var time = DateTime.Now.TimeOfDay;
        hourHand.rotation = Quaternion.Euler(0, 30 * (float)time.TotalHours, 0);
        minuteHand.rotation = Quaternion.Euler(0, 6 * (float)time.TotalMinutes, 0);
        secondHand.rotation = Quaternion.Euler(0, 6 * (float)time.TotalSeconds, 0);
    }
}
