using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public int index = 0;
    public int special_scene = 0;
    public float speedMove = .5f;
    public GameObject arrow, prev, next;
    //public GameObject RiversideUI;

    Vector2 stampPos;
    float pressTime;
    SceneGameManager sceneManager;
    int num_scene = 0;
    [SerializeField]
    GameObject currentSceneUI;
    SceneGame currentSceneGame;
     SaveManager saveManager;
    Tween tw;
    GameManager gm;
    bool _enable = true;
    public bool enable {
        set { _enable = value; arrow.SetActive(value); }
        get { return _enable; }
    }

    public SceneGame currentScene {
        get { return currentSceneGame; }
    }

    private void Start() {
        gm = GameManager.Ins ;
        sceneManager = gm.sceneManager;
        sceneManager.sceneChanger = this;
        special_scene = 0;
        for (int i = 0; i < sceneManager.requestScenes.Count; i++) {
            if (sceneManager.requestScenes[i].special) special_scene++;
        }
        num_scene = sceneManager.requestScenes.Count - special_scene;
        //RiversideUI.SetActive(false);
        currentSceneGame = sceneManager.requestScenes[index];
        currentSceneGame.OnSceneActive();
         saveManager = SaveManager.Ins;
    }

    /*void Update() {

        if (Input.GetMouseButtonDown(0)) {
            stampPos = Input.mousePosition;
            pressTime = Time.time;
            //CreateTestCharacter("Home");
        } else if (Input.GetMouseButtonUp(0)) {

            Vector2 cur = Input.mousePosition;
            float dist = cur.x - stampPos.x;
            float elapedTime = Time.time - pressTime;
            if (elapedTime < 0.2f) {
                if (dist > 0) {
                    if(index > 0) {
                        index--;
                        Vector3 pos = transform.position;
                        pos.x = sceneManager.requestScenes[index].transform.position.x;
                        transform.position = pos;
                    }
                } else {
                    if(index < sceneManager.requestScenes.Count-1) {
                        index++;
                        Vector3 pos = transform.position;
                        pos.x = sceneManager.requestScenes[index].transform.position.x;
                        transform.position = pos;
                    }
                }
            }

        }

    }*/

    public void NextScene() {
        if (!_enable || currentSceneGame.special) return;
        if (index < num_scene - 1) {
            if (sceneManager.requestScenes[index + 1].special) return;
            if (sceneManager.IsSceneUnlocked(index + 1)) {

                if (tw != null) tw.Kill();
                tw = null;

                prev.SetActive(true);
                index++;
                if (index == num_scene - 1) next.SetActive(false);
                if (currentSceneGame) currentSceneGame.OnSceneInactive();

                currentSceneGame = sceneManager.requestScenes[index];
                Vector3 pos = currentSceneGame.transform.position;
                pos.z = -10;
                enable = false;
                currentSceneGame.OnSceneActive();
                if (currentSceneUI) currentSceneUI.SetActive(false);
                tw = transform.DOMove(pos, speedMove).OnComplete(OnChangeSceneCompleted);

            } else {
                print("Scene : " + sceneManager.requestScenes[index + 1].name + " is locked");
                if (sceneManager.requestScenes[index + 1].name.Equals("Resort"))
                {

                    if (tw != null) tw.Kill();
                    tw = null;

                    next.SetActive(true);
                    index = 2;
                    if (index == 0) prev.SetActive(false);
                    if (currentSceneGame) currentSceneGame.OnSceneInactive();

                    currentSceneGame = sceneManager.requestScenes[index];
                    Vector3 pos = currentSceneGame.transform.position;
                    pos.z = -10;
                    enable = false;
                    currentSceneGame.OnSceneActive();
                    if (currentSceneUI) currentSceneUI.SetActive(false);
                    tw = transform.DOMove(pos, speedMove).OnComplete(OnChangeSceneCompleted);

                }
            }
        }
    }

    public void PrevScene() {
        if (!_enable || currentSceneGame.special) return;
        if (index > 0) {
            if (sceneManager.requestScenes[index - 1].special) return;
            if (sceneManager.IsSceneUnlocked(index - 1)) {

                if (tw != null) tw.Kill();
                tw = null;

                next.SetActive(true);
                index--;
                if (index == 0) prev.SetActive(false);
                if (currentSceneGame) currentSceneGame.OnSceneInactive();

                currentSceneGame = sceneManager.requestScenes[index];
                Vector3 pos = currentSceneGame.transform.position;
                pos.z = -10;
                enable = false;
                currentSceneGame.OnSceneActive();
                if (currentSceneUI) currentSceneUI.SetActive(false);
                tw = transform.DOMove(pos, speedMove).OnComplete(OnChangeSceneCompleted);

            } else {
                print("Scene : " + sceneManager.requestScenes[index - 1].name + " is locked");
                if (sceneManager.requestScenes[index - 1].name.Equals("Resort"))
                {

                    if (tw != null) tw.Kill();
                    tw = null;

                    next.SetActive(true);
                    index = 0;
                    if (index == 0) prev.SetActive(false);
                    if (currentSceneGame) currentSceneGame.OnSceneInactive();

                    currentSceneGame = sceneManager.requestScenes[index];
                    Vector3 pos = currentSceneGame.transform.position;
                    pos.z = -10;
                    enable = false;
                    currentSceneGame.OnSceneActive();
                    if (currentSceneUI) currentSceneUI.SetActive(false);
                    tw = transform.DOMove(pos, speedMove).OnComplete(OnChangeSceneCompleted);

                }
            }
        }
    }

    void OnChangeSceneCompleted() {
        enable = true;

    }

   

    public void MoveOutToPrevScene() {
        if (!currentSceneGame.special) return;
        currentSceneGame = sceneManager.requestScenes[index];
        Vector3 pos = currentSceneGame.transform.position;
        pos.z = -10;
        transform.position = pos;
        if (currentSceneUI) currentSceneUI.SetActive(false);
        prev.transform.parent.gameObject.SetActive(true);
    }

    public void MoveInSpecialScene() {
        if (currentSceneGame.special) return;
        if (currentSceneGame.name == "Home") {
            MoveInRiverside();
        }
    }

    public void MoveInRiverside() {
        prev.transform.parent.gameObject.SetActive(false);
        currentSceneGame = sceneManager.requestScenes[3];
        Vector3 pos = currentSceneGame.transform.position;
        pos.z = -10;
        transform.position = pos;
        //currentSceneUI = RiversideUI;
        //currentSceneUI.SetActive(true);
    }

    public void MoveToScene(int sceneIndex) {
        index = sceneIndex;

        if (sceneManager.requestScenes[index].special) return;
        if (sceneManager.IsSceneUnlocked(index)) {

            if (tw != null) tw.Kill();
            tw = null;

            if (currentSceneGame) currentSceneGame.OnSceneInactive();

            currentSceneGame = sceneManager.requestScenes[index];
            if (index == 0) {
                next.SetActive(true);
                prev.SetActive(false);
            } else if (index == num_scene - 1) {
                next.SetActive(false);
                prev.SetActive(true);
            } else {
                next.SetActive(true);
                prev.SetActive(true);
            }
       

            Vector3 pos = currentSceneGame.transform.position;
            pos.z = -10;
            transform.position = pos;
            currentSceneGame.OnSceneActive();
            if (currentSceneUI) currentSceneUI.SetActive(false);
        }
    }
 }
