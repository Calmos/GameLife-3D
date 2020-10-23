using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newcell : MonoBehaviour
{
    [SerializeField] int conditionVie = 3; //default 3
    [SerializeField] GameObject bloc;
    [SerializeField] LayerMask mask;
    GameManager gm;
    bool born = false;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.cells.Add(gameObject);

        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity, mask);

        if(colliders.Length == conditionVie)
        {
            born = true;
        }
    }

    public void Born()
    {
        Debug.Log("born function called");
        if (born)
        {
            Instantiate(bloc, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
