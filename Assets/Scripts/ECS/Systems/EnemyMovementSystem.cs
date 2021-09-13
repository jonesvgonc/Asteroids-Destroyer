using Unity.Entities;
using Unity.Transforms;

public class EnemyMovementSystem : SystemBase
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
        var bounds = GetSingleton<GameDataComponent>().Bounds;

        Entities
               .ForEach((Entity entity, EnemyComponent enemy, ref Translation translate) =>
               {
                   translate.Value += enemy.Direction * delta * enemy.Speed;

                   if (translate.Value.y > bounds.y)
                       translate.Value.y = -bounds.y;
                   else if (translate.Value.y < -bounds.y)
                       translate.Value.y = bounds.y;

                   if (translate.Value.x > bounds.x)
                       translate.Value.x = -bounds.x;
                   else if (translate.Value.x < -bounds.x)
                       translate.Value.x = bounds.x;

               })
               .WithStructuralChanges()
               .Run();
    }
}
