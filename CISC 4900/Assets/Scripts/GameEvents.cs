using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvents", menuName = "Quiz/new GameEvents")]
public class GameEvents : ScriptableObject
{

public delegate void UpdateQuestionUICallback(Questions question);
public UpdateQuestionUICallback UpdateQuestionUI;

public delegate void UpdateQuestionAnswerCallback(AnswersSheet pickedAns);
public UpdateQuestionAnswerCallback UpdateQuestionAnswer ;

public delegate void DisplayResolutionCallback(UIManager.ResolutionScreenType type, int score);
public DisplayResolutionCallback DisplayResolutionScreen;

public delegate void ScoreUpdateCallback();
public ScoreUpdateCallback ScoreUpdated;

[HideInInspector]
public int CurrentFinalScore;

[HideInInspector]
public int StarterHighscore;
}
