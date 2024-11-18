using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SpikeHeadBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private float _timeToReachTarget;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _timeElapsed;
    private bool _sawPlayer;
    private bool _goingUp;


    void Start()
    {
        _targetObject.SetActive(false);
        _startPosition = transform.position;
        _targetPosition = _targetObject.transform.position;
    }

    void Update()
    {
        if (_sawPlayer)
        {
            _timeElapsed += Time.deltaTime;
            Move();
        }
    }
    //player entering trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _sawPlayer = true;
        }
    }
    private void Move()
    {
        float interpolation = _timeElapsed / _timeToReachTarget;
        if (!_goingUp) //Going down
        {
            transform.position = Vector2.Lerp(_startPosition, _targetPosition, interpolation);
        }
        else //going up
        {
            transform.position = Vector2.Lerp(_targetPosition, _startPosition, interpolation);
        }
        
        if (interpolation >= 1) //If reached bottom or top
        {
            if (_goingUp) _sawPlayer = false; //reached top, reset _sawPlayer
            _goingUp = !_goingUp; //Start going up if hit bottom, or reset to false if reached top
            _timeElapsed = 0;
        }
    }
    //hitting the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Spike head hit player");
        PlayerController.instance.Die();
    }
}
