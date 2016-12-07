using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
//Listの使用に必要
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// デッキのコントローラークラス
/// デッキに含まれるカードのリストを保持したり、手札(HandController）にカードを提供する役目を担う 
/// DeckControllerは主にHandDirectorからの要求に答える役割を持つ
/// 例えば手札が足りなくなった場合はDeckControllerに用意された手札の配列からカードを 提供するといった具合である
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

	// 高取先生からもらった部分。
	//デッキに存在する手札のリスト [SerializeField]
	private List<HandController> _handList;
	//カードを提供する
	public HandController GetHandFromDeck() {  // これは、HandDirectorから呼ばれる。　例えば、 newCard = _deckController.GetHandFromDeck(); のような感じ。
		//デッキにカードが存在したら 
		if(_handList.Count > 0)
			{
				//一番最後の要素を取得
				HandController hand = _handList.Last(); 
				//一番最後の要素をリストから削除 
				_handList.RemoveAt(_handList.Count - 1); 
				//返す
				return hand;
			} 
		Debug.LogError("カードがデッキに存在しません"); 
		//nullを返す
		return null;
	}



	public void Initialize ()
	{
		
	 	// 合成防止の蓋として、DeckCardのタグを持つDecｋCardを配置。
		GameObject topDeck = _cardGenerator.DeckCreate();
		RectTransform topdeckPos= topDeck.GetComponent<RectTransform>();
		topdeckPos.anchoredPosition = new Vector2 (deckPos_x,deckPos_y);


		// DeckCardの下に、HandCardの束を配置。ひとまず、60枚のカードデックとする。
		for(int i = 0; i < 60 ; i++ ){
			int a = Random.Range (0, 20);

			HandController firstDeck = _cardGenerator.Create(a);
			RectTransform deckPos= firstDeck.gameObject.GetComponent<RectTransform>();
			deckPos.anchoredPosition = new Vector2 (deckPos_x,deckPos_y);
			_handList.Add (firstDeck);
		}

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
