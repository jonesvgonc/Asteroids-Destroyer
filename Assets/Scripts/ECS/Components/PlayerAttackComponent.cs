using Unity.Entities;

public struct PlayerAttackComponent : IComponentData
{
    public float LifeTime;
    public float ProjectileMaxLifeTime;
    public bool Destroyed;
}
