using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterList : SerializedMonoBehaviour
{
    public Database database;
    public List<CustomerDatabase> guests;
    public List<CustomerDatabase> villgers;

    [Button("Apply List Guest")]
    void ApplyListGuest() {
        List<CustomerDatabase> data;
        List<CustomerDatabase> _list, _list2;

        if (guests.Count == 0) data = guests;
        else data = new List<CustomerDatabase>();

        _list = database.customerDescription.Select(x => x.Value).Where(p => p.type.Equals(CharacterType.Guest)).ToList();

        if (data.Count == 0) {
            data = _list;
        } else {
            _list2 = data;
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
        data = data.OrderBy(x => x.id).ToList();
        guests = data;
    }

     

    [Button("Apply List Villgers")]
    void ApplyListVillgers() {
        List<CustomerDatabase> data;
        List<CustomerDatabase> _list, _list2;

        if (villgers.Count == 0) data = villgers;
        else data = new List<CustomerDatabase>();

        _list = database.customerDescription.Select(x => x.Value).Where(p => p.type.Equals(CharacterType.Villager)).ToList();

        if (data.Count == 0) {
            data = _list;
        } else {
            _list2 = data;
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
        data = data.OrderBy(x => x.id).ToList();
        villgers = data;
    }
 

}
