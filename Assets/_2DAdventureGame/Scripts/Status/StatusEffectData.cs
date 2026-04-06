using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectData", menuName = "Scriptable Objects/StatusEffectData")]
public class StatusEffectData : ScriptableObject
{
    public string effectID;
    public Sprite icon;
    public string displayName;
    public int maxStack;
    public float duration;
    public bool isDebuff;
    public Color color;
}
