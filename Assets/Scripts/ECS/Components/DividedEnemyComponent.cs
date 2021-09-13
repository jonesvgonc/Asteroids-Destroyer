using Unity.Entities;
using Unity.Mathematics;

public struct DividedEnemyComponent : IComponentData
{
    public int DivisionLevel;
    public float3 Position;
    public bool Created;
}
