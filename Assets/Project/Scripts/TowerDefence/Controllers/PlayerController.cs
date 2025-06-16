using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
namespace TowerDefence
{
    
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;
        [SerializeField] private WeaponData _weaponsData;
        [SerializeField] private GameObject _heldWeapon;
        [SerializeField] private float _maxRayDistance = 20;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private string _weaponSlotTag;
        private bool _mouseWasPressedThisFrame = false;
        [SerializeField] private UnityEvent<WeaponData.Weapons> OnWeaponPurchased;
        private WeaponData.Weapons _weaponType;
        private void Start() {
            StartCoroutine(HeldWeaponRoutine());
        }
        public void CreateGun(WeaponData.Weapons weaponType)
        {
            WeaponData.WeaponInf weapon = _weaponsData.weaponDataList.FirstOrDefault(w => w.WeaponType == weaponType);
            _mouseWasPressedThisFrame = true;
            _heldWeapon = Instantiate(weapon.WeaponPrefab);
            _weaponType = weapon.WeaponType;
        }

        IEnumerator HeldWeaponRoutine()
        {
            while(_gameState.CurrentGameState == GameState.GameStateEnum.Playing)
            {
                if(_heldWeapon != null)
                {        
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if(Physics.Raycast(ray, out RaycastHit hit, _maxRayDistance, _layerMask))
                    {
                        _heldWeapon.transform.position = hit.point;
                    }
                    if(Input.GetMouseButtonUp(0) && !_mouseWasPressedThisFrame
                        && hit.collider.CompareTag(_weaponSlotTag)
                        && hit.collider.transform.childCount == 0
                    )
                    {
                        _heldWeapon.transform.position = hit.collider.transform.position;
                        _heldWeapon.transform.SetParent(hit.collider.transform);
                        _heldWeapon.GetComponent<WeaponAttack>().StartFireRoutine();
                        OnWeaponPurchased?.Invoke(_weaponType);
                        _heldWeapon = null;
                    }
                    if(Input.GetMouseButtonUp(1) && !_mouseWasPressedThisFrame)
                    {
                        Destroy(_heldWeapon);
                        //_heldWeapon = null;
                    }
                }
                if(_mouseWasPressedThisFrame)_mouseWasPressedThisFrame = false;
                yield return null;
            }
        }
    }
}
