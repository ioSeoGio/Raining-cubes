using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitDetectorTrait : MonoBehaviour
{
    [SerializeField] private int minDieDelay = 2;
    [SerializeField] private int maxDieDelay = 5;
    private bool _wasCollisionHit = false;
    private RandomColorChanger _randomColorChanger = new();
 
    private void OnCollisionEnter(Collision collision)
    {
        if (_wasCollisionHit == false)
        {
            _wasCollisionHit = true;
            _randomColorChanger.ChangeColor(gameObject);
            Destroy(gameObject, RandomHelper.GetRandomNumber(minDieDelay, maxDieDelay + 1));
        }
    }
}
