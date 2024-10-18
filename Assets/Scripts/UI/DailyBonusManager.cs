using UnityEngine;
using System;

public class DailyBonusManager : MonoBehaviour
{
    public PlayerData playerData;
    private DateTime lastLoginDate;

    private void Start()
    {
        CheckDailyBonus();
    }

    public void CheckDailyBonus()
    {
        lastLoginDate = DateTime.Parse(PlayerPrefs.GetString("lastLoginDate", DateTime.Now.ToString()));
        DateTime today = DateTime.Now;

        if (lastLoginDate.Date != today.Date)
        {
            GiveDailyBonus();
            PlayerPrefs.SetString("lastLoginDate", today.ToString());
        }
    }

    private void GiveDailyBonus()
    {
        playerData.totalScore += 100; 
        Debug.Log("Daily bonus awarded!");

        SaveSystem.SavePlayer(playerData);
    }

    public void SendNotificationIfMissed()
    {

    }
}
