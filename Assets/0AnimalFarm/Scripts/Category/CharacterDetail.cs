using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CharacterDetail : MonoBehaviour
{
    [HideInInspector]
    public CategoryCharacter categoryCharacter;
    [HideInInspector]
    public CustomerDatabase customerDatabase;
    public LanguageSelector charName;
    public LanguageSelector charDes;
    public Image image;
    public Image imageLock;
    public Image fadeBg;
    public Button closeBtn;
    public GameObject box;
    public float time;
    public float timeFade;
    public Ease scaleIn = Ease.Flash;
    public Ease scaleOut = Ease.Flash;
    GameManager gm;
    
    
    private void Awake() {
        gm = GameManager.Ins;
      
    }
    private void OnEnable() {
      
       
        fadeBg.DOFade(0, 0.1f);
        fadeBg.DOFade(0.4f, timeFade);
        
        
    }
    public void ShowDetail(CustomerDatabase customerDatabase,bool isUnlock) {
        this.customerDatabase = customerDatabase;
        DescriptionData desc_lang = customerDatabase.GetType().GetField(categoryCharacter.save.lang).GetValue(customerDatabase) as DescriptionData;
 
        image.sprite = customerDatabase.image;

        if (isUnlock) {
            charName.Text = desc_lang.name;
            charDes.Text = desc_lang.desc;
            image.gameObject.SetActive(true);
            image.sprite = customerDatabase.image;
        } else {
            charName.Text = SetText();
            charDes.Text = SetText();
            imageLock.gameObject.SetActive(true);
            imageLock.sprite = customerDatabase.image;
        }

      
    }
    string SetText() {
        return "???";
    }

  
   
}
