using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class HitDetectorTrait : MonoBehaviour, IPoolableObject
{
    [SerializeField] private int minDieDelay = 2;
    [SerializeField] private int maxDieDelay = 5;
    private bool _wasCollisionHit = false;
    private ColorChanger _randomColorChanger;

    public event Action<IPoolableObject> OnReturnToPoolRequested;

    private void Start()
    {
        _randomColorChanger = new ColorChanger(GetComponent<Renderer>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_wasCollisionHit == false)
        {
            _wasCollisionHit = true;
            _randomColorChanger.ChangeColor(gameObject, Color.blue);
            StartCoroutine(ReturnToPoolAfterDelay());
        }
    }

    private IEnumerator ReturnToPoolAfterDelay()
    {
        float delay = RandomHelper.GetRandomNumber(minDieDelay, maxDieDelay + 1);
        
        yield return new WaitForSeconds(delay);
        
        OnReturnToPoolRequested?.Invoke(this);
    }

    public void ResetState()
    {
        _wasCollisionHit = false;
        _randomColorChanger.ChangeColor(gameObject, Color.white);
    }
}
