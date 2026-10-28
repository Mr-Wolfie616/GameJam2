using System;
using UnityEngine;

public class ToolDisplayHelper : MonoBehaviour
{
    [Serializable]
    public struct ToolSpriteMap
    {
        public ToolType toolType;
        public Sprite toolHeldSprite;
    }

    [SerializeField]
    private ToolSpriteMap[] toolSpriteMaps;
    public Sprite GetSpriteForTool(ToolType type)
    {
        foreach (var map in toolSpriteMaps)
        {
            if (map.toolType == type)
            {
                return map.toolHeldSprite;
            }
        }

        return null;
    }
}
public enum ToolType
{
    Scalpel,
    Bonesaw,
    None
}