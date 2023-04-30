using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ToFQuestionsH : ScriptableObject
{
    /**
    * TOFQuestions is how we are able to create the questions in unity
    * This is the questions setting that takes in the values of
    * question: The actual question
    * isTrue: Which answer is right
    * questioned: Have we answered this question already
    * We start at the current question 0 and add more from QuestionData
    * */

    [System.Serializable]
    public class TOFQuestionDataH
    {
        public string question = string.Empty;
        public bool isTrue = false;
        public bool questioned = false;
    }

    public int currentQuestion = 0;
    public List<TOFQuestionDataH> questionlist;

    public void AddQuestion()
    {
        questionlist.Add(new TOFQuestionDataH());
    }



}
