using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController instance;
    [SerializeField] private TextMeshProUGUI _scoreUIText;
    [HideInInspector] public int fruitCollected;
    private Transform spawnPoint;
    
    void Start()
    {
        instance = this;
        fruitCollected = 0;
        DontDestroyOnLoad(this);
        spawnPoint = SceneController.instance._spawnPoint;
        spawnPoint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _scoreUIText.text = $"{fruitCollected:D2} / {SceneController.instance.TotalFruit:D2}";
    }

    public void Die()
    {
        Debug.Log("Player died");
        transform.position = spawnPoint.position;
    }

}
