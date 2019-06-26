using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IUpdateable {

    [HideInInspector]
    public Player player = null;

    public GameObject bossPrefab;

    public AudioClip[] BGMs;
    public AudioClip hitSFX;
    public AudioClip deathSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip BossBGM;

    private AudioSource _audio;
    private int currentScene;
    private Enemy[] enemies = null;
    private Transform bossHolder;
    private bool bossSpawned = false;
    private bool bossStage;
    private int currentSceneIndex { get { return SceneManager.GetActiveScene().buildIndex; } }

    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    void Awake() {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(gameObject);
        _audio = GetComponent<AudioSource>();
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Time.timeScale = 1f;
        player = FindObjectOfType<Player>();
        bossSpawned = false;
        PlayMusic(BGMs[scene.buildIndex]);
    }

    public void EndGame(string state) {
        UIManager.instance.ShowResultScreen(true);
        UIManager.instance.SetResultText(state);

        AudioClip clip = state == "WIN" ? winSFX : loseSFX;
        if (clip)
            PlaySFX(clip);

        if (state == "WIN")
            Invoke("NextLevel", 3);
        else
            Time.timeScale = 0f;
    }

    public void NextLevel() {
        SceneManager.LoadScene(currentSceneIndex + 1, LoadSceneMode.Single);
    }

    public void LoadLevel(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void LoadLevel(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void CheckEnemiesAlive() {
        bossHolder = GameObject.Find("BossHolder").transform;
        enemies = FindObjectsOfType<Enemy>();
        bossStage = FindObjectOfType<SpawnManager>()._bossCanSpawn;
        if (enemies.Length <= 0) {
            if (!bossSpawned && bossStage) {
                GameObject boss = Instantiate(bossPrefab, bossHolder.position, Quaternion.identity).gameObject;
                boss.transform.parent = bossHolder.transform;
                bossSpawned = true;
                PlayMusic(BossBGM);
            }
            else
                EndGame("WIN");
        }
        print("Current amount of enemies: " + enemies.Length);
    }

    public void DestroyEnemies() {
        CheckEnemiesAlive();
        if (enemies.Length > 0) {
            foreach (Enemy enemy in enemies) {
                enemy.GetComponent<HealthComponent>().TakeDamage(1000000);
                Console.instance.Write("All enemies destroyed. Close console to resume.");
            }
        }
    }

    public void Aggro() {
        CheckEnemiesAlive();
        if (enemies.Length > 0) {
            foreach (Enemy enemy in enemies) {
                enemy.GetComponent<AIComponent>().ForceTargetPlayer();
            }
        }
    }

    public void Restart() {
        LoadLevel(currentSceneIndex);
    }

    public void GodMode(string arg) {
        if (player) {
            if (arg == "" || arg == null)
                Console.instance.Write("Error: Godmode must receive params 'on' or 'off'.");
            else if (arg == "off") {
                player.GetComponent<HealthComponent>().isInvincible = false;
                Console.instance.Write("godmode: off");
            }
            else if (arg == "on") {
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

    #region SoundManager
    public void PlayMusic(AudioClip bgm) {
        if (bgm) {
            print(_audio);
            if (_audio.isPlaying) {
                if (_audio.clip == bgm) {
                    return;
                }
                _audio.clip = bgm;
            }
            _audio.Play();
        }
    }

    public void PlaySFX(AudioClip sfx) {
        if (sfx) {
            _audio.PlayOneShot(sfx);
        }
    }

    public void StopMusic() {
        _audio.Stop();
    }
    #endregion
}
