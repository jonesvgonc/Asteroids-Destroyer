using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class MoveSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<PlayerComponent>();
    }

    protected override void OnUpdate()
    {
        var delta = Time.DeltaTime;


        var moveAmmount = 0f;

        var playerEntity = GetSingletonEntity<PlayerComponent>();

        var buffer = EntityManager.GetBuffer<AccelerateComponent>(playerEntity);

        for (int j = 0; j < buffer.Length; j++)
        {
            moveAmmount += 1;
        }
        if (moveAmmount != 0)
        {
            var LocalToWorldComponent = EntityManager.GetComponentData<LocalToWorld>(playerEntity);
            var moveComponent = EntityManager.GetComponentData<Translation>(playerEntity);

            moveComponent.Value += LocalToWorldComponent.Up * (moveAmmount * EntityManager.GetComponentData<PlayerComponent>(playerEntity).Speed * delta);

            EntityManager.SetComponentData(playerEntity, moveComponent);
        }
        buffer.Clear();
    }
}
