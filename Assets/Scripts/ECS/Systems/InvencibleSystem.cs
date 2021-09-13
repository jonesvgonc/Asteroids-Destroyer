using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class InvencibleSystem : SystemBase
{
    private float timeCount = 0f;

    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<InvencibleFlag>();
    }

    protected override void OnUpdate()
    {
        timeCount += Time.DeltaTime;
        if(timeCount > 1f)
        {
            EntityManager.DestroyEntity(GetSingletonEntity<InvencibleFlag>());
            var playerComp = GetSingleton<PlayerComponent>();
            playerComp.GetHit = false;
            SetSingleton(playerComp);
            timeCount = 0;
        }
    }
}
