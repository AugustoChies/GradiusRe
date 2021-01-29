using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioMaster : ScriptableObject
{
    public delegate void PlayMusic(AudioClip clip);
    public delegate void ChangeMusic();

    public PlayMusic PlayNewMusicCommand, playSoundCommand;
    public ChangeMusic PauseMusicCommand,StopMusicCommand,PlayCurrentMusicCommand;

}
