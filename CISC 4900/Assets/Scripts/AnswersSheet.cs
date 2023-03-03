using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswersSheet : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI infoTextObject;
    [SerializeField] Image toggle;


    [Header("Textures")]
    [SerializeField] Sprite uncheckedToggle;
    [SerializeField] Sprite checkedToggle;

    [Header("References")]
    [SerializeField] GameEvents events;

    private RectTransform rect;
    public RectTransform Rect { 
        get { 
            if(rect ==null)
            {
                rect = GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
            }
            return rect;
        } 
    }

    private int ansIndex = -1;
    public int AnsIndex { get { return ansIndex; } }

    private bool Checked = false;

    public void UpdateData(string info, int index)
    {
        infoTextObject.text = info;
        ansIndex = index;
    }
    public void Reset()
    {
      Checked = false;
        UpdateUI();
    }
    public void SwitchState()
    {
        Checked = !Checked;
        UpdateUI();

        if(events.UpdateQuestionAnswer != null)
        {
            events.UpdateQuestionAnswer(this);
        }
    }
    public void UpdateUI()
    {
        toggle.sprite = (Checked) ? checkedToggle : uncheckedToggle;

    }

}