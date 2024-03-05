using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioSource playerAudio;
    public void StartNewMusic(AudioClip newSong) 
    {  
        //If the requested song is not already playing
        if(newSong != playerAudio.clip) 
        {
            //Lower volume of music player
            while (playerAudio.volume > 0)
            {
                playerAudio.volume = (float)(playerAudio.volume - 0.1);
            }
            //Stop the music, set the music to new area's music then raise the volume and start playing
            playerAudio.Stop();
            playerAudio.clip = newSong;
            playerAudio.volume = 1;
            playerAudio.Play();
        }
    }
}
