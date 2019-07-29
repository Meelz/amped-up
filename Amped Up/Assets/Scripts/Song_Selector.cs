using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Song_Selector : MonoBehaviour {

    public GameObject songSelectionTemplate;
    public GameObject songSelectionList;
    public Text titleText;
    public Text artistText;
    public Text difficultyText;
    public Animator animator;
    public Animator loading;
    private AudioClip[] clip = new AudioClip[12];
    private AudioSource audioSource;
    public GameObject backgroundImage;


    private string currentSongPath;

    private float audioStartTime;
    private float audioLength;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        Parse();
    }
	
	// Update is called once per frame
	void Update () {
        
	    if (audioSource.time >= (audioStartTime + audioLength))
        {
            audioSource.Play();
            audioSource.time = audioStartTime;
        }

        if (audioSource.volume < 0.95f)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1.0f, Time.deltaTime);
        }
        else
        {
            audioSource.volume = 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animator.SetTrigger("EscapeOut");
        }
    }

    void Parse()
    {
        Debug.Log("Parsing");
        DirectoryInfo info = new DirectoryInfo(Game_Data.songDirectory);
        FileInfo[] smFiles = info.GetFiles("*.sm", SearchOption.AllDirectories);
        
        Debug.Log("Parsing Dir: " + Game_Data.songDirectory + " | Amount: " + smFiles.Length);
        for (int i = 0; i < smFiles.Length; i++)
        {
            Song_Parser parser = new Song_Parser();
            Song_Parser.Metadata songData = parser.Parse(smFiles[i].FullName);

            audioStartTime = songData.sampleStart;
            audioLength = songData.sampleLength;

            if (!songData.valid)
            {
                //Song data isnt valid
                continue;
            }
            else
            {
                GameObject songObj = (GameObject)Instantiate(songSelectionTemplate, songSelectionList.transform.position, Quaternion.identity);
                songObj.GetComponentInChildren<Text>().text = songData.title + " - " + songData.artist;
                songObj.transform.parent = songSelectionList.transform;
                songObj.transform.localScale = new Vector3(1, 1, 1); //Scale changes for some reason - reset it

                StartCoroutine(LoadPreview(songData.musicPath, i));

                //Get access to the button control
                Button songBtn = songObj.GetComponentInChildren<Button>();
                if (File.Exists(songData.bannerPath))
                {
                    Texture2D texture = new Texture2D(275, 52);
                    texture.LoadImage(File.ReadAllBytes(songData.bannerPath));
                    Debug.Log(songData.bannerPath);
                    songBtn.image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    songBtn.image.material.SetColor("_Color", Color.white);
                    songObj.GetComponentInChildren<Text>().enabled = false;
                }
                songBtn.onClick.AddListener(delegate { StartSong(songData); });

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.Select;
                int localClipIndexCopy = i;
                entry.callback.AddListener(eventData =>
                {
                    if (songData.musicPath != currentSongPath)
                    {
                        Debug.Log(songData.musicPath);
                        Debug.Log(localClipIndexCopy);
                        StartCoroutine(PreviewTrack(songData.musicPath, localClipIndexCopy));
                        Song_Parser.Metadata meta = Game_Data.chosenSongData;
                        titleText.text = songData.title;
                        artistText.text = songData.artist;

                        Texture2D texture = new Texture2D(800, 600);
                        texture.LoadImage(File.ReadAllBytes(songData.backgroundPath));
                        backgroundImage.GetComponentInChildren<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);


                        difficultyText.text = "Easy					" + songData.easyDiff + "\n" + "Medium				" + songData.medDiff + "\n" + "Hard					" + songData.hardDiff;        
                    }
                });

                songBtn.GetComponent<EventTrigger>().triggers.Add(entry);
            }
        }
        loading.SetTrigger("LoadComplate"); // i made a dumb typo
    }

    IEnumerator LoadPreview(string musicPath, int songNo)
    {
        string url = string.Format("file://{0}", musicPath);
        WWW www = new WWW(url);

        while (!www.isDone)
        {
            yield return null;
        }
        clip[songNo] = www.GetAudioClip(false, false);
    }
    IEnumerator PreviewTrack(string musicPath, int i)
    {
        Debug.Log("Starting Preview for " + musicPath);
        audioSource.clip = clip[i];

        Debug.Log("Loaded");

        audioSource.Play();
        audioSource.time = audioStartTime;

        currentSongPath = musicPath;

        audioSource.volume = 0;
        yield return null;
    }

    void StartSong(Song_Parser.Metadata songData)
    {
        //Do stuff
        Debug.Log(songData.title + " Chosen!");
        Game_Data.chosenSongData = songData;
        audioSource.mute = !audioSource.mute;
        animator.SetTrigger("FadeOut");
    }
}
