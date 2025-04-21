using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ObjectSpawnCoroutine : MonoBehaviour
{
    [SerializeField] private Transform _objectParent;
    [SerializeField] private float _yIntend = 10;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private HitDetectorTrait _prefab;
    [SerializeField] private float _cooldownInSeconds = 0.5f;
    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectSpawner _objectSpawner = new();
    private ObjectPool<HitDetectorTrait> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<HitDetectorTrait>(
            createFunc: () => CreateObject(),
            actionOnGet: (hitDetectorTrait) => ActionOnGet(hitDetectorTrait),
            actionOnRelease: (hitDetectorTrait) => hitDetectorTrait.gameObject.SetActive(false),
            actionOnDestroy: (hitDetectorTrait) => Destroy(hitDetectorTrait.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void OnEnable()
    {
        StartCoroutine(Coroutine());    
    }

    private void OnDisable()
    {
        StopCoroutine(Coroutine());
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), 0f, _cooldownInSeconds);
    }

    private void GetObject()
    {
        _pool.Get();
    }

    private IEnumerator Coroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_cooldownInSeconds);

        while (true)
        {
            CreateObject();
            yield return wait;
        }
    }

    private void ActionOnGet(HitDetectorTrait hitDetectorTrait)
    {
        hitDetectorTrait.gameObject.transform.position = RandomHelper.GetRandomPointOnTerrain(_terrain, _yIntend);
        hitDetectorTrait.gameObject.SetActive(true);
    }

    private HitDetectorTrait CreateObject()
    {
        return _objectSpawner.Spawn(_prefab, RandomHelper.GetRandomPointOnTerrain(_terrain, _yIntend), _objectParent);
    }
}
