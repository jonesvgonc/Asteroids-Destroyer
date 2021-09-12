using Unity.Entities;
using Unity.Transforms;

public class RotateSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<PlayerComponent>();
    }

    protected override void OnUpdate()
    {
        var delta = Time.DeltaTime;

        var rotatetAmmount = 0f;

        var playerEntity = GetSingletonEntity<PlayerComponent>();

        var buffer = EntityManager.GetBuffer<RotateComponent>(playerEntity);

        for (int j = 0; j < buffer.Length; j++)
        {
            rotatetAmmount += buffer[j].Direction; 
        }

        var rotateComponent = EntityManager.GetComponentData<RotationEulerXYZ>(playerEntity);

        rotateComponent.Value.z += rotatetAmmount * EntityManager.GetComponentData<PlayerComponent>(playerEntity).RotationSpeed * delta;

        EntityManager.SetComponentData(playerEntity, rotateComponent);

        buffer.Clear();
    }
}
