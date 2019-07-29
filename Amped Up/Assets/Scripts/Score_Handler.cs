using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Score_Handler : MonoBehaviour {

    public float score = 0.0f;
    public float multiplier = 1.0f;
    public int combo = 0;
    public int maxCombo = 0;
    public int notesHit = 0;
    public int noteCount = 0;
    public Text scoreText;
    public Text comboText;
    public Text maxComboText;
    public Text comboCounter;
    public GameObject judgementSick;
    public GameObject judgementMiss;
    public GameObject canvas;
    private GameObject[] judgement;
    


    private Heartbeat_Controller heartScript;
    private AudioSource audioSource;
    private GameObject heart;
    private Renderer render;
    private List<GameObject> arrows;

    private const float scoreVal = 100.0f;
    private const float strumOffset = 0.1f;

	// Use this for initialization
	void Start () {
        Song_Parser.Metadata meta = Game_Data.chosenSongData;
        heart = GameObject.FindGameObjectWithTag("Player");
        heartScript = heart.GetComponent<Heartbeat_Controller>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(LoadTrack(meta.musicInst, meta)); //loads instrument track, omit this and the loadtrack coroutine if we are cancelling this
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + combo.ToString();
        maxComboText.text = "Max Combo: " + maxCombo.ToString();
        Game_Data.currentScore = score;
        Game_Data.maxCombo = maxCombo;
        Game_Data.notesHit = notesHit;
        Game_Data.noteCount = noteCount;
        if(combo<10)
        {
            comboCounter.text = "";
        }
        else
        {
            comboCounter.text = combo.ToString();
        }
            }

    IEnumerator LoadTrack(string path, Song_Parser.Metadata meta)
    {
        Debug.Log(path);
        string url = string.Format("file://{0}", path);
        WWW www = new WWW(url);

        while (!www.isDone)
        {
            yield return null;
        }

        AudioClip clip = www.GetAudioClip(false, false);
        audioSource.clip = clip;

        Debug.Log("Loaded");
        audioSource.Play();
    }
    void AddScore()
    {
        score += scoreVal * multiplier;
    }

    void AddCombo()
    {
        combo++;
        notesHit++;
        noteCount++;
        if (combo > maxCombo)
        {
            maxCombo = combo;
           
        }
        if(audioSource.mute)
        {
            audioSource.mute = !audioSource.mute;
        }
        judgement = GameObject.FindGameObjectsWithTag("judgement");

        for (var i = 0; i < judgement.Length; i++)
        {
            Destroy(judgement[i]);
        }
        GameObject newJudgement = Instantiate(judgementSick, new Vector3(-248, 102, 0), Quaternion.identity) as GameObject;
        newJudgement.transform.SetParent(canvas.transform, false);
        newJudgement.transform.localScale = new Vector3(1, 1, 1);
    }

    void LoseScore()
    {
        score -= scoreVal * multiplier;
        if (score < 0)
        {
            score = 0;
        }
    }

    void LoseCombo()
    {
        noteCount++;
        combo = 0;
        if (!audioSource.mute)
        {
            audioSource.mute = !audioSource.mute;
        }

        judgement = GameObject.FindGameObjectsWithTag("judgement");

        for (var i = 0; i < judgement.Length; i++)
        {
            Destroy(judgement[i]);
        }
        GameObject newJudgement = Instantiate(judgementMiss, new Vector3(-248, 102, 0), Quaternion.identity) as GameObject;
        newJudgement.transform.SetParent(canvas.transform, false);
        newJudgement.transform.localScale = new Vector3(1, 1, 1);
    }
}
