using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemToSpriteMap", menuName = "Item/Item to Sprite Map")]
public class ItemToSpriteMapping : ScriptableObject
{
    public const string kHandgunName = "Handgun";
    public const string kKnifeName = "Knife";
    public const string kHealthPotionName = "Health Potion";
    public const string kAmmoName = "Ammo";
    public const string kBoltcuttersName = "Boltcutters";
    public const string kSaphireName = "Saphire";
    public const string kEmeraeldName = "Emerald";
    public const string kCloverKeyName = "Clover Key";
    public const string kDiamondKeyName = "Diamond Key";
    public const string kOddShapedKeyName = "Odd Shaped Key";
    public const string kFrontDoorKeyName = "Front Door Key";

    [Serializable]
    public class ItemToSprite
    {
        public string ObjectKey;
        public Sprite Sprite;
    }

    public List<ItemToSprite> ItemToSpriteList = new List<ItemToSprite>();

    Dictionary<string, Sprite> m_Map = new Dictionary<string, Sprite>();
    public Sprite GetSprite(string key)
    {
        if (m_Map.Count == 0)
            SetupMap();

        if (m_Map.TryGetValue(key, out Sprite result))
            return result;
        return null;
    }

    void SetupMap()
    {
        foreach(ItemToSprite itos in ItemToSpriteList)
            m_Map.Add(itos.ObjectKey, itos.Sprite);
    }
}
