using UnityEngine;
using System.Collections;

public class CardGenerator : MonoBehaviour {

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

	// Use this for initialization
	void Start () {

		// HandCard配置
		for (int i = -5; i < -2; i++) {
			GameObject fstdraw = Instantiate (handCardPrefab)as GameObject;
			// ↓多分ここにランダムにmasterdatamanagerからの

			// ↑カード情報をもらうコードが必要
			fstdraw.transform.position = new Vector3 (i, 0, handPos_z);
		}

		// DeacCard配置
		GameObject deckSet = Instantiate (deckCardPrefab)as GameObject;
		deckSet.transform.position = new Vector3 (deckPos_x, 0, deckPos_z);
	
	}
	
	// Update is called once per frame
	void Update () {

		// HandCardが減ったのを検出して、カードを送る。

		// HandCardControllerから呼ばれる、合成時にカード作る関数
		// _cardRawDataJsonの中に、id組み合わせのデータが必要。
		Fusion(id a, id b){
			
			
		}
	
	}

}
