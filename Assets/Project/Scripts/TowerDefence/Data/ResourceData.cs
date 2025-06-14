using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource Data", menuName = "Scriptable Objects/Create Resource Data")]
public class ResourceData : ScriptableObject
{
    [Serializable]
    public struct WeaponData
    {
        public string WeaponName;
        public int WeaponCost;
    }
    public List<WeaponData> WeaponsData;
}
