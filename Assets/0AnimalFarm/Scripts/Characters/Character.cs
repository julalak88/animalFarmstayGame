using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Sirenix.OdinInspector;
using UnityEngine.Experimental.GlobalIllumination;
using System.Diagnostics;
using DG.Tweening.Core.Easing;

public class Character : MonoBehaviour
{
    [MinMaxSlider(0.5f, 1f)]
    public Vector2 moveSpeed;
    public List<Sprite> walk;

    [HideInInspector]
    public PointTarget target;
    [HideInInspector]
    public SceneGame currentScene;
    [HideInInspector]
    public SceneGame requestScene;
    [HideInInspector]
    public int numActivity;

    SceneGame prevScene;
    internal CharacterMovement movement;
    internal SceneGameManager sceneManager;
    internal CustomerManager customerManager;
    internal UnlockManager unlock;
    [SerializeField]
    GameObject noti;
    [SerializeField]
    BoxCollider2D col2D;
    //Database database;
    internal SpriteRenderer sprite;
    internal float t_anim = 0;
    internal int ind_anim = 0;
    Tween tw;
    internal bool _reached = false;
    bool didActivity = false;
    float activityTime;

    public bool isGoingOut {
        get { return goOut; }
    }
    bool goOut = false;
    protected virtual void Awake() {
        movement = GetComponent<CharacterMovement>();
        movement.character = this;
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();

    }

    protected virtual void Start() {
        sceneManager = GameManager.Ins.sceneManager;
        unlock = GameManager.Ins.unlockManager;
        customerManager = GameManager.Ins.customerManager;

    }

    public void OnStart(bool firstCome) {
        if (sceneManager == null) sceneManager = GameManager.Ins.sceneManager;
        if (customerManager == null) customerManager = GameManager.Ins.customerManager;
        if (currentScene == null) return;
        if (firstCome) {
            if (noti != null) {
                noti.SetActive(true);
            }
            col2D.enabled = true;
            target = currentScene.RandomPointTarget(PointTarget.PointTargetType.MAIN);
        } else {
            col2D.enabled = false;
            target = currentScene.RandomPointTarget(PointTarget.PointTargetType.ENTER);
        }
        prevScene = currentScene;
        transform.position = target.transform.position;
        numActivity = 3;
    }

    public void GotoBusStop()
    {
        requestScene = null;
        target = currentScene.RandomPointTarget(PointTarget.PointTargetType.BUS);
        prevScene = currentScene;
        GoToTarget();
        Invoke("TimeForBusStop", 15f);
    }

    public virtual void OnUpdate() {

        if (goOut && !sprite.gameObject.activeInHierarchy) return;

        t_anim += Time.deltaTime;
        if (t_anim > .3f) {
            t_anim = 0;
            sprite.sprite = walk[(++ind_anim) % walk.Count];
            if (!_reached) {
                int dir = movement.GetDirection();
                if (dir == -1 || dir == 1) sprite.transform.localScale = new Vector3(dir, 1);
            }
        }

        movement.OnUpdate();

    }

    public void TabToTalk() {
        if (customerManager.touchToTalk == true) {
          
            customerManager.touchToTalk = false;
            noti.SetActive(false);
            col2D.enabled = false;
            if (target != null) {
                target.inQueue = false;
            }

            unlock.TalkToGuest(gameObject.name, this);
        
        }
    }

    public bool isTimeOut = false;
    void TimeForBusStop()
    {
        isTimeOut = true;
        RandomRequest();
        CancelInvoke("TimeForBusStop");
    }
 
