using UnityEngine;
using System.Collections;

/// <summary>
/// 手札のコントローラークラス 
/// 手札をリストで管理し、不足分のカードをデッキ(DeckController）に要求する役目
/// </summary>
public class HandController : MonoBehaviour
{
	[SerializeField]
	private	DeckController _deckController;

	public GameObject handCardPrefab;

	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		for (int i = -5; i < -2; i++) {
			GameObject draw = Instantiate (handCardPrefab)as GameObject;
			draw.transform.position = new Vector3 (i, 0, 3);

		
		}
	}

	void Update() 
	{
	}	
	
}
