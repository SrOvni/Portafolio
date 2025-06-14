using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Game State", menuName = "Scriptable Objects/Game State")]
public class GameState : ScriptableObject, IStart
{
    public enum GameStateEnum
    {
        Playing,
        GameOver
    }
    private bool _winner;
    public bool Winner{get{return _winner;}set{_winner = value;}}
    private int _enemyCount;
    public int EnemyCount{get{return _enemyCount;}set{_enemyCount = value;}}
    private bool _gameOver;
    public bool GameOver{get{return _gameOver;}set
    {
        _gameOver = value;
        _currentGameState = GameStateEnum.GameOver;
        _onGameOver?.Invoke();
    }}
    private UnityEvent _onGameOver;
    public UnityEvent OnGameOver { get => _onGameOver;}
    [SerializeField] private GameStateEnum _currentGameState;
    public GameStateEnum CurrentGameState { get { return _currentGameState;}}

    public IEnumerator StartGame()
    {
        _currentGameState = GameStateEnum.Playing;
        yield return null;
    }
}
