using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuViewModel : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPrefab;

    private MainMenuView mainMenuView;

    private bool fadingText = false;

    private float timeToFade = 1f;

    public void StartMainMenu()
    {
        var mainMenu = Instantiate(mainMenuPrefab);
        mainMenuView = mainMenu.GetComponent<MainMenuView>();
        mainMenuView.StartNewGameButton.onClick.AddListener(() => StartGame());
        fadingText = true;
        StartCoroutine(FadeInOut());
    }

    public void DestroyMainMenu()
    {
        Destroy(mainMenuView.gameObject);
    }

    public void StartGame()
    {
        DestroyMainMenu();
        UIGameManager.Instance.StartInGameMenu();
        fadingText = false;        
    }   

    private IEnumerator FadeInOut()
    {
        var fadeOut = true;
        while(fadingText)
        {
            if (fadeOut)
            {
                Fade(0f);
                yield return new WaitForSeconds(timeToFade);
                fadeOut = false;
            }else
            {
                Fade(1f);
                yield return new WaitForSeconds(timeToFade);
                fadeOut = true;
            }
        }
    }

    public void Fade(float alpha)
    {
        mainMenuView.StartGameText.CrossFadeAlpha(alpha, timeToFade, false);
    }
}
