using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteService
{
    ItemToSpriteMapping m_Mapping;

    public SpriteService()
    {
        m_Mapping = Resources.Load<ItemToSpriteMapping>("SObjects/ItemToSpriteMap");
    }

    public Sprite GetSpriteForKey(string key)
    {
        return m_Mapping.GetSprite(key);
    }
}
