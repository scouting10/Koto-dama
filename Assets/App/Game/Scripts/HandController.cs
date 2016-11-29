using UnityEngine;
using System.Collections;

/// <summary>
/// 手札のコントローラークラス 
/// 手札をリストで管理し、不足分のカードをデッキ(DeckController）に要求する役目
/// </summary>
public class HandController : MonoBehaviour
{
	

	// Deckcontrollerからもらうカードデータ
	public WordRawData data;
		// など、など

	// 元に戻る用、次の配置用の、カード初期位置の保存
	private Vector3 startPos;


	// OnTriggerでの判定
	private bool withCard = false;
	private bool withField = false;
	private bool withDeck = false;
	private bool withObject = false;

	// カード合成時
	// OnTriggerからUpdateに渡される、「相手カード」のidと、Destroy用に格納するotherのGameObject
	private string handCardId_b;
	private GameObject otherCard;
	// DeckControllerの合成関数ComCard()を呼ぶためのDeckController変数
	private DeckController deckController;
		


	void Start ()
	{
		//初期データを格納
		startPos = this.transform.position;
		//カードアタッチ時（to カード、フィールド、オブジェクト）に使用
		deckController = GameObject.Find ("DeckController").GetComponent<DeckController>();

		Debug.Log (data.id);
		Debug.Log (data.word);
	}


	void Update() 
	{
		// ↓というわけで、Onmousedragを試してみる。OnMouseDrag()は、Updateの外で記述。

		//指でつままれたら、指の位置に座標を変える。　ここ相当適当に書いてるから、これから直す。
		/*
		if (Input.GetMouseButton (0)) {
			// クリック中の座標を取得。y座標は邪魔なのでカット。しかし、カメラ傾けてたら機能しなさそう、、、
	        Vector3 mousePos_screen = Input.mousePosition;
			Vector3 mousePos_world = Camera.main.ScreenToWorldPoint (mousePos_screen);
			mousePos = new Vector3(mousePos_world.x, 0,mousePos_world.z);

			// ↑というわけで、Rayを使ってみる。が、オブジェクト動かず。
			
			Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//if (hit.collider == this) { このifがあると、コンパイルエラーになる。
				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
					float x = hit.point.x;
					float z = hit.point.z;
					this.transform.position = new Vector3 (x, 0.0f, z);
					Debug.Log ("Hit");
				}

			//}
		} */


		// んで、指を離した時、その座標が「何か」と重なってたら、合成関数を走らせる。
		// 何かと重なってるか否かは、下記のOnTriggerEnter,OnTriggerExitでwith「何か」として検出。
		if (Input.GetMouseButtonUp (0)) {
			if (withCard) {
				string handCardId_a = this.data.id;
				deckController.ComCard(handCardId_a, handCardId_b, startPos);
				Debug.Log ("withCard");
				// withなんたらを初期化する。
				Destroy (otherCard);
				Destroy (this.gameObject);

			} else if (withDeck) {
				string handCardId_a = this.data.id;
				deckController.ComDeck(handCardId_a, startPos);
				withDeck = false;		
				Destroy (this.gameObject);

			} else if (withField) {
				deckController.ComField(startPos);
				withField = false;		
				Destroy (this.gameObject);

			} else if (withObject) {
				deckController.ComObject(startPos);
				Destroy (this.gameObject);
			
			} else {
				// カードを初期位置へ
				// 本当は、動いて戻したい。↓だと、パッと瞬間移動する。
				// 多分、 speedを定義して、毎フレームそれだけ戻るようにするのだが、、、？
				this.transform.position = startPos;
			}
		}
	}

	void OnMouseDrag ()
	{
		Vector3 objectPointInScreen
		= Camera.main.WorldToScreenPoint (this.transform.position);

		//スクリーン座標は、GameViewでのXY軸と考えてみると、ここでｙを動かしてｚを固定する理由がわかる。
		Vector3 mousePointInScreen
		= new Vector3 (Input.mousePosition.x,
			Input.mousePosition.y,
			objectPointInScreen.z);

		Vector3 mousePointInWorld = Camera.main.ScreenToWorldPoint (mousePointInScreen);
		this.transform.position = mousePointInWorld;

	}
	

	void OnTriggerEnter(Collider other){
		// ドラッグを離した時、HandCardと重なっていたら、
		// withCard = trueを返す。それによって、Update()からcardgenerator内の合成関数走らせる。
		if (other.gameObject.tag == "HandCard") {
			withCard = true;
			// Update()内のComCard()の引数。相手カードのidを取得する。
			handCardId_b = this.data.id;
			otherCard = other.gameObject;
			Debug.Log (withCard.ToString());
				

		//その他の条件も記述
		} else if (other.gameObject.tag == "Field") {
			withField = true;
		} else if (other.gameObject.tag == "Deck") {
			withDeck = true;
		} else if (other.gameObject.tag == "Obj") {
			withObject = true;
		}
	}

	//多分、OnTriggerExitで一つずつfalseに直すコードも必要。
	void OnTriggerExit(Collider other){
		// 対象から外れた時、withなんたらをfalseに戻す。
		if(other.gameObject.tag=="HandCard"){
			withCard = false;
			Debug.Log (withCard.ToString());

		//その他の条件も記述
		}else if(other.gameObject.tag == "Field"){
			withField = false;
		}else if(other.gameObject.tag == "Deck"){
			withDeck = false;
		}else if(other.gameObject.tag == "Obj"){
			withObject = false;
		}



	}

}
