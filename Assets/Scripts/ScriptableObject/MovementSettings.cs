using Character;
using UnityEngine;

//TODO временный костыль
[CreateAssetMenu(menuName = "Configs/Settings", fileName = "MovementSettings")]
public class MovementSettings : ScriptableObject
{
    public float MovementSpeedLerp;
    public float MovementBreakSpeed;
    public AnimationCurve MovementEase;

    private void OnValidate()
    {
        MovementSpeedSettings.MovementSpeedLerp = MovementSpeedLerp;
        MovementSpeedSettings.BrakingSpeed = MovementBreakSpeed;
        MovementSpeedSettings.MovementEase = MovementEase;
    }
}
