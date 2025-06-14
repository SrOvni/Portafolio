using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class UIController : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private TMP_Text _gameSpeedValue;
    [SerializeField] private Slider _gameSpeedSlider;
    [SerializeField] private Button[] _weaponButtons;
    [SerializeField] private WeaponData _weaponsData;
    [SerializeField] private TMP_Text _goldAmountText;

    public float GameSpeedSlider { get => _gameSpeedSlider.value;}
    private void Awake() {
        _gameSpeedSlider.minValue = 0.5f;
        _gameSpeedSlider.maxValue = 3;
        _gameSpeedSlider.value = 1;
        _gameSpeedValue.text = _gameSpeedSlider.value.ToString();

        //Listen for when game ends
        _gameState.OnGameOver.AddListener(ShowGameOverScreen);

    }
    public void ShowGameOverScreen()
    {
        if (_gameState.Winner)
        {
            _winScreen.SetActive(true);
        }else{
            _loseScreen.SetActive(true);
        }
    }

    public void UpdateGameSpeedText(float value)
    {
        _gameSpeedValue.text = value.ToString("0.00X");
    }
    public void RetryGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CheckIfEnoughGoldForWeapon(int gold)
    {
        for (int i = 0; i < _weaponButtons.Length; i++)
        {
            if(gold > _weaponsData.weaponDataList[i].WeaponCost)
            {
                _weaponButtons[i].interactable = true;
            }
            else{
                _weaponButtons[i].interactable = false;
            }
        }
    }
    public void UpdateGoldAmount(int gold)
    {
        _goldAmountText.text = $"Gold: {gold}";
    }
}
