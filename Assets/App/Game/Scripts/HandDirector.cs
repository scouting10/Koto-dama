using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandDirector: MonoBehaviour {


	[SerializeField]
	private	DeckController _deckController;
	[SerializeField]
	private CardGenerator _cardGenerator;


	// HandCardの位置
	private float handPos_x = 190;
	private float handPos_y = -90;

	// カードデータがきちんと完成したら、これを使う。
	//List<WordRawData> list = MasterDataManager.Instance.wordMasterData._wordRawDataList;

	// 呼び出し元のHandCardとやりとりするための準備
	private GameObject handCard;

	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		
		// 最初のHandCard配置
		for (int i = 0; i < 5; i++) {
			//int a = Random.Range (0, list.Count); 
			// データ完成後はこちら。今は歯抜けなので↓
			int a = Random.Range (0, 20);

			HandController firstDraw = _cardGenerator.Create (a);
			firstDraw.gameObject.transform.localPosition = new Vector3 (handPos_x+100*i, handPos_y,0);
		}


	}
	
	// Update is called once per frame
	void Update () {

		//手札の数が足りてないかどうかのチェックは必要だろうか？
	}

	
	// HandControllerから呼ばれる、手札合成時にカード作る関数
	 /*合成に失敗した時には、comCardにはnew HandController、つまり、初期の、data空っぽのインスタンスが返ってくる。その場合は、comCard (HandController型だが、Generatorが渡してるのはPrefab)壊しておしまい。
	 わかりづらいが、ここでは新しいカードを作っているだけで、合成元のカードには干渉していない。合成元のカードについては、HandController内で処理する。
	 そして、こんなやり方があったとは！ComCardをint型にすることで、呼び出し元のカードに、合成の正否を伝えられる。*/
	public int ComCard (string a, string b, Vector3 c){
		HandController comCard=_cardGenerator.Combination(a,b);
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
}
