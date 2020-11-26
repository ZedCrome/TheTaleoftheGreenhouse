using System;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
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
    private static readonly int Velocity = Animator.StringToHash("Velocity");

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private int lastDirection;
    private int lastDirectionCache;

    private float verticalMove;
    private float horizontalMove;
    private bool moving = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        verticalMove = Input.GetAxisRaw("Vertical");
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (verticalMove != 0 || horizontalMove != 0)
        {
            moving = true;
            animator.SetBool("Moving", true);
        }
        else
        {
            moving = false;
            animator.SetBool("Moving", false);
        }

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
            if (lastDirection != lastDirectionCache)
            {
                DirectionChange();
                lastDirectionCache = lastDirection;
            }
        }
    }

    void DirectionChange()
    {
        if (moving)
        {
            switch (lastDirection)
            {
                case 0:
                    animator.SetTrigger("RunNorth");
                    break;
                case 1:
                    animator.SetTrigger("RunNorthWest");
                    break;
                case 2:
                    animator.SetTrigger("RunWest");
                    break;
                case 3:
                    animator.SetTrigger("RunSouthWest");
                    break;
                case 4:
                    animator.SetTrigger("RunSouth");
                    break;
                case 5:
                    animator.SetTrigger("RunSouthEast");
                    break;
                case 6:
                    animator.SetTrigger("RunEast");
                    break;
                case 7:
                    animator.SetTrigger("RunNorthEast");
                    break;

                default:
                    break;
            }
        }

        if (!moving)
        {
            switch (lastDirection)
            {
                case 0:
                    animator.SetTrigger("StaticNorth");
                    break;
                case 1:
                    animator.SetTrigger("StaticNorthWest");
                    break;
                case 2:
                    animator.SetTrigger("StaticWest");
                    break;
                case 3:
                    animator.SetTrigger("StaticSouthWest");
                    break;
                case 4:
                    animator.SetTrigger("StaticSouth");
                    break;
                case 5:
                    animator.SetTrigger("StaticSouthEast");
                    break;
                case 6:
                    animator.SetTrigger("StaticEast");
                    break;
                case 7:
                    animator.SetTrigger("StaticNorthEast");
                    break;

                default:
                    break;
            }
        }
    }


    //Not written by Robin, kinda stole it from the webs.
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
