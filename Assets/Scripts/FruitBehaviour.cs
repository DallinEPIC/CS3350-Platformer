using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    [SerializeField] private int pointsWorth;
    public float delay; //seconds
    private Animator animator;
    private AudioSource audioSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Collect()
    {
        PlayerController.instance.score += pointsWorth;
        animator.SetTrigger("FruitCollect");
        audioSource.Play();
        float animTime = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animTime + delay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }
}