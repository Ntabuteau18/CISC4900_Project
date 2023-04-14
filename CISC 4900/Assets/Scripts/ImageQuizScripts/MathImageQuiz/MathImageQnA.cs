using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MathImageQnA
{
    public Sprite QuestionImage;
    public string QuestionText;
    public string[] Answers;
    public int CorrectAnswer;

    public MathImageQnA(Sprite questionImage, string questionText, string[] answers, int correctAnswer)
    {
        QuestionImage = questionImage;
        QuestionText = questionText;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
}



