using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScoreBehaviour : MonoBehaviour
{
    private TextMeshProUGUI _victoryText;
    void Start()
    {
        _victoryText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _victoryText.text = PlayerController.instance.totalFruitCollected + " fruit collected!";
    }
}
