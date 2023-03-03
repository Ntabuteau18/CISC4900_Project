using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Questions[] questions = null;
    public Questions[] Questions { get { return questions; } }

    [SerializeField] GameEvents events = null;

    [SerializeField] Animator timerAnimator = null;
    [SerializeField] TextMeshProUGUI timerText = null;

    [SerializeField] Color timerHalfway = Color.blue;
    [SerializeField] Color timerAlmostDone = Color.red;


    private List<AnswersSheet> PickedAnswers = new List<AnswersSheet>();
    private List<int> FinishedQuestions = new List<int>();
    private int currentQuestion = 0;

    private int timerStateParameterHash = 0;

    private IEnumerator IEWaitTillNextRound = null;
    private IEnumerator IEStartTimer = null;
    private Color timerDefaultColor = Color.white;

    private bool isFinished
    {
        get
        {
            return (FinishedQuestions.Count < Questions.Length) ? false : true;
        }
    }

     void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
        
    }
    void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;

    }
    void Awake()
    {
        events.CurrentFinalScore = 0;
    }


    void Start()
    {
        events.StarterHighscore = PlayerPrefs.GetInt(GameUtil.SavePrefKey);
        timerDefaultColor = timerText.color;
        loadQuestions();
        

        timerStateParameterHash = Animator.StringToHash("TimerState");

        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);
        foreach (var question in Questions)
        {
            Debug.Log(question.Info);
        }
        Display();
    }
    public void UpdateAnswers(AnswersSheet newAns)
    {
        if (Questions[currentQuestion].GetAnswerType == global::Questions.AnswerType.Single)
        {
            foreach (var answer in PickedAnswers)
            {
                if (answer != newAns)
                {
                    answer.Reset();
                }
                PickedAnswers.Clear();
                PickedAnswers.Add(newAns);
            }
        }
        else
        {
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAns);
            if (alreadyPicked)
            {
                PickedAnswers.Remove(newAns);
            }
            else
            {
                PickedAnswers.Add(newAns);
            }
        }
    }



    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswersSheet>();
    }

    void Display()
    {
        EraseAnswers();
        var question = GetRandomQuestion();

        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        }
        else
        {
            Debug.LogWarning("Error occured");
        }

        if (question.UseTimer)
        {
            UpdateTimer(question.UseTimer);
        }
    }
    public void Accept()
    {
        UpdateTimer(false);
        bool isCorrect = CheckAnswer();
        FinishedQuestions.Add(currentQuestion);

        
       
        UpdateScore((isCorrect) ? Questions[currentQuestion].PlusScore : -Questions[currentQuestion].PlusScore);

        if (isFinished)
        {
            SetHighScore();
        }

        var type = (isFinished) ? UIManager.ResolutionScreenType.Finish : (isCorrect) ? UIManager.ResolutionScreenType.Correct : UIManager.ResolutionScreenType.Incorrect;

        if(events.DisplayResolutionScreen != null)
        {
            events.DisplayResolutionScreen(type, Questions[currentQuestion].PlusScore);
        }

        if(IEWaitTillNextRound != null)
        {
            StopCoroutine(IEWaitTillNextRound);
        }
        IEWaitTillNextRound = WaitTillNextRound();
        StartCoroutine(IEWaitTillNextRound);
    }
    IEnumerator WaitTillNextRound()
    {
        yield return new WaitForSeconds(GameUtil.ResolutionDelay);
        Display();
    }

    Questions GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;
        return Questions[currentQuestion];
    }

    void UpdateTimer(bool state)
    {
        switch (state)      
        {
            case true:
                IEStartTimer = StartTimer();
                StartCoroutine(IEStartTimer);

                timerAnimator.SetInteger(timerStateParameterHash, 2);
                break;
            case false:
                if(IEStartTimer != null)
                {
                    StopCoroutine(IEStartTimer);
                }

                timerAnimator.SetInteger(timerStateParameterHash, 1);
                break;
        }
    }  
    IEnumerator StartTimer()
    {
        var totalTime = Questions[currentQuestion].Timer;
        var timeLeft = totalTime;

        timerText.color = timerDefaultColor;
        while (timeLeft > 0)
        {
            if(timeLeft < totalTime / 2 && timeLeft > totalTime / 4)
            {
                timerText.color = timerHalfway;
            }
            if (timeLeft < totalTime / 4)
            {
                timerText.color = timerAlmostDone;
            }


                timeLeft--;
            timerText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1f);
        }
        Accept();
    }

    int GetRandomQuestionIndex()
    {
        var random = 0;
        if (FinishedQuestions.Count < Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, Questions.Length);
            } while (FinishedQuestions.Contains(random) || random == currentQuestion);
        }
        return random;
    }

    private void SetHighScore()
    {
        var highscore = PlayerPrefs.GetInt(GameUtil.SavePrefKey);
        if (highscore < events.CurrentFinalScore)
        {
            PlayerPrefs.SetInt(GameUtil.SavePrefKey, events.CurrentFinalScore);
        }

    }


    void loadQuestions()
    {

        Object[] objs = Resources.LoadAll("Questions", typeof(Questions));
        questions = new Questions[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            questions[i] = (Questions)objs[i];
        }
    }
    bool CheckAnswer()
    {
        if (!CompareAnswers())
        {
            return false;
        }
        return true;
    }
    bool CompareAnswers()
    {
        if (PickedAnswers.Count > 0)
        {
            List<int> c = Questions[currentQuestion].GetCorrectAns();
            List<int> p = PickedAnswers.Select(x => x.AnsIndex).ToList();

            var f = c.Except(p).ToList();
            var s = p.Except(c).ToList();

            return !f.Any() && !s.Any();
        }
        return false;
    }
    private void UpdateScore(int add)
    {
        events.CurrentFinalScore += add;

        if(events.ScoreUpdated != null)
        {
            events.ScoreUpdated();
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
    }
}


