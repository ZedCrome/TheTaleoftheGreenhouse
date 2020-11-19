using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestPlayerRenderer : MonoBehaviour
{
    public static readonly string[] staticDirections =
    {
        "staticNorth", "staticNorthWest", "staticWest", "staticSouthWest", "staticSouth", "staticSouthEast",
        "staticEast", "staticNorthEast"
    };
    
    public static readonly string[] runDirections =
    {
        "runNorth", "runNorthWest", "runWest", "runSouthWest", "runSouth", "runSouthEast",
        "runEast", "runNorthEast"
    };

    private static readonly int MoveDirection = Animator.StringToHash("MoveDirection");

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int lastDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void SetDirection(Vector2 direction)
    {
        string[] directionArray;
        int directionValue;

        if (direction.magnitude < 0.01f)
        {
            directionArray = staticDirections;
            directionValue = Array.IndexOf(directionArray, 8);
        }
        else
        {
            directionArray = runDirections;
            lastDirection = DirectionToIndex(direction, 8);
            
        }
        animator.SetFloat((int) MoveDirection, lastDirection);
        animator.Play(directionArray[lastDirection]);
    }


    public static int DirectionToIndex(Vector2 direction, int directionCount)
    {
        Vector2 normalizedDirection = direction.normalized;
        
        //calculate degrees per slice
        float slice = 360f / directionCount;
        
        //Get center of slices so that the "north" direction for example is aligned in the center. 
        float halfslice = slice / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normalizedDirection);
        
        angle += halfslice;

        if (angle < 0)
        {
            angle += 360;
        }

        float sliceCount = angle / slice;

        return Mathf.FloorToInt(sliceCount);
    }
}
