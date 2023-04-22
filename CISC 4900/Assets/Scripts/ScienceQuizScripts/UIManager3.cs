using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[Serializable()]
public struct UIManagerParameters3
{
    [Header("Answer Options")]
    [SerializeField] float margins;
    public float Margins { get { return margins; } }

    [Header("Resolution Screen Options")]
    
    [SerializeField] Color correctBGColor;
    public Color CorrectBGColor { get { return correctBGColor; } }
    
    [SerializeField] Color incorrectBGColor;
    public Color IncorrectBGColor { get { return incorrectBGColor; } }
    
    [SerializeField] Color finalBGColor;
    public Color FinalBGColor { get { return finalBGColor; } }
}
[Serializable()]
public struct UIElements3
{
    [SerializeField] RectTransform answersContentArea;
    public RectTransform AnswersContentArea { get { return answersContentArea; } }

    [SerializeField] TextMeshProUGUI questionInfoTextObject;
    public TextMeshProUGUI QuestionInfoTextObject { get { return questionInfoTextObject; } }

    [SerializeField] TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText { get { return scoreText; } }
    [Space]

    [SerializeField] Animator resolutionScreenAnimator;
    public Animator ResolutionScreenAnimator { get { return resolutionScreenAnimator; } }

    [SerializeField] Image resolutionBG;
    public Image ResolutionBG { get { return resolutionBG; } }

    [SerializeField] TextMeshProUGUI resolutionStateInfoText;
    public TextMeshProUGUI ResolutionStateInfoText { get { return resolutionStateInfoText; } }

    [SerializeField] TextMeshProUGUI resolutionScoreText;
    public TextMeshProUGUI ResolutionScoreText { get { return resolutionScoreText; } }

    [Space]
    [SerializeField] TextMeshProUGUI highScoreText;
    public TextMeshProUGUI HighScoreText { get { return highScoreText; } }

    [SerializeField] CanvasGroup mainCanvasGroup;
    public CanvasGroup MainCanvasGroup { get { return mainCanvasGroup; } }

    [SerializeField] RectTransform finishedUIElements;
    public RectTransform FinishedUIElements { get { return finishedUIElements; } }
}

public class UIManager3 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    public enum ResolutionScreenType {Correct, Incorrect, Finish}

    [Header("References")]
    [SerializeField] GameEvents3 events = null;

    [Header("UI Elements (Prefabs)")]
    [SerializeField] AnswerList3 answerPrefab = null;
    [SerializeField] UIElements3 uIElements = new UIElements3();

    [Space]
    [SerializeField] UIManagerParameters3 parameters = new UIManagerParameters3();

    List<AnswerList3> currentAnswer = new List<AnswerList3>();
    private int resolutionStateParameterHash = 0;
    private IEnumerator IEDisplayTimeRes = null;

     void OnEnable()
    {
        events.UpdateQuestionUI += UpdateQuestionUI;
        events.DisplayResolutionScreen += DisplayResolution;
        events.ScoreUpdated += UpdateScoreUI;
    }
     void OnDisable()
    {
        events.UpdateQuestionUI -= UpdateQuestionUI;
        events.DisplayResolutionScreen -= DisplayResolution;
        events.ScoreUpdated -= UpdateScoreUI;
    }
     void Start()
    {
        UpdateScoreUI();
        resolutionStateParameterHash = Animator.StringToHash("ScreenState"); 

    }
    

    void UpdateQuestionUI(QuestionList3 question)
    {
        uIElements.QuestionInfoTextObject.text = question.Info;
        CreateAnswers(question);
    }

    void DisplayResolution(ResolutionScreenType type, int score)
    {
        UpdateResolutionUI(type, score);
        uIElements.ResolutionScreenAnimator.SetInteger(resolutionStateParameterHash, 2);
        uIElements.MainCanvasGroup.blocksRaycasts = false;

        if (type != ResolutionScreenType.Finish)
        {
            if(IEDisplayTimeRes != null)
            {
                StopCoroutine(IEDisplayTimeRes);
            }
            IEDisplayTimeRes = DisplayTimeResolution();
            StartCoroutine(IEDisplayTimeRes);
        }
    }

    IEnumerator DisplayTimeResolution()
    {
        yield return new WaitForSeconds(GameUtil3.ResolutionDelay);
        uIElements.ResolutionScreenAnimator.SetInteger(resolutionStateParameterHash, 1);
        uIElements.MainCanvasGroup.blocksRaycasts = true;
    }

    void UpdateResolutionUI(ResolutionScreenType type, int score)
    {
        var highscore = PlayerPrefs.GetInt(GameUtil3.SavePrefKey);

        switch (type)
        {
            case ResolutionScreenType.Correct:
                uIElements.ResolutionBG.color = parameters.CorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "CORRECT";
                uIElements.ResolutionScoreText.text = "+" + score;
                audioSource.PlayOneShot(correctSound);
                break;
            case ResolutionScreenType.Incorrect:
                uIElements.ResolutionBG.color = parameters.IncorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "WRONG";
                uIElements.ResolutionScoreText.text = "-" + score;
                audioSource.PlayOneShot(incorrectSound);
                break;
            case ResolutionScreenType.Finish:
                uIElements.ResolutionBG.color = parameters.FinalBGColor;
                uIElements.ResolutionStateInfoText.text = "Final Score";
                uIElements.ResolutionScoreText.text = events.CurrentFinalScore.ToString();

                StartCoroutine(CalculateScore());
                uIElements.FinishedUIElements.gameObject.SetActive(true);
                uIElements.HighScoreText.gameObject.SetActive(true);
                uIElements.HighScoreText.text = ((highscore > events.StartUpHighscore) ? "<color=yellow>new </color>" : string.Empty) + "Highscore: " + highscore;
                break;
 
       
        }
    }

    IEnumerator CalculateScore()
    {
        var scoreValue = 0;
        while (scoreValue < events.CurrentFinalScore)
        {
            scoreValue++;
            uIElements.ResolutionScoreText.text = scoreValue.ToString();
            yield return null;
        }
    }

    void CreateAnswers(QuestionList3 question)
    {
        EraseAnswers();

        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerList3 newAnswer = (AnswerList3)Instantiate(answerPrefab, uIElements.AnswersContentArea);
            newAnswer.UpdateData(question.Answers[i].Info, i);

            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);

            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
            uIElements.AnswersContentArea.sizeDelta = new Vector2(uIElements.AnswersContentArea.sizeDelta.x, offset * -1);

            currentAnswer.Add(newAnswer);
        }
    }

    void EraseAnswers()
    {
        foreach (var answer in currentAnswer)
        {
            Destroy(answer.gameObject);
        }
        currentAnswer.Clear();
    }

    void UpdateScoreUI()
    {
        uIElements.ScoreText.text = " " + events.CurrentFinalScore;
    }
}

