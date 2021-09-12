using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class InputSystem : SystemBase
{

    private float timeToShot = 1f;
    private float timeShotConter = 0f;

    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<PlayerComponent>();
    }

    protected override void OnUpdate()
    {
        var playerEntity = GetSingletonEntity<PlayerComponent>();

        Entities
            .ForEach((Entity entity, AccelerateFlag component) =>
            {
                EntityManager.AddComponent<DestroyFlag>(entity);
                var playerBuffer = EntityManager.GetBuffer<AccelerateComponent>(playerEntity);

                playerBuffer.Add(new AccelerateComponent());

            })
            .WithStructuralChanges()
            .Run();

        Entities
            .ForEach((Entity entity, RotateFlag component) =>
            {
                EntityManager.AddComponent<DestroyFlag>(entity);
                var playerBuffer = EntityManager.GetBuffer<RotateComponent>(playerEntity);

                playerBuffer.Add(new RotateComponent() { Direction = component.Direction });

            })
            .WithStructuralChanges()
            .Run();

        timeShotConter += Time.DeltaTime;
        if (timeToShot < timeShotConter)
        {
            timeShotConter = 0f;

            Entities
                .ForEach((Entity entity, ShotFlag component) =>
                {
                    EntityManager.AddComponent<DestroyFlag>(entity);
                    var attackEntity = EntityManager.CreateEntity();
                    EntityManager.AddComponent<ShotComponent>(attackEntity);
                })
                .WithStructuralChanges()
                .Run();
        }
    }
}
