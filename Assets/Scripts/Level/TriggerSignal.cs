using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSignal : MonoBehaviour
{
    [SerializeField] GameObject[] movingobj = null;
    public float delaytime;
    public bool isImmediate = false;
    ObjectMovement objmovement;

    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(startMovingSequence());    
        
    }

    IEnumerator startMovingSequence()
    {
        for (int i = 0; i < movingobj.Length; i++)
        {
            if (!isImmediate)
            {
                objmovement = movingobj[i].GetComponent<ObjectMovement>();
                objmovement.Startmoving(true);
                yield return new WaitForSeconds(delaytime);
            }
            else
            {
                objmovement = movingobj[i].GetComponent<ObjectMovement>();
                objmovement.Startmoving(true);
            }
        }
    }


}
