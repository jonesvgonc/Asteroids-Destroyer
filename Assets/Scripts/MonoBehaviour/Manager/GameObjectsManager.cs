using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameObjectsManager : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    //This manager cntains all of prefabs that will be converted to entity, to instantiate a new entity of a game object I need to call the referenced prefabs.

    public GameObject PlayerPrefab;
    public static Entity PlayerEntity;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        PlayerEntity = conversionSystem.GetPrimaryEntity(PlayerPrefab);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(PlayerPrefab);
    }
}
