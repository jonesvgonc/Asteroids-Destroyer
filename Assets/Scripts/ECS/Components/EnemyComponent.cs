using Unity.Entities;
using Unity.Mathematics;

public struct EnemyComponent : IComponentData
{
    public float3 Direction;
    public int RotationSpeed;
    public int Speed;
    public bool CanShot;
    public bool Destroyd;
    public bool CanDivide;
    public int DivisionLevel;
}
