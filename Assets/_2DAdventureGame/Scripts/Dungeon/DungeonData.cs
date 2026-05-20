using UnityEngine;

[CreateAssetMenu(fileName = "DungeonData", menuName = "Scriptable Objects/DungeonData")]
public class DungeonData : ScriptableObject
{
    [Header("Info")]
    public string dungeonName;

    [TextArea]
    public string description;

    [TextArea]
    public string warning;

    [Header("Difficulty")]
    public string difficulty;

    [Header("Visual")]
    public Sprite thumbnail;

    [Header("Scene")]
    public string sceneName;
}

