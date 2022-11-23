using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FlagsOfCountries", menuName = "ScriptableObjects/FlagsOfCountries", order = 1)]
public class FlagsOfCountries : ScriptableObject
{
    [SerializeField] FlagOfCountrieElement defaultFlag;

    [SerializeField] FlagOfCountrieElement[] flags = new FlagOfCountrieElement[0];

    private Dictionary<string, Sprite> _flags;

    public Sprite GetSpriteByName(string flag)
    {
        Sprite sprite;
        /*foreach (var flag in flags)
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
        }*/

        sprite = defaultFlag.Sprite;
//        if (!_flags.TryGetValue(flag, out sprite))
//        {
//            sprite = defaultFlag.Sprite;
//        }

        return sprite;
    }

    public void Init()
    {
        _flags = new Dictionary<string, Sprite>();

        foreach (var flag in flags)
        {
            _flags.Add(flag.Name, flag.Sprite);
        }
    }
}

[Serializable]
public class FlagOfCountrieElement
{
    public Sprite Sprite;
    public string Name;
}