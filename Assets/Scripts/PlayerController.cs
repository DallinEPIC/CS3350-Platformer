using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController instance;
    [HideInInspector] public int totalFruitCollected;
    
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        Debug.Log("Player died");
        transform.position = SceneController.instance.spawnPoint.position;
    }

}
