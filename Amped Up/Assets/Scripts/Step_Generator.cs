using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Step_Generator : MonoBehaviour {

    public GameObject aNote;
    public GameObject bNote;
    public GameObject spcNote;
    public GameObject cNote;
    public GameObject dNote;

    public GameObject aBack;
    public GameObject bBack;
    public GameObject spcBack;
    public GameObject cBack;
    public GameObject dBack;

    public float arrowSpeed = 0;

    private bool isInit = false;
    private Song_Parser.Metadata songData;
    private float songTimer = 0.0f;
    private float barTime = 0.0f;
    private float barExecutedTime = 0.0f;
    private GameObject heart;
    private AudioSource heartAudio;
    private Song_Parser.difficulties difficulty;
    private Song_Parser.NoteData noteData;
    private float distance;
    private float originalDistance = 7.0f; //original value = 1.0f, sync seems to be going off because of this
    private float originalArrowSpeed = 0;
    private int barCount = 0;

    private Animator leftAnim;
    private Animator downAnim;
    private Animator upAnim;
    private Animator rightAnim;

	// Use this for initialization
	void Start () {
        heart = GameObject.FindGameObjectWithTag("Player");
        heartAudio = heart.GetComponent<AudioSource>();

        leftAnim = aBack.GetComponent<Animator>();
        downAnim = bBack.GetComponent<Animator>();
        upAnim = cBack.GetComponent<Animator>();
        rightAnim = dBack.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (leftAnim.GetBool("isLit"))
        {
            leftAnim.SetBool("isLit", false);
        }
        if (downAnim.GetBool("isLit"))
        {
            downAnim.SetBool("isLit", false);
        }
        if (rightAnim.GetBool("isLit"))
        {
            rightAnim.SetBool("isLit", false);
        }
        if (upAnim.GetBool("isLit"))
        {
            upAnim.SetBool("isLit", false);
        }

        if (isInit && barCount < noteData.bars.Count)
        {
            float pitch = heartAudio.pitch;
            arrowSpeed = originalArrowSpeed * pitch;

            distance = originalDistance;
            float timeOffset = distance * arrowSpeed;
            songTimer = heartAudio.time;

            if (songTimer - timeOffset >= (barExecutedTime - barTime))
            {
                StartCoroutine(PlaceBar(noteData.bars[barCount++]));

                barExecutedTime += barTime;
            }
        }
	}

    IEnumerator PlaceBar(List<Song_Parser.Notes> bar)
    {
        for (int i = 0; i < bar.Count; i++)
        {
            if (bar[i].aNote)
            {
                GameObject obj = (GameObject)Instantiate(aNote, new Vector3(aBack.transform.position.x, aBack.transform.position.y + distance, aBack.transform.position.z - 0.3f), Quaternion.identity);
                obj.GetComponent<Arrow_Movement>().arrowBack = aBack;
            }
            if (bar[i].bNote)
            {
                GameObject obj = (GameObject)Instantiate(bNote, new Vector3(bBack.transform.position.x, bBack.transform.position.y + distance, bBack.transform.position.z - 0.3f), Quaternion.identity);
                obj.GetComponent<Arrow_Movement>().arrowBack = bBack;
            }
            if (bar[i].spcNote)
            {
                GameObject obj = (GameObject)Instantiate(spcNote, new Vector3(spcBack.transform.position.x, spcBack.transform.position.y + distance, spcBack.transform.position.z - 0.3f), Quaternion.identity);
                obj.GetComponent<Arrow_Movement>().arrowBack = spcBack;
            }
            if (bar[i].cNote)
            {
                GameObject obj = (GameObject)Instantiate(cNote, new Vector3(cBack.transform.position.x, cBack.transform.position.y + distance, cBack.transform.position.z - 0.3f), Quaternion.identity);
                obj.GetComponent<Arrow_Movement>().arrowBack = cBack;
            }
            if (bar[i].dNote)
            {
                GameObject obj = (GameObject)Instantiate(dNote, new Vector3(dBack.transform.position.x, dBack.transform.position.y + distance, dBack.transform.position.z - 0.3f), Quaternion.identity);
                obj.GetComponent<Arrow_Movement>().arrowBack = dBack;
            }

            //very delicate because it needs to be precisely the number between bars
            //this has nothing to do with offset btw, adjust that in audacity manually or gtfo
            yield return new WaitForSeconds((barTime / bar.Count) - ((Time.deltaTime)/2)); // DIVIDING DELTA TIME BY TWO WORKS FOR NOW. DON'T TOUCH IT. FOR THE LOVE OF GOD DON'T TOUCH IT.
            Debug.Log(bar.Count);
        }
    }

    public void InitSteps(Song_Parser.Metadata newSongData, Song_Parser.difficulties newDifficulty)
    {
        songData = newSongData;
        isInit = true;
        barTime = (60.0f / songData.bpm) * 4.0f;
        difficulty = newDifficulty;
        distance = originalDistance;

        switch (difficulty)
        {
            //beginner and challenge are irrelevant
            //adjust accordingly for medium and easy, i've only done for hard so far
            case Song_Parser.difficulties.beginner:
                arrowSpeed = 0.007f;
                noteData = songData.beginner;
                break;
            case Song_Parser.difficulties.easy:
                arrowSpeed = 0.077f; //original value = 0.011f
                noteData = songData.easy;
                break;
            case Song_Parser.difficulties.medium:
                originalDistance = 7.5f;
                arrowSpeed = 0.154f; //original value = 0.011f
                noteData = songData.medium;
                break;
            case Song_Parser.difficulties.hard:
                arrowSpeed = 0.195f; //original value = 0.013f
                noteData = songData.hard;
                break;
            case Song_Parser.difficulties.challenge:
                originalArrowSpeed = 0.009f;
                arrowSpeed = 0.016f;
                noteData = songData.challenge;
                break;
            default:
                goto case Song_Parser.difficulties.easy;
        }

        originalArrowSpeed = arrowSpeed;
    }
}
