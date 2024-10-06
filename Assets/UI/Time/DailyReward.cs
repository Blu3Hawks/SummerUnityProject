using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyReward : MonoBehaviour
{
    private void Start()
    {
        // Get the current date and time
        DateTime currentDateTime = DateTime.Now;

        // Output the current date and time to the console
        Debug.Log("Current Date and Time: " + currentDateTime);

        // You can also access specific components
        Debug.Log("Year: " + currentDateTime.Year);
        Debug.Log("Month: " + currentDateTime.Month);
        Debug.Log("Day: " + currentDateTime.Day);
        Debug.Log("Hour: " + currentDateTime.Hour);
        Debug.Log("Minute: " + currentDateTime.Minute);
        Debug.Log("Second: " + currentDateTime.Second);
    }
}
