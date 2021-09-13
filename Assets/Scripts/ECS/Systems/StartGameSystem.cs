using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class StartGameSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<StartGameFlag>();
    }
    protected override void OnUpdate()
    {
        var gameData = EntityManager.CreateEntity();
        EntityManager.AddComponentData(gameData, new GameDataComponent() { Bounds = UIGameManager.Instance.ScreenBounds, Level = 1, Lives = 3, Score = 0 });

        UIGameManager.Instance.SetScore(0);
        UIGameManager.Instance.SetLives(3);
        UIGameManager.Instance.SetLevel(1);
        var player = EntityManager.Instantiate(GameObjectsManager.PlayerEntity);

        var newEntity = EntityManager.CreateEntity();
        EntityManager.AddComponent<EnemyWaveStartFlag>(newEntity);

        EntityManager.SetComponentData(player, new Translation
        {
            Value = new float3(0, -3.5f, 0)
        });

        EntityManager.DestroyEntity(GetSingletonEntity<StartGameFlag>());
    }
}
