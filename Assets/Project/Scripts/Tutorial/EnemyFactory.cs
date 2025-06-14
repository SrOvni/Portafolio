using System.Runtime.InteropServices.WindowsRuntime;
using EnemyBaseClass;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    public GameObject Prefab { get { return _enemyPrefab;} set { _enemyPrefab = value;}}
    public ObjectPool<EnemyFactory> objectPool;
    // void Awake()
    // {
    //     objectPool = new ObjectPool<Enemy>(
    //     () => Instantiate(Prefab).GetComponent<Enemy>(),
    //     enemy => enemy.SetActive(),
    //     enemy => enemy.SetInactive(),
    //     enemy => Destroy(enemy)
    //     );
    // }
    public Enemy Create(Vector3 position)
    {
        GameObject go = Instantiate(_enemyPrefab, position, Quaternion.identity);
        Enemy enemy = go.GetComponent<Enemy>();
        enemy.Initialize(position);
        return enemy;
    }
}