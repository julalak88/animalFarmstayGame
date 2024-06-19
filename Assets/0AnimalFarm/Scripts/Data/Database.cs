using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using I2.Loc;
using UnityEditor;

public class Database : SerializedMonoBehaviour
{

    public Dictionary<string, CustomerDatabase> customerDescription;
    public Dictionary<string, CustomerDatabase> kids;
    public Dictionary<string, Dictionary<string, ItemDatabase>> items;
    public Dictionary<string, LocationDatabase> Locations;
    public List<PostcardDatabase> postcards;
    public List<VegDatabase> vegetables;
    public List<FoodDatabase> foods;
  
    [HideInInspector]
    public ItemList itemList;
    [HideInInspector]
    public CharacterList characterList;

    private void Awake() {
        itemList = GetComponent<ItemList>();
        characterList = GetComponent<CharacterList>();
        
    }

    [Button("Apply Languages")]
    void ApplyLanguage() {
        foreach (KeyValuePair<string, CustomerDatabase> o in customerDescription) {
            o.Value.TH.name = LocalizationManager.GetTranslation("Customer/" + o.Key + "_name", true, 0, true, false, null, "TH");
            
            o.Value.TH.desc = LocalizationManager.GetTranslation("Customer/" + o.Key + "_desc", true, 0, true, false, null, "TH");
            o.Value.dialogTH.firstmeet = LocalizationManager.GetTranslation("Customer/" + o.Key + "_firstmeet", true, 0, true, false, null, "TH");
            o.Value.dialogTH.hint = LocalizationManager.GetTranslation("Customer/" + o.Key + "_hint", true, 0, true, false, null, "TH");
            o.Value.dialogUS.firstmeet = LocalizationManager.GetTranslation("Customer/" + o.Key + "_firstmeet", true, 0, true, false, null, "TH");
            o.Value.dialogUS.hint = LocalizationManager.GetTranslation("Customer/" + o.Key + "_hint", true, 0, true, false, null, "TH");
             
          
            o.Value.StoryTH.story = LocalizationManager.GetTranslation("Customer/" + o.Key + "_story", true, 0, true, false, null, "TH");
            o.Value.US.name = LocalizationManager.GetTranslation("Customer/" + o.Key + "_name", true, 0, true, false, null, "US");
            o.Value.US.desc = LocalizationManager.GetTranslation("Customer/" + o.Key + "_desc", true, 0, true, false, null, "US");
            o.Value.StoryUS.story = LocalizationManager.GetTranslation("Customer/" + o.Key + "_story", true, 0, true, false, null, "US");
        }
        //AssetDatabase.SaveAssets();

    }

    [Button("Apply Languages Items")]
    void ApplyLanguageItems() {
        foreach (KeyValuePair<string, ItemDatabase> o in items["Farm"]) {
            o.Value.TH.name = LocalizationManager.GetTranslation("Items/" + o.Key + "_name", true, 0, true, false, null, "TH");
            o.Value.TH.desc = LocalizationManager.GetTranslation("Items/" + o.Key + "_desc", true, 0, true, false, null, "TH");
            o.Value.US.name = LocalizationManager.GetTranslation("Items/" + o.Key + "_name", true, 0, true, false, null, "US");
            o.Value.US.desc = LocalizationManager.GetTranslation("Items/" + o.Key + "_desc", true, 0, true, false, null, "US");
           
        }
        foreach (KeyValuePair<string, ItemDatabase> o in items["Global"]) {
            o.Value.TH.name = LocalizationManager.GetTranslation("Items/" + o.Key + "_name", true, 0, true, false, null, "TH");
            o.Value.TH.desc = LocalizationManager.GetTranslation("Items/" + o.Key + "_desc", true, 0, true, false, null, "TH");
            o.Value.US.name = LocalizationManager.GetTranslation("Items/" + o.Key + "_name", true, 0, true, false, null, "US");
            o.Value.US.desc = LocalizationManager.GetTranslation("Items/" + o.Key + "_desc", true, 0, true, false, null, "US");

        }
        foreach (KeyValuePair<string, ItemDatabase> o in items["Home"]) {
            o.Value.TH.name = LocalizationManager.GetTranslation("Items/" + o.Key + "_name", true, 0, true, false, null, "TH");
            o.Value.TH.desc = LocalizationManager.GetTranslation("Items/" + o.Key + "_desc", true, 0, true, false, null, "TH");
            o.Value.US.name = LocalizationManager.GetTranslation("Items/" + o.Key + "_name", true, 0, true, false, null, "US");
            o.Value.US.desc = LocalizationManager.GetTranslation("Items/" + o.Key + "_desc", true, 0, true, false, null, "US");

        }
        foreach (KeyValuePair<string, ItemDatabase> o in items["Resort"]) {
            o.Value.TH.name = LocalizationManager.GetTranslation("Items/" + o.Key + "_name", true, 0, true, false, null, "TH");
            o.Value.TH.desc = LocalizationManager.GetTranslation("Items/" + o.Key + "_desc", true, 0, true, false, null, "TH");
            o.Value.US.name = LocalizationManager.GetTranslation("Items/" + o.Key + "_name", true, 0, true, false, null, "US");
            o.Value.US.desc = LocalizationManager.GetTranslation("Items/" + o.Key + "_desc", true, 0, true, false, null, "US");

        }
        foreach (KeyValuePair<string, ItemDatabase> o in items["Riverside"]) {
            o.Value.TH.name = LocalizationManager.GetTranslation("Items/" + o.Key + "_name", true, 0, true, false, null, "TH");
            o.Value.TH.desc = LocalizationManager.GetTranslation("Items/" + o.Key + "_desc", true, 0, true, false, null, "TH");
            o.Value.US.name = LocalizationManager.GetTranslation("Items/" + o.Key + "_name", true, 0, true, false, null, "US");
            o.Value.US.desc = LocalizationManager.GetTranslation("Items/" + o.Key + "_desc", true, 0, true, false, null, "US");

        }


      //  AssetDatabase.SaveAssets();
    }

    [Button("Apply Languages Postcard")]
    void ApplyLanguagesPostcard()
    {
        foreach (PostcardDatabase o in postcards)
        {
            o.TH.name = LocalizationManager.GetTranslation("Postcard/" + o.id + "_name", true, 0, true, false, null, "TH");
             
            o.TH.desc = LocalizationManager.GetTranslation("Postcard/" + o.id + "_desc", true, 0, true, false, null, "TH");
            o.US.name = LocalizationManager.GetTranslation("Postcard/" + o.id + "_name", true, 0, true, false, null, "US");
            o.US.desc = LocalizationManager.GetTranslation("Postcard/" + o.id + "_desc", true, 0, true, false, null, "US");
           
        }

      //  AssetDatabase.SaveAssets();
    }

    [Button("Apply Languages Food")]
    void ApplyLanguagesFood()
    {
        foreach (FoodDatabase o in foods)
        {
            o.TH.name = LocalizationManager.GetTranslation("Food/" + o.id + "_name", true, 0, true, false, null, "TH");

            o.TH.desc = LocalizationManager.GetTranslation("Food/" + o.id + "_desc", true, 0, true, false, null, "TH");
            o.US.name = LocalizationManager.GetTranslation("Food/" + o.id + "_name", true, 0, true, false, null, "US");
            o.US.desc = LocalizationManager.GetTranslation("Food/" + o.id + "_desc", true, 0, true, false, null, "US");

        }


    }



}


