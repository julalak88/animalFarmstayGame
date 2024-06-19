using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

    public enum DialogState
    {
        Show,
        Hide
    }

    public class AlertPopup : MonoBehaviour
    {

        private readonly int kDialogSort = 10100;
        private float alpha;
        private float addValue;
        private bool needCloseByTapBG;
        private List<ActionButton> actionButtons;
        private Action closedAction;
        private int fontSizeTitle;
        private int fontSizeOther;

        public DialogState state
        {
            get;
            private set;
        }
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI messageText;
        public CanvasGroup canvasGroup;
        public GameObject title;
        public GameObject message;
        public AlertButton dialogButton;
        public GameObject ifChild;
        public GameObject btnChild;
        public Action action;

        // Start is called before the first frame update
        void Start()
        {
            state = DialogState.Hide;
            alpha = 0;
            addValue = 5f;
            enabled = false;
            actionButtons = new List<ActionButton>();

            var size = Mathf.Min(Screen.width, Screen.height);
            fontSizeTitle = Mathf.Max(size / 15, 21);
            fontSizeOther = Mathf.Max(size / 20, 14);
            titleText.fontSize = fontSizeTitle;
            messageText.fontSize = fontSizeOther;

            ToBack();

        }

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case DialogState.Show:
                    if (alpha < 1)
                    {
                        PlusAlpha(addValue);
                    }
                    break;
                case DialogState.Hide:
                    if (alpha > 0)
                    {
                        PlusAlpha(-addValue);
                        if (alpha < 0)
                        {
                            // 閉じた後
                            enabled = false;
                            ToBack();
                            DeleteButtons();
                            closedAction?.Invoke();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

    public Color okColor;
    public Color noColor;
    public void SetRaycasts(bool active) {
        canvasGroup.blocksRaycasts = active;
    }

        public void ShowDialog(string txtTitle, string txtMessage, ActionButton[] acts = null, Action actClosed = null, bool needCloseByTapBG = false)
        {
           
            ToFront();
            
            enabled = true;
             
            titleText.text = txtTitle;
            messageText.text = txtMessage;
             
            title.SetActive(txtTitle != null);
            message.SetActive(txtMessage != null);
           
            closedAction = actClosed;
           
            this.needCloseByTapBG = needCloseByTapBG;
           
            state = DialogState.Show;
            
            actionButtons.Clear();
           
            DeleteButtons();
            int idx = 0;
       
            foreach (var actButton in acts)
            {
               
                var btn = Instantiate(dialogButton);
             
                btn.transform.SetParent(btnChild.transform);
                
                btn.parent = this;
                
                btn.transform.localScale = new Vector3(1, 1, 1);
               
                btn.name = "btn_" + idx;
                btn.index = idx;
                idx += 1;
               
                var buttonText = btn.transform.Find("VLayout/Text").GetComponent<TextMeshProUGUI>();
                buttonText.text = actButton.text;
             
                buttonText.fontSize = fontSizeOther;
                if (buttonText.text.Contains("ยกเลิก"))
                {
                btn.GetComponent<Image>().color = noColor;
                   // btn.GetComponent<Image>().color = actButton.color;
                } else
                {
                btn.GetComponent<Image>().color = okColor;
            }
                actionButtons.Add(actButton);
            }
        }

       
        private void PlusAlpha(float plus)
        {
            alpha += plus * Time.deltaTime;
            SetAlpha(alpha);
        }
      
        private void SetAlpha(float a)
        {
            var g = GetComponent<CanvasGroup>();
            g.alpha = a;
        }

        
        private void CloseDialog()
        {
            if (state == DialogState.Hide)
            {
                return;
            }
            alpha = 1f;
            state = DialogState.Hide;
        }

       
        public void OnClickButton(int idx)
        {
            ActionButton btn = actionButtons[idx];
            btn.action?.Invoke();
      
            CloseDialog();
        }

     
        public void OnClickBackground()
        {
            if (needCloseByTapBG)
            {
                CloseDialog();
            }
        }


       
        private void ToFront()
        {
            Sort(kDialogSort);
        }
        
        private void ToBack()
        {
            Sort(-kDialogSort);
        }
        private void Sort(int s)
        {
            var canvas = GetComponentInChildren<Canvas>();
            canvas.sortingOrder = s;
        }

      
        private void DeleteButtons()
        {
            GameObject child = btnChild;
            foreach (Transform btn in child.transform)
            {
                 
                Destroy(btn.gameObject);
            }

        }
     
      
        public class ActionButton
        {
            public string text;
            public Action action;
            public Color color;

            public ActionButton(string text, Action action = null, Color? color = null)
            {
                this.text = text;
                this.action = action;
                this.color = color ?? Color.white;
            }
        }

    }

 