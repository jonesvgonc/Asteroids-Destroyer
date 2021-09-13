using Unity.Entities;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class GameOverSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<GameOverFlag>();
    }

    protected override void OnUpdate()
    {
        EntityManager.AddComponent<DestroyFlag>(GetSingletonEntity<PlayerComponent>());
        EntityManager.AddComponent<DestroyFlag>(GetSingletonEntity<GameOverFlag>());
        EntityManager.AddComponent<DestroyFlag>(GetSingletonEntity<GameDataComponent>());

        Entities
                .ForEach((Entity entity,
                ref EnemyWaveStartFlag enemyWaveFlag) =>
                {
                    EntityManager.AddComponent<DestroyFlag>(entity);
                })
                .WithStructuralChanges()
                .WithoutBurst()
                .Run();

        Entities
                .ForEach((Entity entity,
                ref EnemyComponent enemyData) =>
                {
                    EntityManager.AddComponent<DestroyFlag>(entity);
                })
                .WithStructuralChanges()
                .WithoutBurst()
                .Run();
        UIGameManager.Instance.GameOver();
    }
}
