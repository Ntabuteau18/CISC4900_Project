using UnityEngine;

[CreateAssetMenu(fileName ="GameEvents", menuName ="Quiz/new GameEvents")]
public class GameEvents : ScriptableObject
{
    public delegate void UpdateQuestionUICallback(QuestionList question);
    public               UpdateQuestionUICallback UpdateQuestionUI = null;

    public delegate void UpdateQuestionAnswerCallback(AnswerList pickedAnswer);
    public               UpdateQuestionAnswerCallback UpdateQuestionAnswer = null;

    public delegate void DisplayResolutionScreenCallback(UIManager.ResolutionScreenType type, int score);
    public               DisplayResolutionScreenCallback DisplayResolutionScreen = null;

    public delegate void ScoreUpdatedCallback();
    public               ScoreUpdatedCallback ScoreUpdated = null;

   
    public int CurrentFinalScore = 0;
    
    public int StartUpHighscore = 0;
}
