using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SceneGame : MonoBehaviour
{
    //SceneGameManager sceneManager;
    //public List<SceneGame> nearScenes;

    public bool special = false;
    public List<PointTarget> queuePoint;

    PointTarget[] points;

    List<PointTarget> enterPoints = new List<PointTarget>();
    List<PointTarget> linkPoints = new List<PointTarget>();
    [SerializeField]
    List<PointTarget> activityPoints = new List<PointTarget>();
    List<PointTarget> mainPoints = new List<PointTarget>();
    List<PointTarget> busPoints = new List<PointTarget>();
    List<Character> queue = new List<Character>();

    internal ObjectUpgrade[] objUpgrades;

    internal SaveManager save;
    internal UnlockManager unlock;
    internal Transform upgradeTransform;
    bool _update = false;
    float t = 0f;
    int n = 0;

    protected virtual void Awake() {
        //sceneManager = GetComponentInParent<SceneGameManager>();
        points = transform.Find("Points").GetComponentsInChildren<PointTarget>();
        for (int i = 0; i < points.Length; i++) {
            points[i].parentScene = this;
            if (points[i].type == PointTarget.PointTargetType.ENTER) enterPoints.Add(points[i]);
            else if (points[i].type == PointTarget.PointTargetType.LINK) linkPoints.Add(points[i]);
            else if (points[i].type == PointTarget.PointTargetType.ACTIVITY) activityPoints.Add(points[i]);
            else if (points[i].type == PointTarget.PointTargetType.MAIN) mainPoints.Add(points[i]);
            else if (points[i].type == PointTarget.PointTargetType.BUS) busPoints.Add(points[i]);
        }
        _update = (queuePoint.Count > 0);

        upgradeTransform = transform.Find("Upgrade");
        objUpgrades = upgradeTransform.GetComponentsInChildren<ObjectUpgrade>();

        n = UnityEngine.Random.Range(0, 4);
    }

    protected virtual void Start() {
        save = SaveManager.Ins;
        unlock = GameManager.Ins.unlockManager;

        if (save.data.upgrade.ContainsKey(gameObject.name)) {
            Dictionary<string, List<string>> objects = save.data.upgrade[gameObject.name];
            for (int i = 0; i < objUpgrades.Length; i++) {
                if (objects.ContainsKey(objUpgrades[i].name)) objUpgrades[i].LoadObject(gameObject.name, objects[objUpgrades[i].name][0]);
                else objUpgrades[i].Locked();
            }
        }
    }

    public virtual void UpdateObjUpgrades(List<string> _name) {

      
            if (save.data.upgrade.ContainsKey(gameObject.name))
            {
                Dictionary<string, List<string>> objects3 = save.data.upgrade[gameObject.name];
                for (int i = 0; i < objUpgrades.Length; i++)
                {
                    if (objects3.ContainsKey(objUpgrades[i].name))
                    {
                        objUpgrades[i].LoadObject(gameObject.name, _name[i]);
                    }

                }
            }
        
    }



    public virtual void OnSceneActive() {
        if(++n >= 5) {
            n = 0;
            unlock.CheckForNewCustomer();
        }
    }

    public virtual void OnSceneInactive() {
     
    }

    public virtual void UpdateScene() {
        if (!_update) return;
        t += Time.deltaTime;
        if(t > 3f) {
            t = 0f;
            for (int i = 0; i < queue.Count; i++) {
                if(queue[i].target != queuePoint[i]) { // customer move to next queue
                    queue[i].target = queuePoint[i];
                    queue[i].GoToTarget();
                }
            }
        }
    }
    int countBus = 0;
    public PointTarget RandomPointTarget(PointTarget.PointTargetType type) {
        PointTarget target = null;
        if (type == PointTarget.PointTargetType.ENTER)
        {
            target = enterPoints[UnityEngine.Random.Range(0, enterPoints.Count)];
        } else if (type == PointTarget.PointTargetType.LINK)
        {
            target = linkPoints[UnityEngine.Random.Range(0, linkPoints.Count)];
        } else if (type == PointTarget.PointTargetType.ACTIVITY && activityPoints.Count > 0)
        {
            
            target = activityPoints[UnityEngine.Random.Range(0, activityPoints.Count)];
            activityPoints.Remove(target);
           
        } else if (type == PointTarget.PointTargetType.MAIN)
        {
            target = mainPoints[UnityEngine.Random.Range(0, mainPoints.Count)];
        }else if (type == PointTarget.PointTargetType.BUS)
        {
            int QueueIndex = 0;
            if (!busPoints[0].inQueue)
            {
                QueueIndex = 0;
            } else if (!busPoints[1].inQueue)
            {
                QueueIndex = 1;
            } else if (!busPoints[2].inQueue)
            {
                QueueIndex = 2;
            }
            target = busPoints[QueueIndex];
            target.inQueue = true;

            if (countBus < busPoints.Count)
            {
                countBus++;
              
            } else
            {
                countBus = 0;
            }
            
          

        } else
        {
            target = points[UnityEngine.Random.Range(0, points.Length)];
        }
        return target;
    }

   
    public void ReturnActivitySlot(PointTarget target) {
        if(!activityPoints.Contains(target))
            activityPoints.Add(target);
    }

    public bool isActivityAvailable {
        get { return (activityPoints.Count > 0); }
    }

    public bool isQueueAvailable {
        get { return (queuePoint.Count > 0 && queue.Count < queuePoint.Count); }
    }

    public void AddCustomerToQueue(Character customer) {
        customer.target = queuePoint[queue.Count];
        queue.Add(customer);
    }

    public void CheckQueue() {
        if (queue.Count > 0) {
            Character customer = queue[0];
            queue.RemoveAt(0);
            customer.RandomActivityAfterQueue();
        }
    }

    public void ShowUpgradeIcon(bool val) {

        for (int i = 0; i < objUpgrades.Length; i++) {
            /*if(objUpgrades[i].isAvailable)*/ objUpgrades[i].showUpgradeIcon = val;
        }
    }
}
