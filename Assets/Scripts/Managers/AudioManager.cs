using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    #region Singleton

    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    void Awake() {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion 

    public AudioClip[] BGMs;
    public AudioClip BossBGM;

    private AudioSource _audio;

    private void Start() {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip bgm) {
        if (bgm) {
            print(_audio);
            if(_audio.isPlaying) {
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
}
