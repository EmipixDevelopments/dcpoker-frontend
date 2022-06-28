using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FlagsOfCountries", menuName = "ScriptableObjects/FlagsOfCountries", order = 1)]
public class FlagsOfCountries : ScriptableObject
{
    [SerializeField] FlagOfCountrieElement defaultFlag;

    [SerializeField] FlagOfCountrieElement[] flags = new FlagOfCountrieElement[0];

    public Sprite GetSpriteByName(string spriteName) 
    {
        Sprite sprite = null;
        foreach (var flag in flags)
        {
            if (flag.Name == spriteName)
            {
                sprite = flag.Sprite;
                break;
            }
        }
        if (sprite == null)
        {
            sprite = defaultFlag.Sprite;
        }
        return sprite;

    }
}

[Serializable]
public class FlagOfCountrieElement
{
    public Sprite Sprite;
    public string Name;
}