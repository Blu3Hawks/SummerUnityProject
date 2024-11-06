using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeHandler : MonoBehaviour
{
    private int amountOfClicks = 0;
    private float clickTimer = 0f;
    private bool isCounting = false;
    private const float godModeActivationTime = 2f;
    private const int requiredClicks = 4;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (isCounting)
        {
            clickTimer += Time.deltaTime;

            if (clickTimer > godModeActivationTime)
            {
                ResetGodModeCounter();
            }
        }
    }

    public void CheckGodMode()
    {
        amountOfClicks++;
        Debug.Log(amountOfClicks.ToString());
        if (!isCounting)
        {
            isCounting = true;
            clickTimer = 0f;
        }
        if (amountOfClicks == 1)
        {
            player.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
            player.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        if (amountOfClicks >= requiredClicks && clickTimer <= godModeActivationTime)
        {
            ApplyGodMode();
            ResetGodModeCounter();
        }
    }

    private void ApplyGodMode()
    {
        Debug.Log("God Mode Activated!");
        player.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        player.gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    private void ResetGodModeCounter()
    {
        amountOfClicks = 0;
        clickTimer = 0f;
        isCounting = false;
    }
}
