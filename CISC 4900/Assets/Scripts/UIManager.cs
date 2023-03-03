using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[Serializable()]
public struct UIManagerParameters
{
    [Header("Answer Options")]
    [SerializeField] float margins;
    public float Margins { get { return margins; } }

    [Header("Resolution Screen Options")]
    
    [SerializeField] Color correctBGColor;
    public Color CorrectBGColor { get { return correctBGColor;  } }
    
    [SerializeField] Color incorrectBGColor;
    public Color InCorrectBGColor { get { return incorrectBGColor; } }
    
    [SerializeField] Color finalBGColor;
    public Color FinalBGColor { get { return finalBGColor; } }
}

[Serializable()]
public struct UIElements
{
    [SerializeField] RectTransform ansContentArea;
    public RectTransform AnsContentArea { get { return ansContentArea; } }

    [SerializeField] TextMeshProUGUI questionInfoTextObject;
    public TextMeshProUGUI QuestionInfoTextObject { get { return questionInfoTextObject; } }

    [SerializeField] TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    [Space]

    [SerializeField] Animator resolutionScreenAnimator;
    public Animator ResolutionScreenAnimator { get { return resolutionScreenAnimator; } }

    [SerializeField] Image resolutionBG;
    public Image ResolutionBG { get { return resolutionBG; } }

    [SerializeField] TextMeshProUGUI resolutionStateTxt;
    public TextMeshProUGUI ResolutionStateTxT { get { return resolutionStateTxt; } }

    [SerializeField] TextMeshProUGUI resolutionScoreTxt;
    public TextMeshProUGUI ResolutionScoreTxt { get { return resolutionScoreTxt; } }


    [Space]
    [SerializeField] TextMeshProUGUI highScoreTxt;
    public TextMeshProUGUI HighScoreTxt { get { return highScoreTxt; } }

    [SerializeField] CanvasGroup mainCanvasGroup;
    public CanvasGroup MainCanvasGroup { get { return mainCanvasGroup; } }

    [SerializeField] RectTransform finishedUIElements;
    public RectTransform FinishedUIElements { get { return finishedUIElements; } }
}
public class UIManager : MonoBehaviour
{
  public enum ResolutionScreenType{ Correct,Incorrect, Finish}

    [Header("References")]
    [SerializeField] GameEvents events;

    [Header("UI Elements (Prefabs")]
    [SerializeField] AnswersSheet ansPrefab;

    [SerializeField] UIElements uiElements;

    [Space]
    [SerializeField] UIManagerParameters parameters;

    List<AnswersSheet> currentAnswer = new List<AnswersSheet>();

    private IEnumerator IEDisplayTimeRes;
    private int resStateParameterHash = 0;

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
        resStateParameterHash = Animator.StringToHash("ScreenState");  
    }
    void DisplayResolution(ResolutionScreenType type, int score)
    {
        UpdateResolutionUI(type, score);
        uiElements.ResolutionScreenAnimator.SetInteger(resStateParameterHash, 2);
        uiElements.MainCanvasGroup.blocksRaycasts = false;

        if(type != ResolutionScreenType.Finish)
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
        yield return new WaitForSeconds(GameUtil.ResolutionDelay);
        uiElements.ResolutionScreenAnimator.SetInteger(resStateParameterHash, 1);
        uiElements.MainCanvasGroup.blocksRaycasts = true;
    }
    
    void UpdateResolutionUI(ResolutionScreenType type, int score)
    {
        var highscore = PlayerPrefs.GetInt(GameUtil.SavePrefKey);
        switch (type)
        {
            case ResolutionScreenType.Correct:
                uiElements.ResolutionBG.color = parameters.CorrectBGColor;
                uiElements.ResolutionStateTxT.text = "CORRECT!";
                uiElements.ResolutionScoreTxt.text = "+" + score;
                break;
            case ResolutionScreenType.Incorrect:
                uiElements.ResolutionBG.color = parameters.InCorrectBGColor;
                uiElements.ResolutionStateTxT.text = "WRONG!";
                uiElements.ResolutionScoreTxt.text = "-" + score;
                break;
            case ResolutionScreenType.Finish:
                uiElements.ResolutionBG.color = parameters.FinalBGColor;
                uiElements.ResolutionStateTxT.text = "FINAL SCORE!";
                uiElements.ResolutionScoreTxt.text = "+" + score;

                StartCoroutine(CalculateScore());
                uiElements.FinishedUIElements.gameObject.SetActive(true);
                uiElements.HighScoreTxt.gameObject.SetActive(true);
                uiElements.HighScoreTxt.text = ((highscore > events.StarterHighscore) ? "<color=yellow>new </color>" : string.Empty) + "Highscore " + highscore;
                break;
            
        }


    }
    IEnumerator CalculateScore()
    {
        var scoreValue = 0;
        while (scoreValue < events.CurrentFinalScore)
        {
            scoreValue++;
            uiElements.ResolutionScoreTxt.text = scoreValue.ToString();

            yield return null;
        }
    }

    void UpdateQuestionUI(Questions question)
    {
        uiElements.QuestionInfoTextObject.text = question.Info;
        CreateAnswers(question);
    }
    void CreateAnswers(Questions question)
    {
        EraseAnswers();

        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswersSheet newAnswer = (AnswersSheet)Instantiate(ansPrefab, uiElements.AnsContentArea);
            newAnswer.UpdateData(question.Answers[i].Info, i);

            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);

            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
            uiElements.AnsContentArea.sizeDelta = new Vector2(uiElements.AnsContentArea.sizeDelta.x, offset * -1);

            currentAnswer.Add(newAnswer);
        }
    }
    void EraseAnswers()
    {
        foreach(var answer in currentAnswer)
        {
            Destroy(answer.gameObject);
        }
        currentAnswer.Clear();
    }
    void UpdateScoreUI()
    {
        uiElements.ScoreText.text = "Score: " + events.CurrentFinalScore;
    }

}
