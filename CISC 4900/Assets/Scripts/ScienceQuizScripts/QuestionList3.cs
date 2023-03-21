using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable()]
public struct Answer3
{
    [SerializeField] private string info;
    public string Info { get { return info; } }

    [SerializeField] private bool isCorrect;
    public bool IsCorrect {  get { return isCorrect; } }
}
[CreateAssetMenu(fileName = "new Question3", menuName = "Quiz/new Question3")]
public class QuestionList3 : ScriptableObject
{
    public enum AnswerType { Multiple, Single}

    [SerializeField] private string info = string.Empty;
    public string Info { get { return info; } }

    [SerializeField] Answer3[] answers = null;
    public Answer3[] Answers {  get { return answers; } }

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
