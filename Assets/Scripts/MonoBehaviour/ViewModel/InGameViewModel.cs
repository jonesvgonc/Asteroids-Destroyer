using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

public class InGameViewModel : MonoBehaviour, IConvertGameObjectToEntity
{
    //This is the In Game View Model, all of the In Game UI rules and methods are concentrated here.

    [SerializeField]
    private GameObject inGameMenuPrefab;

    private InGameView inGameView;

    private EntityManager entityManager;

    private bool accelerating = false;
    private bool shoting = false;

    private bool rotatingClockwise = false;
    private bool rotatingCounterClockwise = false;

    public void StartInGameUI()
    {
        var inGameUI = Instantiate(inGameMenuPrefab);
        inGameView = inGameUI.GetComponent<InGameView>();

        //This is the first time that I have created callbacks to ECS in Mobile from the UI, I have more experiences using the input of keyboard to create these controls
        inGameView.AccelerateTrigger.triggers.Add(NewTriggerEntry("Accelerate", true));
        inGameView.AccelerateTrigger.triggers.Add(NewTriggerEntry("Accelerate", false));
        inGameView.ShotTrigger.triggers.Add(NewTriggerEntry("Shot", true));
        inGameView.ShotTrigger.triggers.Add(NewTriggerEntry("Shot", false));
        inGameView.RotateClockwiseTrigger.triggers.Add(NewTriggerEntry("RotateClock", true));
        inGameView.RotateClockwiseTrigger.triggers.Add(NewTriggerEntry("RotateClock", false));
        inGameView.RotateCounterClockwiseTrigger.triggers.Add(NewTriggerEntry("RotateCounterClock", true));
        inGameView.RotateCounterClockwiseTrigger.triggers.Add(NewTriggerEntry("RotateCounterClock", false));

        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    public EventTrigger.Entry NewTriggerEntry(string commandType, bool pointerDown)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        if(pointerDown)
            entry.eventID = EventTriggerType.PointerDown;
        else
            entry.eventID = EventTriggerType.PointerUp;

        switch (commandType)
        {
            case "Accelerate":
                entry.callback.AddListener((data) => { Accelerate((PointerEventData)data); });
                break;
            case "Shot":
                entry.callback.AddListener((data) => { Shot((PointerEventData)data); });
                break;
            case "RotateClock":
                entry.callback.AddListener((data) => { RotateClock((PointerEventData)data); });
                break;
            case "RotateCounterClock":
                entry.callback.AddListener((data) => { RotateCounterClock((PointerEventData)data); });
                break;
            default:
                break;
        }

        return entry;
    }

    public void Update()
    {
        if (accelerating)
        {
            var entity = entityManager.CreateEntity();
            entityManager.AddComponent<AccelerateFlag>(entity);
        }
        if (rotatingClockwise)
        {
            var entity = entityManager.CreateEntity();
            entityManager.AddComponentData(entity, new RotateFlag() { Direction = -1 });
        }
        if (rotatingCounterClockwise)
        {
            var entity = entityManager.CreateEntity();
            entityManager.AddComponentData(entity, new RotateFlag() { Direction = 1 });
        }
        if (shoting)
        {
            var entity = entityManager.CreateEntity();
            entityManager.AddComponent<ShotFlag>(entity);
        }
    }

    public void EndGame()
    {
        Destroy(inGameView.gameObject);
        UIGameManager.Instance.StartMainMenu();
    }

    public void Shot(PointerEventData data)
    {
        shoting = !shoting; 
    }

    public void Accelerate(PointerEventData data)
    {
        accelerating = !accelerating;
    }

    public void RotateClock(PointerEventData data)
    {
        rotatingClockwise = !rotatingClockwise;
    }

    public void RotateCounterClock(PointerEventData data)
    {
        rotatingCounterClockwise = !rotatingCounterClockwise;
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        
    }
}
