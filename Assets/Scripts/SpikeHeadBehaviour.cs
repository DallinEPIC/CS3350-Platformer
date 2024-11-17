using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SpikeHeadBehaviour : MonoBehaviour
{
    public Transform target;
    float time;
    [SerializeField] private float _timeToReachTarget;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _timeElapsed;
    private bool sawPlayer;


    void Start()
    {
        time = 1;
        _startPosition = transform.position;
        _targetPosition = target.position;
    }

    void Update()
    {
        if (sawPlayer)
        {
            Move();
        }
    }
    //player entering trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sawPlayer = true;
        }
    }
    private void Move()
    {
        transform.position = Vector2.Lerp(transform.position, target.transform.position, time);
        if (transform.position.y <= target.position.y)
        {

        }
    }
    //hitting the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit player");
    }
}
