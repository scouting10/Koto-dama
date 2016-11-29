using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// デッキのコントローラークラス
/// デッキに含まれるカードのリストを保持したり、手札(HandController）にカードを提供する役目を担う 
/// </summary>
public class DeckController : MonoBehaviour
{
	[SerializeField]
	private HandCardsController _handCardsController;


	// Prefabの設定
	public GameObject deckCardPrefab;
	public GameObject handCardPrefab;

	// データ格納してるMasterDataManagerへのアクセス
	List<WordRawData> list;

	// HandCardの位置
	private float handPos_x;
	private float handPos_z = 3f;


	// DeckCardの位置
	private float deckPos_x = -5f;
	private float deckPos_z = -3f;



	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		
		_handCardsController.Initialize ();
		list = MasterDataManager.Instance.wordMasterData._wordRawDataList;

		// HandCard配置
		for (int i = -5; i < 5; i++) {
			GameObject firstDraw = Instantiate (handCardPrefab)as GameObject;
			HandController _handController = firstDraw.GetComponent<HandController> ();
			//int a = Random.Range (0, list.Count); 
				// ↑本当はこれだけど、肝心の歯抜けなので保留。
			int a=Random.Range(0,20);
			// カード情報をもらうdataはHandController内で定義済み
			_handController.data = list[a]; 
			firstDraw.transform.position = new Vector3 (i, 0, handPos_z);
		}

		// DeckCard配置
		GameObject deckSet = Instantiate (deckCardPrefab)as GameObject;
		deckSet.transform.position = new Vector3 (deckPos_x, 0, deckPos_z);

		//comCardIdの初期化
		//comCardId=ComCard(string a,string b);

		Debug.Log (list.Count.ToString());



	}




	// Update is called once per frame
	void Update () {

		// HandCardが減ったのを検出して、カードを送る。
		// ↑これいらんかもなぁ、、、カードが消えるタイミングは、全て把握できるはずで、HandControllerから送られるかも。


	}	

	// HandControllerから呼ばれる、手札合成時にカード作る関数
	public void ComCard (string a, string b, Vector3 c){
		GameObject newCard = Instantiate (handCardPrefab) as GameObject;
		newCard.transform.position = c;
		Debug.Log("ComCard");

		// "a+b+の組み合わせから、MasterDataManagerから合成後のId引っ張ってくる。";
		// newCard.GetComponent<"HandController">().id = ???
	}

	// HandControllerから呼ばれる、Deckアタッチ時にカード作る関数
	// Deckにアタッチした時に起きること、の記述はdeckcontroller内にすると思うので、引数にはカードidも渡す。
	public void ComDeck (string a, Vector3 c){
		GameObject newCard = Instantiate (handCardPrefab) as GameObject;
		newCard.transform.position = c;
		// Randomに、MasterDataManagerから合成後のId引っ張ってくる。
			// ↑これも微妙。アタッチするカードの効果による。
		// newCard.GetComponent<"HandController">().id = Randomなんちゃら?

	}


	// HandControllerから呼ばれる、フィールドアタッチ、オブジェクトアタッチ時にカード作る関数
	// おそらくだが、ComCardと異なり、カードid送り先はフィールド側、オブジェクト側の関数内になるはず
	// なので、ComField()とComObject（）は初期位置しか引数に持たない（はず）。
	public void ComField (Vector3 c){
		GameObject newCard = Instantiate (handCardPrefab) as GameObject;

		newCard.transform.position = c;
		// Randomに、MasterDataManagerから合成後のId引っ張ってくる。;
		// newCard.GetComponent<"HandController">().id = ???

	}
	public void ComObject (Vector3 c){
		GameObject newCard = Instantiate (handCardPrefab) as GameObject;
		newCard.transform.position = c;
		// Randomに、MasterDataManagerから合成後のId引っ張ってくる。;
		// newCard.GetComponent<"HandController">().id = ???

	}



}
