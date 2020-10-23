using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpdate : MonoBehaviour
{
    public int blocsAround;
    [SerializeField] Vector2 conditionsMort = new Vector2(3, 2); //default 3, 2
    public bool die = false;
    [SerializeField] LayerMask mask = new LayerMask();
    [SerializeField] GameObject newCellObject = null;
    [SerializeField] Vector3[] newCells = null;
    GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.blocs.Add(gameObject);

        Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.zero, Quaternion.identity, mask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject && colliders[i].gameObject.transform.position == gameObject.transform.position && colliders[i].gameObject.tag != "Unbreakable")
            {
                gm.blocs.Remove(colliders[i].gameObject);
                Destroy(colliders[i].gameObject);
            }
        } 
    }

    public void NewGeneration()
    {
        #region die
        blocsAround = 0;

        Collider[] colliders = Physics.OverlapBox (transform.position, transform.localScale, Quaternion.identity, mask);

        blocsAround = colliders.Length - 1;

        if(blocsAround > conditionsMort.x || blocsAround < conditionsMort.y)
        {
            die = true;
        }
        #endregion
        #region create
        foreach(Vector3 newcell in newCells)
        {
            Instantiate(newCellObject, transform.position + newcell, Quaternion.identity);
        }
        #endregion
    }
}
