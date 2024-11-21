using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCupBehaviour : MonoBehaviour
{
    private Animator _animator;
    public GameObject victoryPanel;
    [SerializeField] private string _nextLevelName;
    void Start()
    {
        _animator = GetComponent<Animator>();
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
            _animator.SetTrigger("Victory");
            StartCoroutine(WaitAndLoadNextLevel());
        }
    }

    private IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(2);
        PlayerController.instance.gameObject.SetActive(false);
        if (_nextLevelName != "")
        {
            SceneManager.LoadScene(_nextLevelName);
        }
    }
}
