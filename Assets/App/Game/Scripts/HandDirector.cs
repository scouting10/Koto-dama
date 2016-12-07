using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
//Listの使用に必要
using System.Linq;

/// <summary>
/// HandDirectorは読んで字のごとく手札の統括を行うためのクラスである。 基本的に手札に関する命令はHandDirectorが行う。 HandDirectorは全ての手札をリストで保持する。手札のイベントを全て受け取れる。 受け取ったイベントごとにメソッドを走らせる。上記の要素を満たすように構成する。
/// </summary>

public class HandDirector: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{


	[SerializeField]
	private	DeckController _deckController;
	[SerializeField]
	private CardGenerator _cardGenerator;

	// 手札が格納されるList
	List<HandController> handList;

	// HandCardの位置
	private float handPos_x = 190;
	private float handPos_y = -90;

	// カードデータがきちんと完成したら、これを使う。
	//List<WordRawData> list = MasterDataManager.Instance.wordMasterData._wordRawDataList;


	// HandControllerから送られる、合成カードの箱。
	HandController handCard_a;
	HandController handCard_b;
	public Vector3 startPos; //までいるかな？

	// おそらく、deck, field, obj側のOnPointerEnter内で、このboolをコントロールする。いや、その時の動きは、例えばFieldDirectorにやらせるのだろうか。
	bool deck = false;
	bool field = false;
	bool obj = false;

	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		
		// 最初のHandCard配置
		for (int i = 0; i < 5; i++) {
			//int a = Random.Range (0, list.Count); 
			// データ完成後はこちら。今は歯抜けなので↓
			HandController firstDraw = _deckController.GetHandFromDeck();
			// ↓実は、.で上流に遡れる！　そして、また降りてこれる！ これもOK → firstDraw.gameObject.transform.gameObject.GetComponent<RectTransform> ();
			RectTransform rect = firstDraw.gameObject.GetComponent<RectTransform> ();
			rect.anchoredPosition = new Vector2 (handPos_x+100*i, handPos_y);

			/* 
			↓の書き方では、だめ。
			Vector2 rect = firstDraw.gameObject.GetComponent<RectTransform> ().anchoredPosition;
			rect = new Vector2 (handPos_x+100*i, handPos_y);
			どうやらここまでやると、Vector2の値だけ、魚拓みたいにとるらしい。途中の流れは保存されない。

			これも、だめ。
			firstDraw.transform.position = new Vector2 (handPos_x+100*i, handPos_y);
			*/
			i = firstDraw.number;
			handList.Add (firstDraw); 

		}


	}
	
	// Update is called once per frame
	void Update () {

		//手札の数が足りてないかどうかのチェックは必要だろうか？
	}


	// 手札がタッチダウンされた瞬間
	public void OnPointerDown(HandController selfCard){
		handCard_a = selfCard;
	}

	public void OnDrag(HandController selfCard){
		//handCard_a = selfCard; 値を何度も入れるようなコードにしない。
	}

	//手札がタッチアップされた瞬間
	public void OnPointerUp() {

		// カードと重なってるならCardGeneratorで合成発動
		if(handCard_a != null && handCard_b != null){
			HandController comCard = _cardGenerator.Combination (handCard_a.word_data.id, handCard_b.word_data.id);

/*          多分、startPosは途中で値変わるとまずいから、private据え置きがいいんだけど、やり方がわからん。
			Vector3 _startPos{
				get{
					return handCard_a.startPos;
				}
*/

			comCard.transform.position = handCard_a.startPos;


		// ここから下の処理は、例えばFieldDirectorにさせるのかもなぁ。
		}else if(deck){
			
		}else if(field){
			
		}else if(obj){
			
		}
	}

	public void OnPointerEnter(HandController otherCard){
		handCard_b = otherCard;
	}

	// 手札同士をあわしている状態からドラッグしたまま外した瞬間
	public void OnPointerExit (HandController otherCard){
		handCard_b = null;
	}



/* この辺、OnPointerUpでオブジェクトごと持ってこれるのを知る前の残骸たち。
 * int型で合成の可否を判定したり、、、試行錯誤。
 
	// HandControllerから呼ばれる、手札合成時にカード作る関数
	合成に失敗した時には、comCardにはnew HandController、つまり、初期の、data空っぽのインスタンスが返ってくる。その場合は、comCard (HandController型だが、Generatorが渡してるのはPrefab)壊しておしまい。
	わかりづらいが、ここでは新しいカードを作っているだけで、合成元のカードには干渉していない。合成元のカードについては、HandController内で処理する。
	そして、こんなやり方があったとは！ComCardをint型にすることで、呼び出し元のカードに、合成の正否を伝えられる。
	int ComCard (string a, string b, Vector3 c){   

		HandController comCard=_cardGenerator.Combination(handCardId_a,handCardId_b);
		if (comCard.com_data.id != "") {
			comCard.transform.position = c;
			Debug.Log ("GetComCard");
			return -1;
		}
		Destroy (comCard.gameObject);
		return 0;
	}


	// HandControllerから呼ばれる、Deckアタッチ時にカード作る関数
	// Deckにアタッチした時に起きること、の記述はdeckcontroller内にする？今は、新しいカードの補充だけ記述。引数は元のカードのポジションだけ。
	public void ComDeck (Vector3 c){
		// 何をするか未定。とりあえず、アタッチしたカードは消えるので、Generatorに新しいカード要求
		//int a = Random.Range(0, List.Count); ← 完成系はこちら。
		int a = Random.Range(0,20);
		HandController newCard = _cardGenerator.Create (a);
		newCard.transform.position = c;

	}


	// HandControllerから呼ばれる、フィールドアタッチ、オブジェクトアタッチ時にカード作る関数
	// おそらくだが、ComCardと異なり、カードid送り先はフィールド側、オブジェクト側の関数内になるはず
	// なので、ComField()とComObject（）は初期位置しか引数に持たない（はず）。 ← 上記、ComDeckも一緒
	public void ComField (Vector3 c){
		// 何をするか未定。とりあえず、アタッチしたカードは消えるので、Generatorに新しいカード要求
		//int a = Random.Range(0, List.Count); ← 完成系はこちら。
		int a = Random.Range(0,20);
		HandController newCard = _cardGenerator.Create (a);
		newCard.transform.position = c;

	}

	public void ComObject (Vector3 c){
		// 何をするか未定。とりあえず、アタッチしたカードは消えるので、Generatorに新しいカード要求
		//int a = Random.Range(0, List.Count); ← 完成系はこちら。
		int a = Random.Range(0,20);
		HandController newCard = _cardGenerator.Create (a);
		newCard.transform.position = c;

	}
*/

}
