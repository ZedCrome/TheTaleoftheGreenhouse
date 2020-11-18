using UnityEngine;

public class TestTableItemSpot : MonoBehaviour
{
	public bool hasItem;

	SpriteRenderer spriteRenderer;

	public enum GameItems
    {
		Empty,
		Pot,	
    };

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer.sprite == null)
		{
			PlacedItem(GameItems.Empty);
		}
		else
        {
			Debug.Log("Load in what items it has saved");
        }
	}

	public void PlacedItem(GameItems newState)
	{
		//Trying to make a state machine depending on items.
		switch (newState)
		{
			case GameItems.Empty:
				{
					Debug.Log(gameObject + " is empty");
				}
				break;


			case GameItems.Pot:
				{
					Debug.Log(gameObject + " have cute Pot");
				}
				break;
		}
	}	
}
