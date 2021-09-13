using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class EnemyAnimationSystem : SystemBase
{
    private EntityQuery enemyQuery;

    protected override void OnCreate()
    {
        base.OnCreate();

        enemyQuery = GetEntityQuery(typeof(EnemyComponent));

        RequireForUpdate(enemyQuery);
    }

    protected override void OnUpdate()
    {
        var delta = Time.DeltaTime;
        

        Entities
               .ForEach((Entity entity, EnemyComponent enemy, ref RotationEulerXYZ rotation) =>
               {
                   rotation.Value.z += enemy.RotationSpeed * delta;
               })
               .WithStructuralChanges()
               .Run();
    }
}
