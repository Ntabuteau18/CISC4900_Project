using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

/*
*The GameManager is the brains of the game coordinating every action made from each script and managing all of the game's logic from loading and displaying questions to checking the answers.
*
*The Menu method goes back to the main menu of the game.
*The Restart method restarts the quiz again.
*The SetHighScore method creates a new highscore if the current score is higher than the old highscore.
*The UpdateScore method is responsible for updating the current score that is seen on the screen.
*The GetRandomQuestionIndex method randomly retrieves an index from the remaining questions and returns it.
*The GetRandomQuestion method gets the question info selected from the random index in GetRandomQuestionIndex.
*The LoadQuestions method loads in the question objects that are in the Resources folder under Mathquestions and loads them all into the game.
*The CompareAnswers method checks the picked answer List to find the answer chosen and compares it to the actual correct answers and will send back true if it is right or false if it finds a right answer not chosen.
*The CheckAnswers method calls Compare Answers to check if what was returned is true or false.
*The IEnumerator WaitTillNextRound delays transitioning from one question to another for a second until the pop-up screen is gone.
*The IEnumerator StartTimer is a timer used on certain questions that dictates you only have a certain amount of time until the question auto-accepts whatever answer chosen.
*The UpdateTimer method is switching between if a question has a timer checked then use it, if not, don't use it.
*The Accept answer takes the answer choice made by the user, checks if it was right or wrong, displays the appropriate pop-up screen, updates the score and moves on to the next question after a delay. At the end of the game, it displays the Game Over screen. Once done it checks if a new highscore is needed to be set.
*The Display method puts the question text on the screen and if the question uses a timer, that timer is shown as well.
*The EraseAnswers method clears all answer choices made by the player by destroying them with a new empty list.
*The UpdateAnswers method takes in the answer choice made by the user and first looks if the question has a single answer, if so, then it clears all previously picked answers and sets the picked answer to the one the user made.
*
*On the Start method, it sets the starter highscore, loads the questions, and creates a random seed that initializes Unity's random class when picking random questions, so it's different every playthrough of the game.
*
*On the Enable and Disable, it subscribes to the delegate UpdateQuestionAnswer to inform it of changes in answers.
*
On the Awake method, it sets the current score to 0 each time the game starts.
*/



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
