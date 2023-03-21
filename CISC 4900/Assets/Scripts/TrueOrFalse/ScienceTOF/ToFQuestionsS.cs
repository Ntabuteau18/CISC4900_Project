using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ToFQuestionsS : ScriptableObject
{
    [System.Serializable]
    public class TOFQuestionDataS
    {
        public string question = string.Empty;
        public bool isTrue = false;
        public bool questioned = false;
    }

    public int currentQuestion = 0;
    public List<TOFQuestionDataS> questionlist;

    public void AddQuestion()
    {
        questionlist.Add(new TOFQuestionDataS());
    }



}
