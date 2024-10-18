using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selectGameMenu;
    public GameObject newGameMenu;

    public GameObject enterNameHere;

    public void DisableMenus()
    {
        mainMenu.SetActive(false);
        selectGameMenu.SetActive(false);
        newGameMenu.SetActive(false);
        enterNameHere.SetActive(false);
    }
    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
    }
    public void EnableSelectGameMenu()
    {
        selectGameMenu.SetActive(true);
    }

    public void EnableNewGameMenu()
    {
        newGameMenu.SetActive(true);
        enterNameHere.SetActive(true);
    }
}
