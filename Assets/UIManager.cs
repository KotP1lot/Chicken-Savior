using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField]TextMeshProUGUI topScoreText;
    [SerializeField]GameObject endPanel;
    Animator animator;
    EventSyst syst;
    int score = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnDead(TypeDie typeDie)
    {
        int preScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score >= preScore) 
        {
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
        }
        topScoreText.text = "TOP " + preScore.ToString();
        animator.Play("End");
    }

    public void OnRestart() 
    {
        SceneManager.LoadScene(0);
    
    }

    void OnStepForward() 
    {
        score++;
        scoreText.text = score.ToString();
    }
    private void OnEnable()
    {
        syst = GameObject.Find("EventSys").GetComponent<EventSyst>();
        syst.OnDead += OnDead; 
        syst.OnStepForward += OnStepForward; 
    }
    private void OnDisable()
    {
        syst.OnDead -= OnDead;
        syst.OnStepForward -= OnStepForward;
    }
}
