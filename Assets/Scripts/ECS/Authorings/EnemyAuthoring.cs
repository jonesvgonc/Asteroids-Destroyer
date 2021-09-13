using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public bool CanShot;
    public bool CanDivide;
    public int DivisionLevel;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new EnemyComponent() { CanShot = CanShot, CanDivide = CanDivide, DivisionLevel = DivisionLevel });
        dstManager.AddComponent<RotationEulerXYZ>(entity);
    }
}
