using Unity.Entities;

public struct PlayerComponent : IComponentData
{
    public float RotationSpeed;
    public float Speed;
    public bool GetHit;
}
