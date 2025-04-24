using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class HitDetectorTrait : MonoBehaviour, IPoolableObject
{
    [SerializeField] private int minDieDelay = 2;
    [SerializeField] private int maxDieDelay = 5;
    [SerializeField] private Color _hitColor = Color.blue;
    [SerializeField] private Color _defaultColor = Color.white;
    private bool _wasCollisionHit = false;
    private ColorChanger _randomColorChanger;

    public event Action<IPoolableObject> ReturnToPoolRequested;

    public void ResetState()
    {
        _wasCollisionHit = false;
        _randomColorChanger.ChangeColor(_defaultColor);
    }

    private void Start()
    {
        _randomColorChanger = new ColorChanger(GetComponent<Renderer>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_wasCollisionHit == false && collision.gameObject.GetComponent<Platform>() != null)
        {
            _wasCollisionHit = true;
            _randomColorChanger.ChangeColor(_hitColor);
            StartCoroutine(ReturnToPoolAfterDelay());
        }
    }

    private IEnumerator ReturnToPoolAfterDelay()
    {
        float delay = RandomHelper.GetRandomNumber(minDieDelay, maxDieDelay + 1);
        
        yield return new WaitForSeconds(delay);
        
        ReturnToPoolRequested?.Invoke(this);
    }
}
