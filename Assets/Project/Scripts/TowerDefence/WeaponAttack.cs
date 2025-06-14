using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] Logger _logger;
    [SerializeField] private  WeaponData _weaponsData;
    private WeaponData.WeaponInf weapon;
    [SerializeField] string _loggerName;
    [SerializeField] Transform _weaponBarrel;
    [SerializeField] GameObject _canonBallPrefab;
    [SerializeField] Transform _canonBallSpawnPoint;
    [SerializeField] private UnityEvent OnWeaponShoot;
    [SerializeField] private WeaponData.Weapons _weaponType; 
    [SerializeField] private LayerMask _enemyLayer;
     private void Start() {
        // if(_logger == null)
        // {
        //     _logger = GameObject.Find(_loggerName).GetComponent<Logger>();
        //     if(_logger == null)
        //     {
        //         Debug.LogError("No se tiene referencia al Logger en el objecto: " + gameObject.name);
        //     }
        //     weapon = _weaponsData.weaponDataList.FirstOrDefault(w => w.WeaponType == _weaponType);
        // }
    }
    public void StartFireRoutine() => StartCoroutine(FireRoutine());
    IEnumerator FireRoutine()
    {
        while (_gameState.CurrentGameState == GameState.GameStateEnum.Playing)
        {
            Ray ray = new Ray(_weaponBarrel.position, _weaponBarrel.forward);
            if(Physics.Raycast(ray, out RaycastHit rayHit, weapon.WeaponRange, _enemyLayer))
            {
                _logger.Log(rayHit.collider.name);
                if(rayHit.collider.CompareTag("Enemy"))
                {
                    if(_weaponType == WeaponData.Weapons.Canon)
                    {
                        Instantiate(_canonBallPrefab, _canonBallSpawnPoint.position, _canonBallSpawnPoint.rotation);

                    }else{
                        Health enemyHealth = rayHit.collider.GetComponent<Health>();
                        if(enemyHealth != null)
                        {
                            enemyHealth.ReceiveDamage(weapon.Damage);
                        }
                    }
                    OnWeaponShoot?.Invoke();
                }
                Debug.DrawRay(ray.origin, ray.direction * weapon.WeaponRange, Color.red);
                yield return new WaitForSeconds(weapon.ShotCoolDown);
            }else
            {
                Debug.DrawRay(ray.origin, ray.direction * weapon.WeaponRange, Color.yellow);
                yield return null;
            }

        }
    }
}
