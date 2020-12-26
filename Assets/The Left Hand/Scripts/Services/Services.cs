using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Services : MonoBehaviour
{
    public static SpriteService Sprite;

    private void Awake()
    {
        Sprite = new SpriteService();
    }
}
