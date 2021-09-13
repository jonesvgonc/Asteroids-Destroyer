using System.Linq;
using Unity.Entities;
using UnityEngine;

public class PlayerAttackAnimationSystem : SystemBase
{
    private EntityQuery attackQuery;

    private float timeToChangeSprite = 0.25f;
    private float timeCount = 0f;
    private int spriteIndex = 0;
    private int spriteMax = 0;
    protected override void OnCreate()
    {
        base.OnCreate();
        attackQuery = GetEntityQuery(typeof(PlayerAttackComponent));

        RequireForUpdate(attackQuery);
    }

    protected override void OnUpdate()
    {
        if (spriteMax == 0) spriteMax = SpritesManager.Instance.PlayerShotSprites.Count();

        timeCount += Time.DeltaTime;
        if (timeCount > timeToChangeSprite)
        {
            timeCount = 0;
            Entities
               .ForEach((Entity entity, PlayerAttackComponent attack) =>
               {
                   var spriteRenderer = EntityManager.GetComponentObject<SpriteRenderer>(entity);
                   spriteRenderer.sprite = SpritesManager.Instance.PlayerShotSprites[spriteIndex];
               })
               .WithStructuralChanges()
               .Run();

            spriteIndex++;
            if (spriteIndex >= spriteMax) spriteIndex = 0;
        }
    }
}
