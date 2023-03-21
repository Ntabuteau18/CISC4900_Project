using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ToFQuestionsH : ScriptableObject
{
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
