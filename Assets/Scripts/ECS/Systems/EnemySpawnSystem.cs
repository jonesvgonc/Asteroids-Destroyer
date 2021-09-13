using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class EnemySpawnSystem : SystemBase
{
    private Random _random;

    protected override void OnCreate()
    {
        base.OnCreate();
        _random = new Random(0xab4523);
        RequireSingletonForUpdate<EnemyWaveStartFlag>();
    }

    protected override void OnUpdate()
    {
        EntityManager.DestroyEntity(GetSingletonEntity<EnemyWaveStartFlag>());

        var gameData = GetSingleton<GameDataComponent>();

        var enemyAmmount = gameData.Level * 5;        

        var random = new Random(_random.NextUInt());

        var bounds = gameData.Bounds;

        gameData.EnemiesAmmount = enemyAmmount;

        SetSingleton(gameData);

        for (var index = 0; index < enemyAmmount; index++)
        {
            var enemy = EntityManager.Instantiate(GameObjectsManager.EnemyAsteroidEntity);
            var enemyPos = new float3(random.NextFloat(-bounds.x, bounds.x), random.NextFloat(-bounds.y, bounds.y), 0);
            var enemyRotationSpeed = random.NextInt(1, 10);
            var enemyDirection = new float3(random.NextInt(-1, 2), random.NextInt(-1, 2), 0);

            var enemyComponent = EntityManager.GetComponentData<EnemyComponent>(enemy);
            enemyComponent.Direction = enemyDirection;
            enemyComponent.RotationSpeed = enemyRotationSpeed;
            enemyComponent.Speed = random.NextInt(1, 6);

            EntityManager.SetComponentData(enemy, enemyComponent);

            EntityManager.SetComponentData(enemy, new Translation
            {
                Value = enemyPos
            });
        }
    }
}
