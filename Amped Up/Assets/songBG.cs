using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

public class songBG : MonoBehaviour
{
    public GameObject music;
    public VideoClip movies;
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        Song_Parser.Metadata songData = Game_Data.chosenSongData;
        SpriteRenderer image = GetComponent<SpriteRenderer>();
        if (File.Exists(songData.backgroundPath))
        {
            Texture2D texture = new Texture2D(800, 600);
            texture.LoadImage(File.ReadAllBytes(songData.backgroundPath));
            Debug.Log(songData.bannerPath);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        if(songData.movies)
        {
            videoPlayer.clip = movies;
            videoPlayer.time = music.GetComponent<AudioSource>().time;
            videoPlayer.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
