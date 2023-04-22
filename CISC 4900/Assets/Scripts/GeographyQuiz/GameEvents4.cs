using UnityEngine;

[CreateAssetMenu(fileName ="GameEvents4", menuName ="Quiz/new GameEvents4")]
public class GameEvents4 : ScriptableObject
{
    public delegate void UpdateQuestionUICallback(QuestionList4 question);
    public               UpdateQuestionUICallback UpdateQuestionUI = null;

    public delegate void UpdateQuestionAnswerCallback(AnswerList4 pickedAnswer);
    public               UpdateQuestionAnswerCallback UpdateQuestionAnswer = null;

    public delegate void DisplayResolutionScreenCallback(UIManager4.ResolutionScreenType type, int score);
    public               DisplayResolutionScreenCallback DisplayResolutionScreen = null;

    public delegate void ScoreUpdatedCallback();
    public               ScoreUpdatedCallback ScoreUpdated = null;

    
    public int CurrentFinalScore = 0;
   
    public int StartUpHighscore = 0;
}
