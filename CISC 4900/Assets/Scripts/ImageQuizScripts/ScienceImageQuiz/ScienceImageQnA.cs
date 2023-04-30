using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ScienceImageQnA
{
    /**
     * ImageQnA is the tools necessary to create the question text, image, and answers in unity.
     * This script is responsible for allowing us to directly create these assets in Unity, by setting the image and text together along with
     * its corresponding 4 potential answers
     * 
     * */
    public Sprite QuestionImage;
    public string QuestionText;
    public string[] Answers;
    public int CorrectAnswer;

    public ScienceImageQnA(Sprite questionImage, string questionText, string[] answers, int correctAnswer)
    {
        QuestionImage = questionImage;
        QuestionText = questionText;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
}



