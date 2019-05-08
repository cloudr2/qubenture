using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private Enemy[] enemies = null;
    public Player player = null;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = FindObjectOfType<Player>();
        CheckEnemiesAlive();
    }

    public void EndGame(string state)
    {
        if (state == "Win")
        {
            Win();
        } else {
            Lose();
        }
    }

    private void Lose()
    {
        print("You Lose");
    }

    private void Win()
    {
        print("You Win");
    }

    public void LoadLevel(int sceneIndex, LoadSceneMode sceneMode)
    {
        SceneManager.LoadScene(sceneIndex, sceneMode);
    }

    public void LoadLevel(string sceneName, LoadSceneMode sceneMode)
    {
        SceneManager.LoadScene(sceneName, sceneMode);
    }

    public void CheckEnemiesAlive()
    {
        enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length <= 0)
        {
            EndGame("Win");
        }
        print("Current amount of enemies: " + enemies.Length);
    }

    public void DestroyEnemies()
    {
        CheckEnemiesAlive();
        if (enemies.Length > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.GetComponent<HealthComponent>().TakeDamage(1000000);
            }
        }
        else
            print("No enemies left");
    }

    public void Aggro()
    {
        CheckEnemiesAlive();
        if (enemies.Length > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.GetComponent<AIComponent>().ForceTargetPlayer();
            }
        }
        else
            print("No enemies left");
    }

    public void Start()
    {
        LoadLevel("Stage01", LoadSceneMode.Single);
    }

    public void GodMode(string arg)
    {
        if (player)
        {
            if (arg == "" || arg == null)
                Console.instance.Write("Error: Godmode must receive params 'on' or 'off'.");
            else if (arg == "off")
            {
                player.GetComponent<HealthComponent>().isInvincible = false;
                Console.instance.Write("godmode: off");
            }
            else if (arg == "on")
            {
                player.GetComponent<HealthComponent>().isInvincible = true;
                Console.instance.Write("godmode: on");
            }
            else
                Console.instance.Write("the argument " + arg + " is not valid.");
        }
    }
}