    public void RandomRequest() {
        //print("---------------------- random request -----------------------------");
        if (tw != null) tw.Kill(); tw = null;
        float rnd = UnityEngine.Random.Range(0f, 1f);

        if (currentScene.name == "Home") {
            if (rnd > .7f && prevScene == currentScene)
            { // random some link point

                requestScene = null;
                target = currentScene.RandomPointTarget(PointTarget.PointTargetType.LINK);

            } else if (rnd > .2f && currentScene.isActivityAvailable)
            { // random some activity in current scene
                requestScene = null;
                target = currentScene.RandomPointTarget(PointTarget.PointTargetType.ACTIVITY);

            } else
            { // random some scene
                requestScene = sceneManager.RandomAvailableScene();
                target = currentScene.RandomPointTarget(PointTarget.PointTargetType.ENTER);

            }

        } else if (currentScene.name == "Riverside" || currentScene.name == "Farm") {
                if (currentScene.isActivityAvailable) { // random some activity in current scene

                    //print(">>>>>>> random some activity in current scene");
                    requestScene = null;
                    target = currentScene.RandomPointTarget(PointTarget.PointTargetType.ACTIVITY);
                    if (didActivity) {
                        currentScene.ReturnActivitySlot(target);
                        target = currentScene.RandomPointTarget(PointTarget.PointTargetType.LINK);
                    }

                } else if (!didActivity && currentScene.isQueueAvailable) currentScene.AddCustomerToQueue(this);

                else if (rnd > .5f && prevScene == currentScene) { // random some link point

                    //print(">>>>>>> random some link point");
                    requestScene = null;
                    target = currentScene.RandomPointTarget(PointTarget.PointTargetType.LINK);

                } else { // random some scene

                    //print(">>>>>>> random some new scene");
                    requestScene = sceneManager.RandomAvailableScene();
                    target = currentScene.RandomPointTarget(PointTarget.PointTargetType.ENTER);

                }
            
        } else {

            if (rnd > .4f && currentScene.isActivityAvailable) { // random some activity in current scene

                //print(">>>>>>> random some activity in current scene");
                requestScene = null;
                target = currentScene.RandomPointTarget(PointTarget.PointTargetType.ACTIVITY);
                if (target.activity == NameList.Activities.Eat && didActivity) {
                    currentScene.ReturnActivitySlot(target);
                    target = currentScene.RandomPointTarget(PointTarget.PointTargetType.LINK);
                }

                if (target.activity == NameList.Activities.Rest && target.guesthouse.isClean==false)
                {
                    currentScene.ReturnActivitySlot(target);
                    target = currentScene.RandomPointTarget(PointTarget.PointTargetType.LINK);
                }

            } else if (rnd > .25f && prevScene == currentScene) { // random some link point

                //print(">>>>>>> random some link point");
                requestScene = null;
                target = currentScene.RandomPointTarget(PointTarget.PointTargetType.LINK);

            } else { // random some scene

                //print(">>>>>>> random some new scene");
                requestScene = sceneManager.RandomAvailableScene();
                target = currentScene.RandomPointTarget(PointTarget.PointTargetType.ENTER);

            }
        }

        prevScene = currentScene;
       
        GoToTarget();
    }

    public void RandomActivityAfterQueue() {
        requestScene = null;
        target = currentScene.RandomPointTarget(PointTarget.PointTargetType.ACTIVITY);
        GoToTarget();
    }

    public void GoToTarget() {
        //if (requestScene == null) return;
        movement.speed = UnityEngine.Random.Range(moveSpeed.x, moveSpeed.y);
        _reached = false;
        movement.destination = target.transform.position;
        movement.SearchPath();
        movement.canMove = true;
       
    }

    public virtual void OnReached() {
        if (target == null) {
            RandomRequest();
            return;
        }
        _reached = true;
        didActivity = false;
        if (target.type == PointTarget.PointTargetType.MAIN) { // out

            movement.canMove = false;
            //movement.ClearPath();
            goOut = true;

        } else if (target.type == PointTarget.PointTargetType.ACTIVITY) {

            sprite.transform.localScale = new Vector3(target.direction * -1, 1);

            if (target.aUpgrade && target.aUpgrade.isAvailable) {

                activityTime = target.aUpgrade.time; //database.activity.list[target.activity].time;
                //print("------------ activity : " + target.activity.ToString() + " = " + activityTime);
                didActivity = true;
                if (target.activity == NameList.Activities.Eat) {
                   
                       
                        TableFood table = target.aUpgrade as TableFood;
                        table.customer = this;
                   // sceneManager.foodManager.RandomFood(table);
                    if (sceneManager.foodManager.HaveFood())
                    {
                        sceneManager.foodManager.RandomFood(table);
                    } else
                    {
                        
                        unlock.AddRecord(table.foodName);
                        table.ClearFood();
                        RandomRequest();
                    }
                    
                } else if (target.activity == NameList.Activities.Rest) {
                    sprite.gameObject.SetActive(false);
                    tw = DOVirtual.DelayedCall(activityTime, () => ActivityComplete(true));
                } else if (((target.aUpgrade is Livestock) && ((Livestock)target.aUpgrade).data.numProduct == 0) || ((target.aUpgrade is Plot) && ((Plot)target.aUpgrade).index != 2)) {
                    tw = DOVirtual.DelayedCall(activityTime, () => ActivityComplete(false));
                } else {
                   
                    tw = DOVirtual.DelayedCall(activityTime, () => ActivityComplete(true));
                }
            } else {
                tw = DOVirtual.DelayedCall(1, () => ActivityComplete(false));
            }

        } else if (target.type == PointTarget.PointTargetType.LINK) {

            if (target.linkPoint.Count > 0) {

                int rnd = UnityEngine.Random.Range(0, target.linkPoint.Count);
                target = target.linkPoint[rnd];
                prevScene = currentScene;
                if (sceneManager.IsSceneUnlocked(target.parentScene.name)) {
                    //currentScene = target.parentScene;
                } else {
                    //currentScene = sceneManager.RandomAvailableScene();
                    requestScene = null;
                    target = sceneManager.RandomAvailableScene().RandomPointTarget(PointTarget.PointTargetType.ENTER);
                }
                movement.canMove = false;
                Vector3 jumpPosition = target.transform.position;
                sprite.DOFade(0, .3f).OnComplete(() => OnFadeOutComplete(jumpPosition));

            } else RandomRequest();

        } else if (target.type == PointTarget.PointTargetType.QUEUE) {

            sprite.transform.localScale = new Vector3(target.direction * -1, 1);

        } else if (requestScene != null) {

            prevScene = currentScene;
            target = requestScene.RandomPointTarget(PointTarget.PointTargetType.ENTER);
            requestScene = null;
            movement.canMove = false;
            Vector3 jumpPosition = target.transform.position;
            sprite.DOFade(0, .3f).OnComplete(() => OnFadeOutComplete(jumpPosition));

        }
    }

