using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class VegRecipe : MonoBehaviour
{
    public Image vegImg;
    public TextMeshProUGUI totalText;
    

    public void Show(Sprite _sp,int _val)
    {
        vegImg.sprite = _sp;
        vegImg.SetNativeSize();

        totalText.text = "X " + _val.ToString();
    }
    
}
