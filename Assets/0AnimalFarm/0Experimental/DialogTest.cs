using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    public DialogManager dialog;
    public Database database;
    public string customerName = "";
    public enum Lang { TH, US };
    public Lang lang;

    bool start = false;

    void Update()
    {
        if (start) return;
        if(Input.GetKeyDown(KeyCode.Space)) {
            start = true;
            dialog.OnDialogComplete = OnDialogComplete;
            CustomerDatabase data = database.customerDescription[customerName];
            StoryData story = data.GetType().GetField("Story"+lang).GetValue(data) as StoryData;
            dialog.AddText(story.story);
            dialog.ShowDialog(data.image);
        }
    }

    private void OnDialogComplete() {
        start = false;
    }
}
