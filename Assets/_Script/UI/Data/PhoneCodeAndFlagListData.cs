using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PhoneCodeAndFlagListData
{
    public List<CodeAndFlag> List = new List<CodeAndFlag>();
}

[Serializable]
public class CodeAndFlag
{
    public string PhoneCode;
    public string FlagName;
}
