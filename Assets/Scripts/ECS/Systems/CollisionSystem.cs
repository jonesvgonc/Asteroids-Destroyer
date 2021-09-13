using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class CollisionSystem : SystemBase
{
    private EntityQuery enemyQuery;
    private EntityCommandBufferSystem commandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();

        enemyQuery = GetEntityQuery(typeof(EnemyComponent));
        commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

        RequireSingletonForUpdate<PlayerComponent>();
    }
    protected override void OnUpdate()
    {
        var enemiesEntities = enemyQuery.ToEntityArray(Allocator.TempJob);
        var commandBuffer = commandBufferSystem.CreateCommandBuffer().AsParallelWriter();

        if (enemiesEntities.Length > 0)
        {
            Entities
                .ForEach((Entity attackEntity,
                ref Translation translation,
                ref PlayerAttackComponent attackData) =>
                {
                    if (!attackData.Destroyed)
                    {
                        for (int i = 0; i < enemiesEntities.Length; i++)
                        {
                            var enemyComponent = EntityManager.GetComponentData<EnemyComponent>(enemiesEntities[i]);
                            if (!enemyComponent.Destroyd)
                            {
                                var enemyTranslation = EntityManager.GetComponentData<Translation>(enemiesEntities[i]);

                                var collide = math.distance(translation.Value, enemyTranslation.Value) <= .5f;

                                if (collide)
                                {
                                    EntityManager.AddComponentData(attackEntity, new DestroyFlag());
                                    EntityManager.AddComponentData(enemiesEntities[i], new DestroyFlag());
                                    attackData.Destroyed = true;
                                    enemyComponent.Destroyd = true;

                                    if (EntityManager.GetComponentData<EnemyComponent>(enemiesEntities[i]).CanDivide)
                                    {
                                        var newEnemy = EntityManager.CreateEntity();
                                        EntityManager.AddComponentData(newEnemy, new DividedEnemyComponent()
                                        {
                                            DivisionLevel = EntityManager.GetComponentData<EnemyComponent>(enemiesEntities[i]).DivisionLevel,
                                            Position = EntityManager.GetComponentData<Translation>(enemiesEntities[i]).Value
                                        });
                                    }
                                    var gameData = GetSingleton<GameDataComponent>();
                                    gameData.EnemiesAmmount--;
                                    if (gameData.EnemiesAmmount < 1)
                                    {
                                        var ent = EntityManager.CreateEntity();
                                        EntityManager.AddComponentData(ent, new EnemyWaveStartFlag() { });
                                        gameData.Level++;
                                        UIGameManager.Instance.SetLevel(gameData.Level);
                                    }
                                    gameData.Score += 10;
                                    UIGameManager.Instance.SetScore(gameData.Score);
                                    SetSingleton<GameDataComponent>(gameData);
                                }
                            }
                        }
                    }
                })
                .WithStructuralChanges()
                .WithoutBurst()
                .Run();
        }

        var playerEntity = GetSingletonEntity<PlayerComponent>();
        var playerTranslation = EntityManager.GetComponentData<Translation>(playerEntity);
        for (int i = 0; i < enemiesEntities.Length; i++)
        {
            var enemyComponent = EntityManager.GetComponentData<EnemyComponent>(enemiesEntities[i]);
            if (!enemyComponent.Destroyd)
            {
                var enemyTranslation = EntityManager.GetComponentData<Translation>(enemiesEntities[i]);

                var collide = math.distance(playerTranslation.Value, enemyTranslation.Value) <= .5f;

                if (collide)
                {
                    playerTranslation.Value = new float3(0, -3.5f, 0);
                    EntityManager.AddComponentData(enemiesEntities[i], new DestroyFlag());
                    enemyComponent.Destroyd = true;

                    EntityManager.SetComponentData(playerEntity, playerTranslation);

                    if (EntityManager.GetComponentData<EnemyComponent>(enemiesEntities[i]).CanDivide)
                    {
                        var newEnemy = EntityManager.CreateEntity();
                        EntityManager.AddComponentData(newEnemy, new DividedEnemyComponent()
                        {
                            DivisionLevel = EntityManager.GetComponentData<EnemyComponent>(enemiesEntities[i]).DivisionLevel,
                            Position = EntityManager.GetComponentData<Translation>(enemiesEntities[i]).Value
                        });
                    }
                    var gameData = GetSingleton<GameDataComponent>();

                    gameData.Lives--;
                    UIGameManager.Instance.SetLives(gameData.Lives);
                    if(gameData.Lives < 0)
                    {
                        //TODO: GAME OVER
                    }

                    gameData.EnemiesAmmount--;
                    if (gameData.EnemiesAmmount < 1)
                    {
                        var ent = EntityManager.CreateEntity();
                        EntityManager.AddComponentData(ent, new EnemyWaveStartFlag() { });
                        gameData.Level++;
                        UIGameManager.Instance.SetLevel(gameData.Level);
                    }

                    SetSingleton<GameDataComponent>(gameData);
                }
            }
        }


        enemiesEntities.Dispose();
    }
}
