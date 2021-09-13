using Unity.Entities;
using Unity.Transforms;

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
            var bounds = GetSingleton<GameDataComponent>().Bounds;

            moveComponent.Value += LocalToWorldComponent.Up * (moveAmmount * EntityManager.GetComponentData<PlayerComponent>(playerEntity).Speed * delta);

            if (moveComponent.Value.y > bounds.y)
                moveComponent.Value.y = -bounds.y;
            else if (moveComponent.Value.y < -bounds.y)
                moveComponent.Value.y = bounds.y;

            if (moveComponent.Value.x > bounds.x)
                moveComponent.Value.x = -bounds.x;
            else if (moveComponent.Value.x < -bounds.x)
                moveComponent.Value.x = bounds.x;

            EntityManager.SetComponentData(playerEntity, moveComponent);
        }
        buffer.Clear();
    }
}
