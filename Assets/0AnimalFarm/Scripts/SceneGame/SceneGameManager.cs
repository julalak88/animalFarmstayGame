using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using Lean.Pool;

public class SceneGameManager : SerializedMonoBehaviour
{

    [HideInInspector]
    public SceneChanger sceneChanger;

    public LeanGameObjectPool coinPool;
    public Dictionary<string, SceneGame> scenes;
    public List<SceneGame> requestScenes;
    public FoodManager foodManager;
    public HarvestManager harvestManager;
    public FarmSceneGame farmScene;
    public RiversideSceneGame riverside;
    public ResortSceneGame resort;
    public CloudManager cloudManager;

    CustomerManager customer;
    ObjectMovement river;
    SaveManager saveManager;
    List<SceneGame> availableScenes = new List<SceneGame>();
    List<Coin> tempCoin = new List<Coin>();

    float n = 0;

    private void Awake() {
        customer = GetComponent<CustomerManager>();
        river = GetComponentInChildren<ObjectMovement>();
        //availableScenes.Add(scenes["Home"]);
        //availableScenes.Add(scenes["Riverside"]);
    }

    private void Start() {
        SoundManager.Ins.PlayBGM("BGM1");
        saveManager = SaveManager.Ins;
        for (int i = 0; i < saveManager.data.unlocked_scene.Count; i++) {
            availableScenes.Add(scenes[saveManager.data.unlocked_scene[i]]);
        }
    }

    void Update() {
        customer.UpdateCustomers();
        river.OnMovement();
        for (int i = 0; i < requestScenes.Count; i++)
            requestScenes[i].UpdateScene();

        if (++n == 2) {
            n = 0;
            cloudManager.UpdateClouds();
        }
    }

    /*public SceneGame RandomRequestScene() {
        int rnd = UnityEngine.Random.Range(0, requestScenes.Count);
        return requestScenes[rnd];
    }*/

    public SceneGame RandomAvailableScene() {
        int rnd = UnityEngine.Random.Range(0, availableScenes.Count);
        return availableScenes[rnd];
    }

    public SceneGame ToHomeScene() {
        return scenes["Home"];
    }

    public Coin CreateCoinDrop(Vector3 pos, Vector3 coinpos) {
        Coin coin = coinPool.Spawn(pos).GetComponent<Coin>();
        tempCoin.Add(coin);
        coin.MoveToPosition(coinpos);
        return coin;
    }

    public Coin CreateCoin(Vector3 pos ,Vector3 coinpos)
    {
   
        Coin coin = coinPool.Spawn(pos).GetComponent<Coin>();
        tempCoin.Add(coin);
        coin.MoveExp(coinpos);
        return coin;
    }

    public Coin CreateCoinDrop(Vector3 pos) {
        Coin coin = coinPool.Spawn(pos).GetComponent<Coin>();
        tempCoin.Add(coin);
        coin.SetFromLoad(pos);
        return coin;
    }

    public void ReturnCoin(Coin coin) {
        tempCoin.Remove(coin);
        coinPool.Despawn(coin.gameObject);
    }

    public void LoadCoin() {
        if (saveManager == null) saveManager = SaveManager.Ins;
        int length = saveManager.data.coin_save.Count;
        CoinSaveData data;
        Coin coin;
        for (int i = 0; i < length; i++) {
            data = saveManager.data.coin_save[i];
            coin = CreateCoinDrop(data.position);
            coin.value = data.value;
            coin.isCoin = data.isCoin;
        }
        saveManager.data.coin_save.Clear();
    }

    public void SaveCoin() {
        print("Save Coins");
        saveManager.data.coin_save.Clear();
        CoinSaveData data;
        Coin coin;
        for (int i = 0; i < tempCoin.Count; i++) {
            coin = tempCoin[i];
            data = new CoinSaveData();
            data.value = coin.value;
            data.isCoin = coin.isCoin;
            data.position = coin.targetPosition;
            saveManager.data.coin_save.Add(data);
        }
    }

    public bool IsSceneUnlocked(int index) {
        return saveManager.data.unlocked_scene.Contains(requestScenes[index].name);
    }

    public bool IsSceneUnlocked(string sceneName) {
        return saveManager.data.unlocked_scene.Contains(sceneName);
    }
}
