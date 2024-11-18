using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController instance;
    [SerializeField] private TextMeshProUGUI _scoreUIText;
    [HideInInspector] public int score;
    void Start()
    {
        instance = this;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _scoreUIText.text = $"{score:D3}";
    }

    public void Die()
    {
        Debug.Log("Player died");
    }
}
