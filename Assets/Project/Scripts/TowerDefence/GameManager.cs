using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour, IStart
{
    [Range(0,5)]
    [SerializeField] private float _gameSpeed = 1;
    [SerializeField] private GameState _gameState;
    [SerializeField] private static GameManager _instance;
    public static GameManager Instance { get => _instance; set => _instance = value; }
    public float GameSpeed { get => _gameSpeed;}
    [SerializeField] private UnityEvent OnGameOver;
    private void Awake() {
        if (_instance == null)
        {
            _instance = this;
        }else{
            Destroy(gameObject);
        }
        _gameState.OnGameOver.AddListener(EndGame);
    }
    public void UpdateGameSpeed(float value)
    {
        Time.timeScale = value;
        _gameSpeed = value;
    }
    private void Start() =>  StartCoroutine(StartGame());
    public IEnumerator StartGame()
    {
        yield return _gameState.StartGame();
        yield return null;
    }

    public void EndGame()
    {

    } 
}
