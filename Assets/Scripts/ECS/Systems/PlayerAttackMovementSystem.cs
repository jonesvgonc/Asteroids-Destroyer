using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PlayerAttackMovementSystem : SystemBase
{
    private EntityCommandBufferSystem _commandBufferSystem;

    protected override void OnCreate()
    {
        _commandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();

        RequireSingletonForUpdate<PlayerComponent>();
    }
    protected override void OnUpdate()
    {
        var delta = Time.DeltaTime;
        var commandBuffer = _commandBufferSystem.CreateCommandBuffer().AsParallelWriter();

        var bounds = UIGameManager.Instance.ScreenBounds;

        Dependency = Entities
            .ForEach((int entityInQueryIndex, Entity entity, LocalToWorld localWorld, ref PlayerAttackComponent projectile, ref Translation translation) =>
            {
                var newTranslation = localWorld.Up * 100 * delta;
                translation.Value += newTranslation;

                if (translation.Value.y > bounds.y)
                    translation.Value.y = -bounds.y;
                else if (translation.Value.y < -bounds.y)
                    translation.Value.y = bounds.y;

                if (translation.Value.x > bounds.x)
                    translation.Value.x = -bounds.x;
                else if (translation.Value.x< -bounds.x)
                    translation.Value.x = bounds.x;

                projectile.LifeTime += delta;
                if (projectile.LifeTime > projectile.ProjectileMaxLifeTime)
                    commandBuffer.AddComponent<DestroyFlag>(entityInQueryIndex, entity);
            })
            .ScheduleParallel(Dependency);
    }
}
