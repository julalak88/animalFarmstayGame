using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuesthouseCleaning : MonoBehaviour
{
    public BoxCollider2D coll2D;
    [SerializeField]
    Guesthouse guesthouse;
    public GameObject effectWarning;
    public GameObject effectClean;
    private void Start()
    {
        guesthouse = GetComponentInParent<Guesthouse>();
        //coll2D.enabled = false;
    }
    public void GuestCheckOut()
    {
        coll2D.enabled = true;
        effectWarning.SetActive(true);
    }

    public void Cleaning()
    {
        coll2D.enabled = false;
        guesthouse.isClean = true;
        effectClean.SetActive(true);
        effectWarning.SetActive(false);
        
    }
}
