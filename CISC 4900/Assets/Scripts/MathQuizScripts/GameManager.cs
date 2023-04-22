using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    QuestionList[] questions = null;
    public QuestionList[] Questions { get { return questions; } }

    [SerializeField] GameEvents events = null;

    [SerializeField] Animator timerAnimator = null;

    [SerializeField] TextMeshProUGUI timerText = null;

    [SerializeField] Color timerHalfWay      = Color.yellow;
    [SerializeField] Color timerAlmostDone   = Color.red;
    private          Color timerDefaultColor = Color.black;

    private List<AnswerList> PickedAnswers = new List<AnswerList>();
    private List<int> FinishedQuestions    = new List<int>();
    private int currentQuestion            = 0;
    private int timerStateParaHash         = 0;

    private IEnumerator IEWaitTillNextRound = null;
    private IEnumerator IEStartTimer        = null;
    

    private bool IsFinished
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
        events.StartUpHighscore = PlayerPrefs.GetInt(GameUtil.SavePrefKey);

        timerDefaultColor = timerText.color;
        LoadQuestions();

        timerStateParaHash = Animator.StringToHash("TimerState");

        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);


        Display();
    }

    public void UpdateAnswers(AnswerList newAnswer)
    {
        if (Questions[currentQuestion].GetAnswerType == QuestionList.AnswerType.Single)
        {
            foreach (var answer in PickedAnswers)
            {
                if (answer != newAnswer)
                {
                    answer.Reset();
                }
                
            }
            PickedAnswers.Clear();
            PickedAnswers.Add(newAnswer);
        }
        else
        {
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);
            if (alreadyPicked)
            {
                PickedAnswers.Remove(newAnswer);
            }
            else
            {
                PickedAnswers.Add(newAnswer);
            }
        }
    }

    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswerList>();
    }

    void Display()
    {
        EraseAnswers();
        var question = GetRandomQuestion();

        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        } else{ Debug.Log("Error occured"); }

        if (question.UseTimer)
        {
            UpdateTimer(question.UseTimer);
        }
    }
    

    public void Accept()
    {
        UpdateTimer(false);
        bool isCorrect = CheckAnswers();
        FinishedQuestions.Add(currentQuestion);


        UpdateScore((isCorrect) ? Questions[currentQuestion].AddScore : -Questions[currentQuestion].AddScore);

        if (IsFinished)
        {
            SetHighScore();
        }

        var type
            = (IsFinished)
            ? UIManager.ResolutionScreenType.Finish
            : (isCorrect) ? UIManager.ResolutionScreenType.Correct
            : UIManager.ResolutionScreenType.Incorrect;

        if (events.DisplayResolutionScreen != null)
        {
            events.DisplayResolutionScreen(type, Questions[currentQuestion].AddScore);
        }

        
        if (IEWaitTillNextRound != null)
        {
            StopCoroutine(IEWaitTillNextRound);
        }
        IEWaitTillNextRound = WaitTillNextRound();
        StartCoroutine(IEWaitTillNextRound);
    }

     void UpdateTimer(bool state)
    {
        switch (state)
        {
            case true:
                IEStartTimer = StartTimer();
                StartCoroutine(IEStartTimer);

                timerAnimator.SetInteger(timerStateParaHash, 2);
                break;
            case false:
                if(IEStartTimer != null)
                {
                    StopCoroutine(IEStartTimer);
                }
                timerAnimator.SetInteger(timerStateParaHash, 1);
                break;
        }
    }
    IEnumerator StartTimer()
    {
        var totalTime = Questions[currentQuestion].Timer;
        var timeleft = totalTime;


        timerText.color = timerDefaultColor;
        while (timeleft > 0)
        {
            timeleft--;
            if(timeleft < totalTime / 2 && timeleft > totalTime/ 4)
            {
                timerText.color = timerHalfWay;
            }
            if(timeleft < totalTime / 4)
            {
                timerText.color = timerAlmostDone;
            }

            timerText.text = timeleft.ToString();
            yield return new WaitForSeconds(1f);
        }
        Accept();
    }

    IEnumerator WaitTillNextRound()
    {
        yield return new WaitForSeconds(GameUtil.ResolutionDelay);
        Display();
    }

    bool CheckAnswers()
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
            List<int> CorrectAnswers_ = Questions[currentQuestion].GetCorrectAnswers();
            List<int> PickedAnswers_ = PickedAnswers.Select(x => x.AnswerIndex).ToList();

            var n = CorrectAnswers_.Except(PickedAnswers_).ToList();
            var s = PickedAnswers_.Except(CorrectAnswers_).ToList();

            return !n.Any() && !s.Any();
        }
        return false;
    }

    void LoadQuestions()
    {
        Object[] obj = Resources.LoadAll("MathQuestions", typeof(QuestionList));
        questions = new QuestionList[obj.Length];
        for (int i = 0; i < obj.Length; i++)
        {
            questions[i] = (QuestionList)obj[i];
        }
    }

    QuestionList GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;
        return Questions[currentQuestion];
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
    
    

    private void UpdateScore(int add)
    {
        events.CurrentFinalScore += add;

        if(events.ScoreUpdated != null)
        {
            events.ScoreUpdated();
        }
    }

    private void SetHighScore()
    {
        var highscore = PlayerPrefs.GetInt(GameUtil.SavePrefKey);
        if (highscore < events.CurrentFinalScore)
        {
            PlayerPrefs.SetInt(GameUtil.SavePrefKey, events.CurrentFinalScore);
        }
    }

    /**
     *  Restarts the game
     * */
    public void Restart()
    {
        
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    /**
     *  Goes back to main menu
     * */
    public void menu()
    {
        SceneManager.LoadScene(0);
    }

}
