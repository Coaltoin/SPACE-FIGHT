using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    public void DisableButton(bool Status, Button button)
    {
        if (Status == true)
        {
            if (button != null)
            {
                button.interactable = false;
            }
        }
        else
        {
            if (button != null)
            {
                button.interactable = true;
            }
        }
    }
}
