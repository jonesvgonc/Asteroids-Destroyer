using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    //This is the manager of all UI, he knows all of the View Models and start them when it is called;

    public static UIGameManager Instance;

    [SerializeField]
    private InGameViewModel inGameViewModel;
    [SerializeField]
    private MainMenuViewModel mainMenuViewModel;

    public Vector2 ScreenBounds;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

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
