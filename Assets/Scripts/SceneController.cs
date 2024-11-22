using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [HideInInspector] public int TotalFruit;
    public static SceneController instance;
    [SerializeField] public Transform spawnPoint;
    [HideInInspector] public int fruitCollected;
    [SerializeField] private TextMeshProUGUI _scoreUIText;
    void Start()
    {
        Transform player = PlayerController.instance.GetComponent<Transform>();
        player.position = spawnPoint.position;
        PlayerController.instance.gameObject.SetActive(true);
    }

    private void Awake()
    {
        instance = this;
        spawnPoint.gameObject.SetActive(false);
        TotalFruit = 0;
        fruitCollected = 0;
    }

    void Update()
    {
        _scoreUIText.text = $"{fruitCollected:D2} / {TotalFruit:D2}";
    }
}
