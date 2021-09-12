using System.Collections.Generic;
using UnityEngine;

public class SpritesManager : MonoBehaviour
{
    public static SpritesManager Instance;

    public List<Sprite> PlayerSprites;
    public List<Sprite> PlayerShotSprites;
    public List<Sprite> EnemyShotSprites;

    public void Awake()
    {
        Instance = this;
    }
}
