using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FoodManager : MonoBehaviour
{

     
    List<string> bought_food;

    private void Start() {
        
        bought_food = SaveManager.Ins.data.bought_food;
    }

    public void RandomFood(TableFood table) {
        if (bought_food.Count < 1) return;
        int rnd = UnityEngine.Random.Range(0, bought_food.Count);
        string foodName = bought_food[rnd];
        FoodDatabase food = GameManager.Ins.database.foods.Where(x => x.id.Equals(foodName)).Select(x =>x).FirstOrDefault() ;
        table.SetOrder(foodName, food.image, food.cookTime);
    }

    public bool HaveFood()
    {
        bool isHave = false;
        if(bought_food.Count > 0)
        {
            isHave = true;
        }
        return isHave;
    } 
}
