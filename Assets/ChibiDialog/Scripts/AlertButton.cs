using UnityEngine;

 
    public class AlertButton : MonoBehaviour
    {

        public AlertPopup parent;
        public int index;

        public void OnClick()
        {
            parent.OnClickButton(index);
        }

    }

 