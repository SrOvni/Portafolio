using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] float _gameFPS = 120;
    [SerializeField] TMP_Text _frameRateText;
    private void Awake() {
        Application.targetFrameRate = (int)_gameFPS;
    }
    private void Update() {
        _frameRateText.text = Application.targetFrameRate.ToString();
    }
}
