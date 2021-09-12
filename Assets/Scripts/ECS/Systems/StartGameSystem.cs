using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class StartGameSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<StartGameFlag>();
    }
    protected override void OnUpdate()
    {
        var inGameEntity = EntityManager.CreateEntity();
        EntityManager.AddComponentData(inGameEntity, new InGameFlag());

        var player = EntityManager.Instantiate(GameObjectsManager.PlayerEntity);

        EntityManager.SetComponentData(player, new Translation
        {
            Value = new float3(0, -3.5f, 0)
        });

        EntityManager.DestroyEntity(GetSingletonEntity<StartGameFlag>());
    }
}
