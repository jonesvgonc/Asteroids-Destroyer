using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float RotationSpeed = 6f;
    public float Speed = 6f;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new PlayerComponent() { RotationSpeed = RotationSpeed, Speed = Speed });
        dstManager.AddComponent<RotationEulerXYZ>(entity);
        dstManager.AddBuffer<AccelerateComponent>(entity);
        dstManager.AddBuffer<RotateComponent>(entity);
    }
}
