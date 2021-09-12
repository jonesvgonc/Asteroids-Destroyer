using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct RotateFlag : IComponentData
{
    //Ask to inputsystem to add an rotate in the buffer.
    public float Direction;
}
