using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ToFQuestions : ScriptableObject
{
    [System.Serializable]
    public class TOFQuestionData
    {
        public string question = string.Empty;
        public bool isTrue = false;
        public bool questioned = false;
    }

    public int currentQuestion = 0;
    public List<TOFQuestionData> questionlist;

    public void AddQuestion()
    {
        questionlist.Add(new TOFQuestionData());
    }



}
