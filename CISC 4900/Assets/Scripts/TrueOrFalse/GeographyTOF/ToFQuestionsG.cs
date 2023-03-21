using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ToFQuestionsG : ScriptableObject
{
    [System.Serializable]
    public class TOFQuestionDataG
    {
        public string question = string.Empty;
        public bool isTrue = false;
        public bool questioned = false;
    }

    public int currentQuestion = 0;
    public List<TOFQuestionDataG> questionlist;

    public void AddQuestion()
    {
        questionlist.Add(new TOFQuestionDataG());
    }



}
