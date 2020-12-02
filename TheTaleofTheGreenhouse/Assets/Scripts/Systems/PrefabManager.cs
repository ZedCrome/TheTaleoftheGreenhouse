using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;
    
    public GameObject[] prefabList;
    
    void Awake()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
    }

    public GameObject CreateNewObjectInstance(string objectTag)
    {
        foreach (GameObject prefabObject in prefabList)
        {
            if (prefabObject.CompareTag(objectTag))
            {
                GameObject newObject = Instantiate(prefabObject);
                return newObject;
            }
        }
    
        Debug.LogError("Could not find prefab with tag: " + objectTag);
        return null;
    }
}
