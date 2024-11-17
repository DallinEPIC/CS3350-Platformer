using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryCupBehaviour : MonoBehaviour
{
    public GameObject victoryPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("YOU WIN");
            victoryPanel.gameObject.SetActive(true);
        }
    }
}
