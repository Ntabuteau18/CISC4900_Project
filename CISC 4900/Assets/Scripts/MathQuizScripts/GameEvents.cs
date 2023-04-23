using UnityEngine;


/*
 * This is the GameEvents script, that allows parts of the game to communicate with each other using delegates and allows the creation
 * of custom game event objects. The delegates in this script are custom event handlers that other scripts can subscribe to when necessary to
 * make changes.
 * 
 * The UpdateQuestionAnswer delegate is  used by the AnswerList script to tell other scripts when the player has made thier choice.
 * The UpdateQuestionUI delegate is used to update the questions shown during the game.
 * The DisplayResolutionScreen delegate is used by the UIManager to show a pop-up screen displaying correct or wrong and the amount of points gained or lossed.
 * The ScoreUpdated delegate is used to tell other scripts when the score has been updated.
 * 
 * The CurrentFinalScore stores the current score of the game.
 * The StartUpHighscore  is used to store the user's high score at the beginning of the game but can be changed if the player achieves a higher score than this one.
 */
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
