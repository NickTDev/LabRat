using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSetter : MonoBehaviour
{
    //Music for the given area
    public AudioClip areaMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check that collided trigger is the player
        if(collision.gameObject.name == "MusicCheck") 
        {
            //Call the Music Handler Script and give it the new music
            collision.gameObject.GetComponent<MusicHandler>().StartNewMusic(areaMusic);
        }
    }
}
