using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IUpdateable
{
    public static GameManager instance = null;

    [HideInInspector]
    public Player player = null;

    public GameObject bossPrefab;

    private int currentScene;
    private Enemy[] enemies = null;
    private Transform bossHolder;
    private bool bossSpawned = false;

    #region Singleton
    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public void OnDisable() {
        UpdateManager.Unregister(this);
    }

    private void OnEnable() {
        UpdateManager.Register(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void CustomUpdate() {
        if (Input.GetKeyDown(KeyCode.F1))
            Restart();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        player = FindObjectOfType<Player>();
        bossSpawned = false;
    }

    public void EndGame(string state)
    {
        Time.timeScale = 0f;
        UIManager.instance.ShowResultScreen(true);
        UIManager.instance.SetResultText(state);
    }

    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void CheckEnemiesAlive()
    {
        bossHolder = GameObject.Find("BossHolder").transform;
        enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length <= 0)
        {
            if (!bossSpawned) {
                GameObject boss = Instantiate(bossPrefab, bossHolder.position, Quaternion.identity).gameObject;
                boss.transform.parent = bossHolder.transform;
                bossSpawned = true;
            }
            else
                EndGame("WIN");
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

    public void Restart() {
        LoadLevel("Stage01");
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

    public void CustomLateUpdate() {
        return;
    }
}
