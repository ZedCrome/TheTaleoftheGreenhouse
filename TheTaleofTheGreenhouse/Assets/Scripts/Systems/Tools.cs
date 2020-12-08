using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    private string[] tagList = { "WaterCan",  };
    public static AudioClip GetRandomSound(AudioClip[] audioClip)
    {
        if (audioClip.Length == 0)
        {
            Debug.LogError("AudioClip does not contain any audio files");
            return null;
        }
        
        int random = Random.Range(0, audioClip.Length);

        return audioClip[random];
    }
    
    public static int GetStackNumber(GameObject trackedObject)
    {
        if (trackedObject == null)
        {
            return 0;
        }
        
        bool shouldContinue = true;
        int operationSteps = 0;
        int stackCounter = 1;

        GameObject nextTrackedObject = trackedObject;
        
        do
        {
            if (nextTrackedObject.transform.parent != null)
            {
                if (nextTrackedObject.transform.parent.CompareTag("TableSlot"))
                {
                    shouldContinue = false;
                }
                else
                {
                    nextTrackedObject = nextTrackedObject.transform.parent.gameObject;

                    if (nextTrackedObject.HasComponent<StackIndex>())
                    {
                        stackCounter++;
                    }
                }
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

    public static int GetSplitStackSize(GameObject trackedObject)
    {
        if (trackedObject == null)
        {
            return 0;
        }
        
        bool shouldContinue = true;
        int operationSteps = 0;
        int stackCounter = 1;

        GameObject nextTrackedObject = trackedObject;
        
        do
        {
            Transform[] childObjects = nextTrackedObject.GetComponentsInChildren<Transform>();

            if (childObjects != null)
            {
                foreach (Transform obj in childObjects)
                {
                    if (obj.gameObject.HasComponent<ObjectSlot>())
                    {
                        if (obj.gameObject.GetComponent<ObjectSlot>().objectInSlot != null)
                        {
                            stackCounter++;
                            nextTrackedObject = obj.gameObject.GetComponent<ObjectSlot>().objectInSlot;
                            nextTrackedObject.GetComponent<StackIndex>().indexNumber = stackCounter;
                        }
                        else
                        {
                            return stackCounter;
                        }
                    }
                }
            }
            else
            {
                return stackCounter;
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
