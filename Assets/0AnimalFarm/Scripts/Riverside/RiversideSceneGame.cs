using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiversideSceneGame : SceneGame
{
    GameManager gameManager;
    GameObject foodMenu;

    protected override void Awake() {
        base.Awake();
    }
    protected override void Start() {
        base.Start();
        gameManager = GameManager.Ins;
        foodMenu = gameManager.uiManager.menu.foodMenu;
    }
    public override void OnSceneActive() {
        base.OnSceneActive();
        if(foodMenu == null) foodMenu = gameManager.uiManager.menu.foodMenu;

        foodMenu.SetActive(true);
    }

    public override void OnSceneInactive() {
        base.OnSceneInactive();
        foodMenu.SetActive(false);
    }

}