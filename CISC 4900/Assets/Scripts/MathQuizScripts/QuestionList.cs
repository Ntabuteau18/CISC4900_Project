using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/*This is the QuestionList file that allows us to create a new Question object and have multiple attributes associated with
 * them. These attributes include if the answer is single or multiple choice, does the question use a timer, the actual question
 * text, how much points you earn for a correct answer, and the answer itself. These options are Serializable as it allows us to
 * manipulate all of them from within the Unity Editor. 
 * 
 * The GetCorrectAnswers List holds all the indexes of the correct answers for their respective questions.
 * 
 * 
 */
[Serializable()]
public struct Answer
{
    [SerializeField] private string info;
    public string Info { get { return info; } }

    [SerializeField] private bool isCorrect;
    public bool IsCorrect {  get { return isCorrect; } }
}
[CreateAssetMenu(fileName = "new Question", menuName = "Quiz/new Question")]
public class QuestionList : ScriptableObject
{
    public enum AnswerType { Multiple, Single}

    [SerializeField] private string info = string.Empty;
    public string Info { get { return info; } }

    [SerializeField] Answer[] answers = null;
    public Answer[] Answers {  get { return answers; } }

    // Parameters

    [SerializeField] private bool useTimer = false;
    public bool UseTimer { get { return useTimer; } }

    [SerializeField] private int timer = 0;
    public int Timer {  get { return timer; } }

    [SerializeField] private AnswerType answerType = AnswerType.Multiple;
    public AnswerType GetAnswerType { get { return answerType; } }

    [SerializeField] private int addScore = 10;
    public int AddScore { get { return addScore; } }

    public List<int> GetCorrectAnswers()
    {
        List<int> CorrectAnswers = new List<int>();
        for (int i = 0; i < Answers.Length; i++)
        {
            if (Answers[i].IsCorrect)
            {
                CorrectAnswers.Add(i);
            }          
        }
        return CorrectAnswers;
    }
}
