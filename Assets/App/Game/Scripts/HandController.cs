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

	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		
	}
}
