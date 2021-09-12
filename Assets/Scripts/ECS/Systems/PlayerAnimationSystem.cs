using System.Linq;
using Unity.Entities;
using UnityEngine;

public class PlayerAnimationSystem : SystemBase
{
    private float timeToChangeSprite = 0.25f;
    private float timeCount = 0f;
    private int spriteIndex = 0;
    private int spriteMax = 0;

    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<PlayerComponent>();
    }

    protected override void OnUpdate()
    {
        if (spriteMax == 0) spriteMax = SpritesManager.Instance.PlayerSprites.Count();
        timeCount += Time.DeltaTime;

        if(timeCount > timeToChangeSprite)
        {
            timeCount = 0;

            var playerEntity = GetSingletonEntity<PlayerComponent>();
            var spriteRenderer = EntityManager.GetComponentObject<SpriteRenderer>(playerEntity);

            spriteRenderer.sprite = SpritesManager.Instance.PlayerSprites[spriteIndex];
            spriteIndex++;
            if (spriteIndex >= spriteMax) spriteIndex = 0;
        }
    }
}
