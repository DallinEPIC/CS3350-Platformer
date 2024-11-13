using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private float _timeToReachTarget;
    [SerializeField, Range(-10,9)] private float _elasticity;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _timeElapsed;
    void Start()
    {
        _targetObject.SetActive(false);
        _startPosition = transform.position;
        _targetPosition = _targetObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _timeElapsed += Time.deltaTime;

        float _burst;
        if (_elasticity < 0) {_burst = 1 / -_elasticity; }
        else if (_elasticity == 0) { _burst = 1; }
        else { _burst = _elasticity + 1; }

        float interpolation = (float) System.Math.Pow(_timeElapsed / _timeToReachTarget, _burst);

        transform.position = Vector3.Lerp(_startPosition, _targetPosition, interpolation);

        if (interpolation >= 1)
        {
            Vector3 temp = _startPosition;
            _startPosition = _targetPosition;
            _targetPosition = temp;
            _timeElapsed = 0;
        }

    }
}
