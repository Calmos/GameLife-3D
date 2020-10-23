using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isPlaying = false;
    [SerializeField] float speedSim = 1;

    float lastUpdate = 0;
    public List<GameObject> blocs;
    public List<GameObject> cells;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPlaying = !isPlaying;
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            speedSim++;
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            speedSim--;
            if(speedSim < 1)
            {
                speedSim = 1;
            }
        }

        if (isPlaying)
        {
            if(Time.time >= lastUpdate + 1/speedSim)
            {
                lastUpdate = Time.time;
                StartCoroutine(UpdateGeneration());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                StartCoroutine(UpdateGeneration());
            }
        }
        
    }

    IEnumerator UpdateGeneration()
    {
        foreach (GameObject bloc in blocs)
        {
            bloc.GetComponent<LifeUpdate>().NewGeneration();
        }
        yield return new WaitForSeconds(0.2f);

        List<GameObject> toRemove = new List<GameObject>();
        foreach (GameObject bloc in blocs)
        {
            if (bloc.GetComponent<LifeUpdate>().die)
            {
                toRemove.Add(bloc);
            }
        }

        foreach (GameObject item in toRemove)
        {
            blocs.Remove(item);
            Destroy(item);
        }
        toRemove.Clear();

        foreach (GameObject cell in cells)
        {
            cell.GetComponent<Newcell>().Born();
        }
        cells.Clear();
    }
}
