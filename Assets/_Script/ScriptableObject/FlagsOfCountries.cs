using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FlagsOfCountries", menuName = "ScriptableObjects/FlagsOfCountries", order = 1)]
public class FlagsOfCountries : ScriptableObject
{
    [SerializeField] FlagOfCountrieElement defaultFlag;

    [SerializeField] FlagOfCountrieElement[] flags = new FlagOfCountrieElement[0];

<<<<<<< Updated upstream:Assets/_Script/ScriptableObject/FlagsOfCountries.cs
    public Sprite GetSpriteByName(string spriteName) 
=======
    private Dictionary<string, Sprite> _flags;

    public Sprite GetSpriteByName(string flag)
>>>>>>> Stashed changes:Assets/_Script/ScriptableObject/Common/FlagsOfCountries.cs
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
<<<<<<< Updated upstream:Assets/_Script/ScriptableObject/FlagsOfCountries.cs
        }
        return sprite;
=======
        }*/
>>>>>>> Stashed changes:Assets/_Script/ScriptableObject/Common/FlagsOfCountries.cs

        sprite = defaultFlag.Sprite;
//        if (!_flags.TryGetValue(flag, out sprite))
//        {
//            sprite = defaultFlag.Sprite;
//        }

        return sprite;
    }
<<<<<<< Updated upstream:Assets/_Script/ScriptableObject/FlagsOfCountries.cs
=======

    public void Init()
    {
        _flags = new Dictionary<string, Sprite>();

        foreach (var flag in flags)
        {
            _flags.Add(flag.Name, flag.Sprite);
        }
    }
>>>>>>> Stashed changes:Assets/_Script/ScriptableObject/Common/FlagsOfCountries.cs
}

[Serializable]
public class FlagOfCountrieElement
{
    public Sprite Sprite;
    public string Name;
}