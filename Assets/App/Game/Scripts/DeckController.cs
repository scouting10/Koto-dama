using UnityEngine;
using System.Collections;

/// <summary>
/// デッキのコントローラークラス
/// デッキに含まれるカードのリストを保持したり、手札(HandController）にカードを提供する役目を担う 
/// </summary>
public class DeckController : MonoBehaviour
{
	[SerializeField]
	private HandController _handController;


	// Prefabの設定
	public GameObject deckCardPrefab;
	public GameObject handCardPrefab;

	/// masterdatamanagerのスクリプトをここに埋め込む？
	/// public MasterDataManager _masterDataManager;
	/// 

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
		

		// HandCard配置
		for (int i = -5; i < -2; i++) {
			GameObject firstDraw = Instantiate (handCardPrefab)as GameObject;
			// ↓多分ここにランダムにmasterdatamanagerからの

			// ↑カード情報をもらうコードが必要
			firstDraw.transform.position = new Vector3 (i, 0, handPos_z);
		}

		// DeckCard配置
		GameObject deckSet = Instantiate (deckCardPrefab)as GameObject;
		deckSet.transform.position = new Vector3 (deckPos_x, 0, deckPos_z);

		//comCardIdの初期化
		//comCardId=ComCard(string a,string b);

		Debug.Log (MasterDataManager.Instance.wordMasterData._wordRawDataList[0].id);


		// 手札の初期化　　　↓　これって、それぞれのPrefabできちんと仕事すんの？
		// 下で新しいカードを生み出すたびに、Initialize()は必要。現在未実装
		_handController.Initialize ();	



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
		Initialize ();
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
