using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GeographyImageQnA
{
    public Sprite QuestionImage;
    public string QuestionText;
    public string[] Answers;
    public int CorrectAnswer;

    public GeographyImageQnA(Sprite questionImage, string questionText, string[] answers, int correctAnswer)
    {
        QuestionImage = questionImage;
        QuestionText = questionText;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
}



