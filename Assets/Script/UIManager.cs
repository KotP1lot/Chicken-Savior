using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    string[] finaltext = new string[] {
        "Don't chick-en up, try again!",
        "Feather-brained move.",
    "That wasn't egg-cellent.",
    "Give it another shot, you're eggs-tremely close!",
    "You chicken out! Don't worry, you'll get another clucking chance.",
    "Winging it didn't work this time.",
    "Oops.. Chiken for dinner today."};
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField]TextMeshProUGUI topScoreText;
    [SerializeField]TextMeshProUGUI finalText;
    [SerializeField]GameObject endPanel;
    [SerializeField]GameObject pausePanel;
    Animator animator;
    EventSyst syst;
    bool isEnded = false;
    int score = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isEnded) 
        {
            OnPause();
        }
    }
    void OnDead(TypeDie typeDie)
    {
        finalText.text = finaltext[Random.Range(0,finaltext.Length)];
        isEnded = true;
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
        SceneManager.LoadScene(1);
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
    public void OnPause() 
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void OnResume() 
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void BackMainMenu() 
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
