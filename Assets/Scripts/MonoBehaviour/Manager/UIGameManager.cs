using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager Instance;

    [SerializeField]
    private InGameViewModel inGameViewModel;
    [SerializeField]
    private MainMenuViewModel mainMenuViewModel;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        StartMainMenu();
    }

    public void StartInGameMenu()
    {
        inGameViewModel.StartInGameUI();
    }

    public void StartMainMenu()
    {
        mainMenuViewModel.StartMainMenu();
    }
}