    public void ActivityComplete(bool success = true) {
        
        bool endActivity = true;
        if (success) {
         
            if (target.activity == NameList.Activities.Rest) {
                unlock.AddRecord(target.aUpgrade.objectName);
                sprite.gameObject.SetActive(true);
                target.guesthouse.isClean = false;
            } else if (target.activity == NameList.Activities.Eat) {
                TableFood table = ((TableFood)target.aUpgrade);
                unlock.AddRecord(table.foodName);
                table.ClearFood();
            } 
            else if (target.activity == NameList.Activities.Milk) endActivity = ((Livestock)target.aUpgrade).Gather(this, "milk");
            else if (target.activity == NameList.Activities.Egg) endActivity = ((Livestock)target.aUpgrade).Gather(this, "egg");
            else if (target.activity == NameList.Activities.Watch) unlock.AddRecord(target.aUpgrade.objectName);


            if (endActivity) {
                CalculateCoinDrop(target.aUpgrade.coin, 0);
            }
        }

        if (endActivity) {
            currentScene.ReturnActivitySlot(target);
            currentScene.CheckQueue();
 
            if (--numActivity <= 0)
            {

                if (tw != null) tw.Kill(); tw = null;
                if (currentScene.name == "Home")
                {
                    requestScene = null;
                    target = currentScene.RandomPointTarget(PointTarget.PointTargetType.MAIN);
                } else
                {

                    requestScene = sceneManager.ToHomeScene();
                    target = currentScene.RandomPointTarget(PointTarget.PointTargetType.ENTER);
                    prevScene = currentScene;
                }
                GoToTarget();

            } else
            {
                

                RandomRequest();
            }
        }
    }

    public void CalculateCoinDrop(int _coin, float exp) {
        /*int length = 0, value2 = 1, value3 = 0;
        if (value > 100) value2 = 100;
        else if (value > 50) value2 = 50;
        else if (value > 10) value2 = 10;
        else if (value >= 5) value2 = 5;
        length = Mathf.FloorToInt(value / value2);
        value3 = value % value2;
        for (int i = 0; i < length; i++) sceneManager.CreateCoinDrop(transform.position, target.GetCoinPosition).value = value2;
        if (value3 != 0) sceneManager.CreateCoinDrop(transform.position, target.GetCoinPosition).value = value3;*/
        /*int value2 = value;
        if (value > 100) value2 = 100;
        else if (value > 50) value2 = 50;
        else if (value > 10) value2 = 10;
        else if (value >= 5) value2 = 5;
        sceneManager.CreateCoinDrop(transform.position, target.GetCoinPosition).value = value2;
        if(value2 != value) sceneManager.CreateCoinDrop(transform.position, target.GetCoinPosition).value = value - value2;*/
        Coin coin = sceneManager.CreateCoinDrop(transform.position, target.GetCoinPosition);
        coin.isCoin = true;
        coin.value = _coin;
        if (exp > 0) {
            coin = sceneManager.CreateCoinDrop(transform.position, target.GetCoinPosition);
            coin.isCoin = false;
            coin.value = Convert.ToInt32(exp);
        }

        


    }

    void GoOut() {
        requestScene = null;
        target = currentScene.RandomPointTarget(PointTarget.PointTargetType.MAIN);
        movement.speed = 1.3f;
        GoToTarget();
    }

    private void OnFadeOutComplete(Vector3 jumpPosition) {
        movement.ClearPath();
        currentScene = target.parentScene;
        transform.position = jumpPosition;
        //if (currentScene.name == "Riverside") transform.localScale = Vector3.one * 1.2f;
        //else transform.localScale = Vector3.one;
        if (currentScene.name == "Home" && numActivity <= 0)
            sprite.DOFade(1, .3f).OnComplete(GoOut);
        else
            sprite.DOFade(1, .3f).OnComplete(RandomRequest);
    }

    public void Eating() {
        tw = DOVirtual.DelayedCall(activityTime, () => ActivityComplete(true));
    }
}
