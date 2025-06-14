using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Objects/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Serializable]
    public enum Weapons
    {
        Gun,
        Canon,
        LaserTurret
    }
    [Serializable]
    public struct WeaponInf {
        public Weapons WeaponType;
        //public string WeaponName = 
        public int WeaponCost;
        public float WeaponRange;
        public int Damage;
        public float ShotCoolDown;
        public GameObject WeaponPrefab;

    }
    public List<WeaponInf> weaponDataList;
}
