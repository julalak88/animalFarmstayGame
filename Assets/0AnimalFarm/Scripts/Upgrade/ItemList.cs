using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ItemList : SerializedMonoBehaviour
{
    public Database database;
    public Dictionary<string, Dictionary<string, List<ItemDatabase>>> items;

    [Button("Apply List Global")]
    void ApplyListGlobal() {

        List<string> ObjUpgrade = new List<string>() {"Lamp"};

        Dictionary<string, List<ItemDatabase>> data;
        List<ItemDatabase> _list, _list2;

        if (items.ContainsKey("Global")) data = items["Global"];
        else data = new Dictionary<string, List<ItemDatabase>>();

        string _objName;
        for (int a = 0; a < ObjUpgrade.Count; a++) {
            _objName = ObjUpgrade[a];
            _list = database.items["Global"].Where(x => x.Value.itemsType.ToString().StartsWith(_objName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value).ToList();
            _list = _list.OrderBy(x => x.id).ToList();
            if (!data.ContainsKey(_objName)) {
                data[_objName] = _list;
            } else {
                _list2 = data[_objName];
                bool found;
                for (int i = 0; i < _list.Count; i++) {
                    found = false;
                    for (int j = 0; j < _list2.Count; j++) {

                        if (_list[i].name == _list2[j].name) {
                            found = true;
                            break;
                        }
                    }
                    if (!found) _list2.Add(_list[i]);
                }
            }
        }
      
        items["Global"] = data;
    }


    [Button("Apply List Home")]
    void ApplyListHome() {

        List<string> ObjUpgrade = new List<string>() { "House", "Shop", "Flowers", "Decoration", "Mailbox", "Fence" ,"Bench"};

        Dictionary<string, List<ItemDatabase>> data;
        List<ItemDatabase> _list, _list2;

        if (items.ContainsKey("Home")) data = items["Home"];
        else data = new Dictionary<string, List<ItemDatabase>>();

        string _objName;
        for (int a = 0; a < ObjUpgrade.Count; a++) {
            _objName = ObjUpgrade[a];
            _list = database.items["Home"].Where(x => x.Value.itemsType.ToString().StartsWith(_objName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value).ToList();
            _list = _list.OrderBy(x => x.id).ToList();
            if (!data.ContainsKey(_objName)) {
                data[_objName] = _list;
            } else {
                _list2 = data[_objName];
                bool found;
                for (int i = 0; i < _list.Count; i++) {
                    found = false;
                    for (int j = 0; j < _list2.Count; j++) {

                        if (_list[i].name == _list2[j].name) {
                            found = true;
                            break;
                        }
                    }
                    if (!found) _list2.Add(_list[i]);
                }
            }
        }
        items["Home"] = data;
    }

   
    [Button("Apply List Riverside")]
    void ApplyListRiverside() {
        List<string> ObjUpgrade = new List<string>() { "FoodtruckLeft", "FoodtruckRight", "Table", "Kitchen"};

        Dictionary<string, List<ItemDatabase>> data;
        List<ItemDatabase> _list, _list2;

        if (items.ContainsKey("Riverside")) data = items["Riverside"];
        else data = new Dictionary<string, List<ItemDatabase>>();

        string _objName;
        for (int a = 0; a < ObjUpgrade.Count; a++) {
            _objName = ObjUpgrade[a];
            _list = database.items["Riverside"].Where(x => x.Value.itemsType.ToString().StartsWith(_objName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value).ToList();
            _list = _list.OrderBy(x => x.id).ToList();
            if (!data.ContainsKey(_objName)) {
                data[_objName] = _list;
            } else {
                _list2 = data[_objName];
                bool found;
                for (int i = 0; i < _list.Count; i++) {
                    found = false;
                    for (int j = 0; j < _list2.Count; j++) {

                        if (_list[i].name == _list2[j].name) {
                            found = true;
                            break;
                        }
                    }
                    if (!found) _list2.Add(_list[i]);
                }
            }
        }
        items["Riverside"] = data;
    }

    [Button("Apply List Farm")]
    void ApplyListFarm() {

        List<string> ObjUpgrade = new List<string>() { "Barn", "Cart","Pond"};

        Dictionary<string, List<ItemDatabase>> data;
        List<ItemDatabase> _list, _list2;

        if (items.ContainsKey("Farm")) data = items["Farm"];
        else data = new Dictionary<string, List<ItemDatabase>>();

        string _objName;
        for (int a = 0; a < ObjUpgrade.Count; a++) {
            _objName = ObjUpgrade[a];
            _list = database.items["Farm"].Where(x => x.Value.itemsType.ToString().StartsWith(_objName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value).ToList();
            _list = _list.OrderBy(x => x.id).ToList();
            if (!data.ContainsKey(_objName)) {
                data[_objName] = _list;
            } else {
                _list2 = data[_objName];
                bool found;
                for (int i = 0; i < _list.Count; i++) {
                    found = false;
                    for (int j = 0; j < _list2.Count; j++) {

                        if (_list[i].name == _list2[j].name) {
                            found = true;
                            break;
                        }
                    }
                    if (!found) _list2.Add(_list[i]);
                }
            }
        }
        items["Farm"] = data;
    }

    [Button("Apply List Resort")]
    void ApplyListResort(){

        List<string> ObjUpgrade = new List<string>() { "Bridge", "House","Shop", "Square" };

        Dictionary<string, List<ItemDatabase>> data;
        List<ItemDatabase> _list, _list2;

        if (items.ContainsKey("Resort")) data = items["Resort"];
        else data = new Dictionary<string, List<ItemDatabase>>();

        string _objName;
        for (int a = 0; a < ObjUpgrade.Count; a++) {
            _objName = ObjUpgrade[a];
            _list = database.items["Resort"].Where(x => x.Value.itemsType.ToString().StartsWith(_objName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value).ToList();
            _list = _list.OrderBy(x => x.id).ToList();
            if (!data.ContainsKey(_objName)) {
                data[_objName] = _list;
            } else {
                _list2 = data[_objName];
                bool found;
                for (int i = 0; i < _list.Count; i++) {
                    found = false;
                    for (int j = 0; j < _list2.Count; j++) {

                        if (_list[i].name == _list2[j].name) {
                            found = true;
                            break;
                        }
                    }
                    if (!found) _list2.Add(_list[i]);
                }
            }
        }
        items["Resort"] = data;
    }



}

[Serializable]
public class NewItem
{
    public string name;
    public Sprite image;
}