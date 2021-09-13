using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class DivideEnemySystem : SystemBase
{
    private EntityQuery divideEnemyQuery;

    private Random _random;

    protected override void OnCreate()
    {
        base.OnCreate();
        divideEnemyQuery = GetEntityQuery(typeof(DividedEnemyComponent));
        _random = new Random(0xab4523);
        RequireForUpdate(divideEnemyQuery);
    }

    protected override void OnUpdate()
    {
        var random = new Random(_random.NextUInt());

        var gameData = GetSingleton<GameDataComponent>();
        var bounds = gameData.Bounds;

        Entities
                .ForEach((Entity enemyEntity,
                ref DividedEnemyComponent divideEnemy) =>
                {
                    if (!divideEnemy.Created)
                    {
                        divideEnemy.Created = true;
                        EntityManager.AddComponent<DestroyFlag>(enemyEntity);
                        for (var index = 0; index < 2; index++)
                        {
                            var newEnemy = new Entity();

                            if (divideEnemy.DivisionLevel == 1) newEnemy = GameObjectsManager.Enemy2AsteroidEntity;
                            else newEnemy = GameObjectsManager.Enemy3AsteroidEntity;

                            var enemy = EntityManager.Instantiate(newEnemy);
                            var enemyPos = divideEnemy.Position;
                            var enemyRotationSpeed = random.NextInt(1, 10);
                            var enemyDirection = new float3(random.NextInt(-1, 2), random.NextInt(-1, 2), 0);

                            var enemyComponent = EntityManager.GetComponentData<EnemyComponent>(enemy);
                            enemyComponent.Direction = enemyDirection;
                            enemyComponent.RotationSpeed = enemyRotationSpeed;
                            enemyComponent.Speed = random.NextInt(1, 6);

                            gameData.EnemiesAmmount++;

                            EntityManager.SetComponentData(enemy, enemyComponent);

                            EntityManager.SetComponentData(enemy, new Translation
                            {
                                Value = enemyPos
                            });
                        }
                    }
                }).WithStructuralChanges()
                .Run();

        SetSingleton(gameData);
    }
}
