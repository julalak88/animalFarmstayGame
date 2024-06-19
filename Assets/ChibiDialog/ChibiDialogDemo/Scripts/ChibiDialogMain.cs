using UnityEngine;

public class ChibiDialogMain : MonoBehaviour
{

    public AlertPopup dialog;

    public void OnClickButton()
    {
        var cancel = new AlertPopup.ActionButton("Cancel", null, new Color(0.9f, 0.9f, 0.9f));
        var ok = new  AlertPopup.ActionButton("OK", () =>
        { 
            Debug.Log("click ok");
        }, new Color(0f, 0.9f, 0.9f));
        AlertPopup.ActionButton[] buttons = { cancel, ok };
        dialog.ShowDialog("★Chibi Dialog★", "It's easy!", buttons);
    }

    public void OnClickButton2()
    {
        //  var cancel = new AlertPopup.ActionButton("Cancel", null, new Color(0.9f, 0.9f, 0.9f));
        var ok = new AlertPopup.ActionButton("OK", () =>
        {
            Debug.Log("click ok");
        }, new Color(0f, 0.9f, 0.9f));
        AlertPopup.ActionButton[] buttons = { ok };
        dialog.ShowDialog("★Chibi Dialog★", "It's easy!", buttons, () =>
        {
            Debug.Log("closed dialog.");
        }, true);
    }
}

