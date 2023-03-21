using UnityEngine;

[CreateAssetMenu(fileName ="GameEvents3", menuName ="Quiz/new GameEvents3")]
public class GameEvents3 : ScriptableObject
{
    public delegate void UpdateQuestionUICallback(QuestionList3 question);
    public               UpdateQuestionUICallback UpdateQuestionUI = null;

    public delegate void UpdateQuestionAnswerCallback(AnswerList3 pickedAnswer);
    public               UpdateQuestionAnswerCallback UpdateQuestionAnswer = null;

    public delegate void DisplayResolutionScreenCallback(UIManager3.ResolutionScreenType type, int score);
    public               DisplayResolutionScreenCallback DisplayResolutionScreen = null;

    public delegate void ScoreUpdatedCallback();
    public               ScoreUpdatedCallback ScoreUpdated = null;

    
    public int CurrentFinalScore = 0;
   
    public int StartUpHighscore = 0;
}
