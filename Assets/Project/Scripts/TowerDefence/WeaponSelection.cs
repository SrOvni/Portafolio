using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private WeaponData.Weapons _weaponType; //Hola aquí también
    public WeaponData.Weapons WeaponType { get{return _weaponType;} set{_weaponType = value;} }
    [SerializeField] private UnityEvent<WeaponData.Weapons> OnWeaponSelection;

    public void SelectWeapon()
    {
        OnWeaponSelection.Invoke(_weaponType);
    }
}
