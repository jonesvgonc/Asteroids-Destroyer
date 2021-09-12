using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ShotSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<PlayerComponent>();
    }

    protected override void OnUpdate()
    {

    }
}
