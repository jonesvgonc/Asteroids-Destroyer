using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct PlayerAttackComponent : IComponentData
{
    public float LifeTime;
    public float ProjectileMaxLifeTime;
}
