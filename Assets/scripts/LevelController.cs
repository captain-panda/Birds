using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Monster[] _monsters;
    [SerializeField] string _nextLevelName;
    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();

    }
    // Update is called once per frame
    void Update()
    {
        if (monstersAreAllDead())
            goToNextLevel();
    }

    private bool monstersAreAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }
        return true;
    }

    private void goToNextLevel()
    {
        SceneManager.LoadScene(_nextLevelName);
    }
}
