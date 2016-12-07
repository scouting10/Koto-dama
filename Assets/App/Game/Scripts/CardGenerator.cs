using UnityEngine;
using System.Collections;
//Listの使用に必要
using System.Collections.Generic;
using System.Linq;

public class CardGenerator : MonoBehaviour {

	// Canvasオブジェクトの取得
	public GameObject canvasObject;

	// Prefabの設定
	public HandController _handController;
	public GameObject _deckPrefab;

	// List変数の宣言
	private List<WordRawData> wordlist;
	private List<CombinationRawData> comlist;

	// Use this for initialization
	public void Initialize () {
		//List変数の初期化
		wordlist = MasterDataManager.Instance.wordMasterData._wordRawDataList;
		comlist = MasterDataManager.Instance.combinationMasterData._combinationRawDataList;



	}

	// Update is called once per frame
	void Update () {

			
	}

	public HandController Create(int index){
		// カード生成
		HandController hand = Instantiate (_handController); 
		//Canvasの子要素として登録する.第一引数が親要素、第二引数が親の座標をもらうか
		hand.transform.SetParent (canvasObject.transform);
		// データの設定
		hand.word_data = wordlist[index];
		// インスタンスを返す
		return hand;

	}

	// デッキカードのいちばん上に、「合成防止の蓋」としてのDeckCardを配置する。
	public GameObject DeckCreate(){
		GameObject deck = Instantiate (_deckPrefab);
		deck.transform.SetParent (canvasObject.transform, false);
		return deck;

	}


	public HandController Combination(string id_a, string id_b){
		for (int i = 0; i < comlist.Count; i++) {
			if (comlist [i].mean == id_a && comlist [i].mean == id_b) { // ここ、コンパイル通すために作ったが、本来は mean ではダメ。
				HandController combination = Instantiate (_handController);
				combination.transform.SetParent (canvasObject.transform, false);
				combination.com_data = comlist [i];
				return combination;
			} 
		}
		HandController misComCard = Instantiate(_handController);
		Debug.Log ("misComCard");
		return misComCard;
	}
}

