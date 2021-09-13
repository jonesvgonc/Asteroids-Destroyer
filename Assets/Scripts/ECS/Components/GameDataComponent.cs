using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct GameDataComponent : IComponentData
{
    public float2 Bounds;
    public int Level;
    public int Score;
    public int Lives;
    public int EnemiesAmmount;
}
