using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameViewModel : MonoBehaviour
{
    [SerializeField]
    private GameObject inGameMenuPrefab;

    private InGameView inGameView;

    public void StartInGameUI()
    {
        var inGameUI = Instantiate(inGameMenuPrefab);
        inGameView = inGameUI.GetComponent<InGameView>();
    }

    public void EndGame()
    {
        Destroy(inGameView.gameObject);
        UIGameManager.Instance.StartMainMenu();
    }
}
