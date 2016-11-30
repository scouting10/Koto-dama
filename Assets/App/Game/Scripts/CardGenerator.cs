using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardGenerator : MonoBehaviour {
	
	// Prefabの設定
	public HandController _handController;
	public DeckController _deckController;

	// List変数の宣言
	private List<WordRawData> wordlist;
	private List<CombinationRawData> comlist;

	// Use this for initialization
	public void Initialize () {
		//List変数の初期化
		wordlist = MasterDataManager.Instance.wordMasterData._wordRawDataList;
		comlist = MasterDataManager.Instance.combinationMasterData._combinationRawDataList;

		_deckController.Initialize ();

	}

	// Update is called once per frame
	void Update () {

			
	}

	public HandController Create(int index){
		// カード生成
		HandController hand = Instantiate (_handController); 
		// データの設定
		hand.word_data = wordlist[index];
		// インスタンスを返す
		return hand;

	}

	public DeckController DeckCreate(){
		// カード生成
		DeckController deck = Instantiate (_deckController); 
		// データの設定
		return deck;

	}

	public HandController Combination(string id_a, string id_b){
		for (int i = 0; i < comlist.Count; i++) {
			if (comlist [i].mean == id_a && comlist [i].mean == id_b) { // ここ、コンパイル通すために作ったが、本来は mean ではダメ。
				HandController combination = Instantiate (_handController);
				combination.com_data = comlist [i];
				return combination;
			} 
		}
		return new HandController() ;
	}
}

