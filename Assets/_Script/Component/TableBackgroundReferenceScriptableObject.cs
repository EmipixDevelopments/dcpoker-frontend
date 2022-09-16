using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "table_customizations_backgrounds_reference", menuName = "ScriptableObjects/Table Customizations Background Reference")]
public class TableBackgroundReferenceScriptableObject : ScriptableObject
{
    public List<Color> Colors;
    public List<Sprite> BackgroundsImages;
}
