using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// デッキのコントローラークラス
/// デッキに含まれるカードのリストを保持したり、手札(HandController）にカードを提供する役目を担う 
/// </summary>
public class DeckController : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private HandDirector _handDirector;
	[SerializeField]
	private CardGenerator _cardGenerator;

	// DeckCardの位置
	private float deckPos_x = 90;
	private float deckPos_y = -90;



	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		
		// DeckCard配置
		DeckController firstDeck = _cardGenerator.DeckCreate();
		RectTransform deckPos= firstDeck.gameObject.GetComponent<RectTransform>();
		deckPos.anchoredPosition = new Vector2 (deckPos_x,deckPos_y);

		_handDirector.Initialize ();

	}




	// Update is called once per frame
	void Update () {

	}	

	// 内容未定。カードとDeckのアタッチ時に、HandControllerから呼ばれる。
	public void ComDeck(string a){
	
	}

	// 手札をマウスオーバーされた時に、HandControllerにDeckCardの情報を送る。
	public void OnPointerEnter(PointerEventData eventData)
	{

	}

	public void OnPointerExit(PointerEventData eventData)
	{

	}
	public void OnDrop(PointerEventData eventData)
	{

	}


}
