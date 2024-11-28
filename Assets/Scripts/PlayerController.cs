using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController instance;
    [HideInInspector] public int totalFruitCollected;
    private Animator animator;
    
    void Start()
    {
        animator = playerMovementBehaviour.instance.animator;
        instance = this;
        DontDestroyOnLoad(this);
    }
    public void Die()
    {
        Debug.Log("Player died");
        StartCoroutine(WaitForAnim());
        
    }
    private IEnumerator WaitForAnim()
    {
        animator.SetTrigger("Die");
        playerMovementBehaviour.instance.rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.1f);
        transform.position = SceneController.instance.spawnPoint.position;

    }

}
