using UnityEngine;
using System.Collections;

/// <summary>
/// デッキのコントローラークラス
/// デッキに含まれるカードのリストを保持したり、手札(HandController）にカードを提供する役目を担う 
/// </summary>
public class DeckController : MonoBehaviour
{
	[SerializeField]
	private HandDirector _handDirector;
	[SerializeField]
	private CardGenerator _cardGenerator;

	// DeckCardの位置
	private float deckPos_x = -5f;
	private float deckPos_z = -3f;



	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		
		// DeckCard配置
		DeckController firstDeck = _cardGenerator.DeckCreate();
		firstDeck.transform.position = new Vector3 (deckPos_x, 0, deckPos_z);

		_handDirector.Initialize ();

	}




	// Update is called once per frame
	void Update () {

	}	

	// 内容未定。カードとDeckのアタッチ時に、HandControllerから呼ばれる。
	public void ComDeck(string a){
	
	}


}
