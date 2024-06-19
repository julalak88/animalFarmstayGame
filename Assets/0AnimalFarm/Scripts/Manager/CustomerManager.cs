using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public List<GameObject> customers;

    Dictionary<string, GameObject> customerDict = new Dictionary<string, GameObject>();
    SceneGameManager sceneManager;
    MenuController menu;
    SaveManager saveManager;
    List<Character> characters = new List<Character>();
    [HideInInspector]
    public bool notCreate = true;
    [HideInInspector]
    public bool touchToTalk = true;
    float nC = 0, mC, mnRnd, mxRnd;
    float maxRandom = 20f;
    int maxAdCustomer = 0, adCustomer = 35, maxCustomer = 60;
    int indScene = 0;

    public int NumCustomers {
        get { return characters.Count; }
    }

    private void Start() {

        sceneManager = GameManager.Ins.sceneManager;
        menu = GameManager.Ins.uiManager.menu;
        saveManager = SaveManager.Ins;
        /*for (int i = 0; i < 15; i++) {
            DOVirtual.DelayedCall(i * 1f, () => CreateTestCharacter("Home"));
        }*/

        mnRnd = 3f;
        mxRnd = maxRandom;
        mC = UnityEngine.Random.Range(1f, 3f);
        
    }

    void Test()
    {
        CreateCharacter("gst0005");
    }

    public void InitCustomers(List<string> _customers) {
        string cusName;
        for (int i = 0; i < _customers.Count; i++) {
            cusName = _customers[i];
            GameObject cusObj = (GameObject)Resources.Load("Customers/" + cusName);
            customers.Add(cusObj);
            customerDict.Add(cusName, cusObj);
        }
    }

    public void AddCustomer(string customerName) {
        GameObject cusObj = (GameObject)Resources.Load("Customers/" + customerName);
        customers.Add(cusObj);
        customerDict.Add(customerName, cusObj);
    }

    public void LoadCustomer() {
        if (saveManager == null) saveManager = SaveManager.Ins;
        if (sceneManager == null) sceneManager = GameManager.Ins.sceneManager;
        if(menu == null) menu = GameManager.Ins.uiManager.menu;
        int length = saveManager.data.customer_save.Count;
        CustomerSaveData data;
        Character _char;
        for (int i = 0; i < length; i++) {
            data = saveManager.data.customer_save[i];
            _char = Instantiate(customerDict[data.customerName], transform).GetComponent<Character>();
            _char.transform.position = data.position;
            _char.name = data.customerName;
            //if (data.currentScene == "Riverside") _char.transform.localScale = Vector3.one * 1.2f;
            //else _char.transform.localScale = Vector3.one;
            _char.OnStart(false);
            _char.currentScene = sceneManager.scenes[data.currentScene];
            _char.numActivity = data.activityRemain;
            _char.RandomRequest();
            characters.Add(_char);
        }
        saveManager.data.customer_save.Clear();
        print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> "+(length + maxAdCustomer) + " " + adCustomer);
        if (length + maxAdCustomer < adCustomer) menu.OnNotificationReady(true);
        else menu.OnNotificationReady(false);
    }

    public void SaveCustomers() {
        print("Save Customers");
        saveManager.data.customer_save.Clear();
        CustomerSaveData data;
        Character _char;
        for (int i = 0; i < characters.Count; i++) {
            _char = characters[i];
            if (_char.numActivity == 0) continue;
            data = new CustomerSaveData();
            data.customerName = _char.name;
            data.currentScene = _char.currentScene.name;
            data.position = _char.transform.position;
            data.activityRemain = _char.numActivity;
            saveManager.data.customer_save.Add(data);
        }
    }

    public void UpdateCustomers() {
        //if (!ready) return;
        Character _char;
        for (int i = 0; i < characters.Count; i++) {
            _char = characters[i];
            _char.OnUpdate();
            if (_char.isGoingOut) {
                Destroy(_char.gameObject);
                characters.RemoveAt(i);
                i--;
            }
        }

        nC += Time.deltaTime;
        if (nC > mC) {
            nC = 0;
            mC = UnityEngine.Random.Range(mnRnd, mxRnd);
            if (maxAdCustomer > 0) {
                if (--maxAdCustomer == 0) {
                    mnRnd = 3f;
                    mxRnd = maxRandom;
                }
            }
          
            CreateCharacter();
            
        }
    }

    void CreateCharacter() {
        if (customers.Count == 0 || characters.Count > maxCustomer) return;
        int rnd = UnityEngine.Random.Range(0, customers.Count);
        Character _char = Instantiate(customers[rnd], transform).GetComponent<Character>();
        _char.name = _char.name.Replace("(Clone)", "");
        //_char.transform.localScale = Vector3.one * .8f;
        indScene = (++indScene) % saveManager.data.unlocked_scene.Count;
        string _scene = saveManager.data.unlocked_scene[indScene];
        _char.currentScene = sceneManager.scenes[_scene];
        _char.OnStart(false);
        _char.RandomRequest();
        characters.Add(_char);
        if (characters.Count + maxAdCustomer < adCustomer) menu.OnNotificationReady(true);
    }

    public void CreateCharacter(string customerName) {//ForNewCharacter
        GameObject cusObj = (GameObject)Resources.Load("Customers/" + customerName);
        //Character _char = Instantiate(customerDict[customerName], transform).GetComponent<Character>();
        Character _char = Instantiate(cusObj, transform).GetComponent<Character>();
        _char.name = _char.name.Replace("(Clone)", "");
        _char.currentScene = sceneManager.scenes["Home"];
        _char.OnStart(true);
        _char.GotoBusStop();
        if (!GameManager.Ins.unlockManager.CheckUnlockCustomer(customerName))
        {
            saveManager.data.unlocked_customer.Add(customerName);
        }
        characters.Add(_char);
    }

    
    public void AddMoreCustomers() {
        mnRnd = 0.5f;
        mxRnd = 1.5f;
        maxAdCustomer = adCustomer;
        mC = 0;
        /*if (characters.Count + maxAdCustomer > maxCustomer)*/ menu.OnNotificationReady(false);
    }

    
}
