using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public enum EntityType
    {
        Table,
        Pot   
    }
    
    [SerializeField]
    private List<Entity> entity = new List<Entity>();
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private class Entity
    {
        public EntityType entityType;

        public int id;
        public GameObject gameObject;
    }

    public int AddEntity(GameObject newObject, EntityType type)
    {
        int freeIndex = CheckForFreeIndexSlot();

        if (freeIndex > -1)
        {
            //entity[freeIndex].gameObject = 
        }
        else
        {
            
        }
        
        //entity[newId].id = newId;
        //entity[newId].gameObject = newObject;

        //return newId;

        return 1;
    }
    
    
    private int CheckForFreeIndexSlot()
    {
        for (int i = 0; i < entity.Count; i++)
        {
            if (entity[i].gameObject == null)
            {
                return i;
            }       
        }

        return -1;
    }
}
