using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct DividedEnemyComponent : IComponentData
{
    public int DivisionLevel;
    public float3 Position;
    public bool Created;
}
