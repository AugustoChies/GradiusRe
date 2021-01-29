using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public GlobalStats stats;
    public ControlsObj controls;
    public AudioSource source,pause;
    public AudioMaster master;
    // Start is called before the first frame update
    void Awake()
    {
        source = this.GetComponent<AudioSource>();
        master.PlayNewMusicCommand += PlayNewSong;
        master.PauseMusicCommand += PauseCurrentSong;
        master.StopMusicCommand += StopCurrentSong;
        master.PlayCurrentMusicCommand += PlayCurrentSong;
    }

    private void Update()
    {
        if (Input.GetKeyDown(controls.start))
        {
            if (stats.paused)
            {
                Time.timeScale = 1;
                source.Play();
                stats.paused = false;
            }
            else
            {
                Time.timeScale = 0;
                source.Pause();
                pause.Play();
                stats.paused = true;
            }
        }
    }

    public void PlayNewSong(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void PlayCurrentSong()
    {
        source.Play();
    }

    public void PauseCurrentSong()
    {
        source.Pause();
    }

    public void StopCurrentSong()
    {
        source.Stop();
    }
}
