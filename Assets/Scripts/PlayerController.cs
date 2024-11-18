using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController instance;
    [SerializeField] private TextMeshProUGUI _scoreUIText;
    [HideInInspector] public int fruitCollected;
    void Start()
    {
        instance = this;
        fruitCollected = 0;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        _scoreUIText.text = $"{fruitCollected:D3}";
    }

    public void Die()
    {
        Debug.Log("Player died");
    }
}
