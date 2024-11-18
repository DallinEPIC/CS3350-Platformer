using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [HideInInspector] public int TotalFruit;
    public static SceneController instance;
    [SerializeField] public Transform _spawnPoint;
    void Start()
    {
        instance = this;
    }

    private void Awake()
    {
        TotalFruit = 0;
    }

    void Update()
    {
        
    }
}
