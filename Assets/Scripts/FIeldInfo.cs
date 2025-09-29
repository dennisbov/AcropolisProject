using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Field")]
public class FIeldInfo : ScriptableObject
{
    public List<SphereTile> tiles = new List<SphereTile>();
}
