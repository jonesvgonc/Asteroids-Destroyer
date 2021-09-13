using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameObjectsManager : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    //This manager cntains all of prefabs that will be converted to entity, to instantiate a new entity of a game object I need to call the referenced prefabs.

    public GameObject PlayerPrefab;
    public static Entity PlayerEntity;

    public GameObject PlayerAttackPrefab;
    public static Entity PlayerAttackEntity;

    public GameObject EnemyAsteroidPrefab;
    public static Entity EnemyAsteroidEntity;

    public GameObject Enemy2AsteroidPrefab;
    public static Entity Enemy2AsteroidEntity;

    public GameObject Enemy3AsteroidPrefab;
    public static Entity Enemy3AsteroidEntity;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        PlayerEntity = conversionSystem.GetPrimaryEntity(PlayerPrefab);
        PlayerAttackEntity = conversionSystem.GetPrimaryEntity(PlayerAttackPrefab);

        EnemyAsteroidEntity = conversionSystem.GetPrimaryEntity(EnemyAsteroidPrefab);
        Enemy2AsteroidEntity = conversionSystem.GetPrimaryEntity(Enemy2AsteroidPrefab);
        Enemy3AsteroidEntity = conversionSystem.GetPrimaryEntity(Enemy3AsteroidPrefab);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(PlayerPrefab);
        referencedPrefabs.Add(PlayerAttackPrefab);
        referencedPrefabs.Add(EnemyAsteroidPrefab);
        referencedPrefabs.Add(Enemy2AsteroidPrefab);
        referencedPrefabs.Add(Enemy3AsteroidPrefab);
    }
}
