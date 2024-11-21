using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    public float delay; //seconds
    private Animator animator;
    private AudioSource audioSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        SceneController.instance.TotalFruit += 1;
    }
    void Collect()
    {
        SceneController.instance.fruitCollected += 1;
        PlayerController.instance.totalFruitCollected += 1;
        GetComponent<CircleCollider2D>().enabled = false;
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