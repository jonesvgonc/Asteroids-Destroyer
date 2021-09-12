using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class ShotSystem : SystemBase
{

    protected override void OnCreate()
    {
        base.OnCreate();
        
    }

    protected override void OnUpdate()
    {
        Entities
               .ForEach((Entity entity, ShotComponent attack) =>
               {
                   var attackEntity = EntityManager.Instantiate(GameObjectsManager.PlayerAttackEntity);

                   var playerEntity = GetSingletonEntity<PlayerComponent>();

                   var playerXYZ = EntityManager.GetComponentData<RotationEulerXYZ>(playerEntity);
                   var playerTranslation = EntityManager.GetComponentData<Translation>(playerEntity);
                   var playerLocal = EntityManager.GetComponentData<LocalToWorld>(playerEntity);

                   EntityManager.SetComponentData(attackEntity, playerLocal);
                   EntityManager.SetComponentData(attackEntity, playerXYZ);
                   EntityManager.SetComponentData(attackEntity, playerTranslation);

                   EntityManager.AddComponent<DestroyFlag>(entity);
               })
               .WithStructuralChanges()
               .Run();
    }
}
