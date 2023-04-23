using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/**
 * AnswerList is a script that handles the logic for showing and updating the selection of an answer choice on the UI.
 * It invokes the delegate UpdateQuestionAnswer delegate from GameEvents to tell it if the answer choice has changed.
 * 
 * The SwitchState method toggles the Checked bool by calling the UpdateUI method, and updates the UpdateQuestionAnswer delegate to tell it this is the answer I'm choosing if checked.
 * The UpdateData method updates the answer option text and the index of the answer.
 * The UpdateUI method swaps between the checked and unchecked status.
 * The Reset method resets the checked status to false and calls the UpdateUI method to set the unchecked status to true and vice versa.
 * The RectTransform is used in creating and positioning of a single answer choice that will be replicated 3 times on the screen with the answer prefab.
 */
public class AnswerList : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI infoTextObject = null;
    [SerializeField] Image toggle = null;

    [Header("Textures")]
    [SerializeField] Sprite uncheckedToggle = null;
    [SerializeField] Sprite checkedToggle = null;

    [Header("References")]
    [SerializeField] GameEvents events = null;

    private RectTransform rect = null;

    public RectTransform Rect 
    {
        get
        {
            if(rect == null)
            {
                rect = GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
            }
            return rect;
        }
    }

    private int answerIndex = -1;
    public int AnswerIndex  {  get { return answerIndex; } }

    private bool Checked = false;

    public void UpdateData(string info, int index)
    {
        infoTextObject.text = info;
        answerIndex = index;
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

    void UpdateUI()
    {
        if (toggle == null) return;

        toggle.sprite = (Checked) ? checkedToggle : uncheckedToggle;
    }
}
