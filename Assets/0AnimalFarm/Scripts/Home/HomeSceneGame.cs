using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneGame : SceneGame
{
    public Staff staff;

    Plot[] plots;

    protected override void Awake() {
        base.Awake();
        plots = upgradeTransform.GetComponentsInChildren<Plot>();
    }

    protected override void Start() {
        base.Start();
        Dictionary<string, PlotData> objects = save.data.plots;
        for (int i = 0; i < plots.Length; i++) {
            if (objects.ContainsKey(plots[i].name)) plots[i].LoadObject(objects[plots[i].name]);
            else plots[i].Locked();
        }
    }


    public override void UpdateScene() {
        base.UpdateScene();
        staff.OnUpdate();
    }
}
