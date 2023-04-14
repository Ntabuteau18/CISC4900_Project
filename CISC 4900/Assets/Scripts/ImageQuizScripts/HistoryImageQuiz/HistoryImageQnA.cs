using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HistoryImageQnA
{
    public Sprite QuestionImage;
    public string QuestionText;
    public string[] Answers;
    public int CorrectAnswer;

    public HistoryImageQnA(Sprite questionImage, string questionText, string[] answers, int correctAnswer)
    {
        QuestionImage = questionImage;
        QuestionText = questionText;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
}



