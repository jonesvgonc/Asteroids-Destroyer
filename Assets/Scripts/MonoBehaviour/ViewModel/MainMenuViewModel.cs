using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuViewModel : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPrefab;

    private MainMenuView mainMenuView;

    public void StartMainMenu()
    {
        var mainMenu = Instantiate(mainMenuPrefab);
        mainMenuView = mainMenu.GetComponent<MainMenuView>();
    }

    public void DestroyMainMenu()
    {
        Destroy(mainMenuView.gameObject);
    }

    public void StartGame()
    {
        DestroyMainMenu();
        UIGameManager.Instance.StartInGameMenu();
    }
}
