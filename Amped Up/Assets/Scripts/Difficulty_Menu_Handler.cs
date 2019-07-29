using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Difficulty_Menu_Handler : MonoBehaviour {

    public Text scoreText;
    public Text comboText;
    public Text notesText;
    public Text grade;
    public float totalNotes;
    public float notesHit;
    public Animator animator;

    

    public void SetBeginner() { Game_Data.difficulty = Song_Parser.difficulties.beginner; UnityEngine.SceneManagement.SceneManager.LoadScene(2); }
    public void SetEasy() { Game_Data.difficulty = Song_Parser.difficulties.easy; animator.SetTrigger("FadeOut"); }
    public void SetMedium() { Game_Data.difficulty = Song_Parser.difficulties.medium; animator.SetTrigger("FadeOut"); }
    public void SetHard() { Game_Data.difficulty = Song_Parser.difficulties.hard; animator.SetTrigger("FadeOut"); }
    public void SetChallenge() { Game_Data.difficulty = Song_Parser.difficulties.challenge; UnityEngine.SceneManagement.SceneManager.LoadScene(2); }
    public void GoToSongSelection() { animator.SetTrigger("FadeOut"); }

    void Start()
    {
        totalNotes = Game_Data.noteCount;
        notesHit = Game_Data.notesHit;

        scoreText.text = Game_Data.currentScore.ToString();
        comboText.text = Game_Data.maxCombo.ToString();
        notesText.text = Game_Data.notesHit.ToString();

        if (Game_Data.hasPassed)
        {
            if (((notesHit / totalNotes)*100) < 20)
            {
                grade.text = "F";
            }
            else if (((notesHit / totalNotes) * 100) < 40)
            {
                grade.text = "D";
            }
            else if (((notesHit / totalNotes) * 100) < 60)
            {
                grade.text = "C";
            }
            else if (((notesHit / totalNotes) * 100) < 80)
            {
                grade.text = "B";
            }
            else if (((notesHit / totalNotes) * 100) < 90)
            {
                grade.text = "A";
            }
            if (((notesHit / totalNotes) * 100) <= 100)
            {
                grade.text = "S";
            }
        }
        else
        {
            grade.text = "F";
        }

    }
}
