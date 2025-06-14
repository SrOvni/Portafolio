using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField]private int _gold = 100;
    public int Gold
    {
        get { return _gold; }
        set { _gold = value; }
    }
    [SerializeField] private int _startGoldAmount = 100;
    [SerializeField] private WeaponData _weaponsData;
    [SerializeField] private Logger _logger;
    [SerializeField] private string _loggerGameObjectName = "Weapons";
    [SerializeField] private int _increaseGoldAmount = 10;
    [SerializeField] private int _increaseGoldDelay = 1;
    [SerializeField] private UnityEvent<int> OnGoldAmountChanged;

    private void Start() {
        _gold = _startGoldAmount;
        OnGoldAmountChanged?.Invoke(_gold);
        StartCoroutine(IncreaseGoldRoutine());
        if(_logger == null)
        {
            _logger = GameObject.Find(_loggerGameObjectName).GetComponent<Logger>();
        }
    }
    IEnumerator IncreaseGoldRoutine()
    {
        while(_gameState.CurrentGameState == GameState.GameStateEnum.Playing)
        {
            yield return new WaitForSeconds(_increaseGoldDelay);
            Gold += _increaseGoldAmount;
            OnGoldAmountChanged?.Invoke(Gold);
            //
        }
    }

    public void SubtractGold(WeaponData.Weapons weaponType)
    {
        WeaponData.WeaponInf weapon = _weaponsData.weaponDataList.FirstOrDefault( w => w.WeaponType == weaponType);
        if(EqualityComparer<WeaponData.WeaponInf>.Default.Equals(weapon, default))
        {
            _logger.Log($"No weapon of type {weaponType} found on {typeof(WeaponData.WeaponInf)}");
            return;
        }else{
            _gold -= weapon.WeaponCost;
            OnGoldAmountChanged?.Invoke(Gold);
        }
    }

}
