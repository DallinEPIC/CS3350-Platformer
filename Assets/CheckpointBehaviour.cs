using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{
    public Transform spawnPoint;
    bool isfound;
    void Start()
    {
        isfound = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && isfound == false)
        {
            isfound = true;
            GetComponent<Animator>().SetTrigger("Checkpoint");
            GetComponent<AudioSource>().Play();
            spawnPoint.position = transform.position;
        }
    }
}
