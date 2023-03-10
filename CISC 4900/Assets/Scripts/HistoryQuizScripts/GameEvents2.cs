using UnityEngine;

[CreateAssetMenu(fileName ="GameEvents2", menuName ="Quiz/new GameEvents2")]
public class GameEvents2 : ScriptableObject
{
    public delegate void UpdateQuestionUICallback(QuestionList2 question);
    public               UpdateQuestionUICallback UpdateQuestionUI = null;

    public delegate void UpdateQuestionAnswerCallback(AnswerList2 pickedAnswer);
    public               UpdateQuestionAnswerCallback UpdateQuestionAnswer = null;

    public delegate void DisplayResolutionScreenCallback(UIManager2.ResolutionScreenType type, int score);
    public               DisplayResolutionScreenCallback DisplayResolutionScreen = null;

    public delegate void ScoreUpdatedCallback();
    public               ScoreUpdatedCallback ScoreUpdated = null;

    
    public int CurrentFinalScore = 0;
   
    public int StartUpHighscore = 0;
}
