using System.Collections;
using Unity.Entities;
using UnityEngine;

public class MainMenuViewModel : MonoBehaviour, IConvertGameObjectToEntity
{
    //This is the Main Menu View Model, all of the Main Menu UI rules and methods are concentrated here.

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
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var newEntity = entityManager.CreateEntity();
        entityManager.AddComponent<StartGameFlag>(newEntity);
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

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        
    }
}
