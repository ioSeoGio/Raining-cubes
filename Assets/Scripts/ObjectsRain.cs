using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsRain : MonoBehaviour
{
    [SerializeField] private Transform _objectParent;
    [SerializeField] private float _yIntend = 10;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private HitDetectorTrait _prefab;
    [SerializeField] private float _cooldownInSeconds = 0.5f;
    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectSpawner _objectSpawner = new();
    private ObjectPool<IPoolableObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<IPoolableObject>(
            createFunc: () => CreateObject(),
            actionOnGet: (poolableObject) => OnGet(poolableObject),
            actionOnRelease: (poolableObject) =>
            {
                poolableObject.gameObject.SetActive(false);
            },
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnObjectCoroutine());    
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnObjectCoroutine());
    }

    private IEnumerator SpawnObjectCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_cooldownInSeconds);

        while (true)
        {
            _pool.Get();
            yield return wait;
        }
    }

    private void OnGet(IPoolableObject poolableObject)
    {
        poolableObject.gameObject.transform.position = RandomHelper.GetRandomPointOnTerrain(_terrain, _yIntend);
        poolableObject.gameObject.SetActive(true);
    }

    private IPoolableObject CreateObject()
    {
        IPoolableObject poolableObject = _objectSpawner.Spawn(_prefab, RandomHelper.GetRandomPointOnTerrain(_terrain, _yIntend), _objectParent);
        poolableObject.OnReturnToPoolRequested += ReturnObjectToPool;

        return poolableObject;
    }

    private void ReturnObjectToPool(IPoolableObject poolableObject)
    {
        poolableObject.ResetState();
        _pool.Release(poolableObject);
    }
}
