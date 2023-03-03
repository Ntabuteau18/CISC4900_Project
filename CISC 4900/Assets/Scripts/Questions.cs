using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Answer
{
    [SerializeField] private string info;
    public string Info { get {return info; } }

    [SerializeField] private bool isCorrect;
    public bool IsCorrect { get {return isCorrect; } }
}
[CreateAssetMenu(fileName = "Questions", menuName = "Quiz/new Questions")]
public class Questions : ScriptableObject{

    public enum AnswerType{Multiple, Single};

    [SerializeField] private string info = string.Empty;
    public string Info{ get {return info; } }

    // Answers array
    [SerializeField] Answer[] answers= null;
    public Answer[] Answers{ get {return answers; } }

    // Parameters
    [SerializeField] private bool useTimer = false;
    public bool UseTimer{ get {return useTimer; } }

    [SerializeField] private int timer = 0;
    public int Timer{ get {return timer; } }

    [SerializeField] private AnswerType ansType = AnswerType.Multiple;
    public AnswerType GetAnswerType{ get {return ansType; } } 

    [SerializeField] private int plusScore = 10;
    public int PlusScore{ get {return plusScore; } }

    public List <int> GetCorrectAns()
    {
         List <int> CorrectAnswers = new List<int>();
         for(int i =0; i< Answers.Length;i++)
         {
            if(Answers[i].IsCorrect)
            {
              CorrectAnswers.Add(i);  
            }
         }
         return CorrectAnswers;
    }
}
