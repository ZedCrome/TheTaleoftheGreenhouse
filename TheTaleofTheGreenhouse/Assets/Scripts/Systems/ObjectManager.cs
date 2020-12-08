using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;
    
    void Awake()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
    }
    
    public int GetStackNumber(GameObject trackedObject)
    {
        if (trackedObject == null)
        {
            return 0;
        }
        
        bool shouldContinue = true;
        int operationSteps = 0;
        int stackCounter = 1;

        var nextTrackedObject = trackedObject;
        
        do
        {
            if (nextTrackedObject.transform.parent.CompareTag("TableSlot"))
            {
                shouldContinue = false;
            }
            else
            {
                nextTrackedObject = nextTrackedObject.transform.parent.gameObject;
                stackCounter++;
            }
            
            if (operationSteps < 20)
            {
                operationSteps++;
            }
            else
            {
                shouldContinue = false;
            }
        } while (shouldContinue == true);

        return stackCounter;
    }
}
