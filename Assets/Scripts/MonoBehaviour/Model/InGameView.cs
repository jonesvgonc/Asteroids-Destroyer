using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameView : MonoBehaviour
{
    //This is the InGame Model class, this class have all of the elements of the Ingame View, these elements can be used by the InGameViewModel.

    public Text ScoreText;
    public Text LivesText;
    public Text LevelText;

    public EventTrigger ShotTrigger;
    public EventTrigger AccelerateTrigger;
    public EventTrigger RotateClockwiseTrigger;
    public EventTrigger RotateCounterClockwiseTrigger;
}
