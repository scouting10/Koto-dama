using UnityEngine;
using System.Collections;

/// <summary>
/// 手札のコントローラークラス 
/// 手札をリストで管理し、不足分のカードをデッキ(DeckController）に要求する役目
/// </summary>
public class HandController : MonoBehaviour
{
	[SerializeField]
	private	DeckController _deckController;
		// ↑んー？これ今のままじゃ使えんな。

	// Deckcontrollerからもらうカードデータ
	public string id;
		// など、など

	// 元に戻る用、次の配置用の、カード初期位置の保存
	private Vector3 startPos = new Vector3();



	// OnTriggerでの判定
	private bool withCard = false;
	private bool withField = false;
	private bool withDeck = false;
	private bool withObject = false;

	// カード合成時
		// OnTriggerからUpdateに渡される、「相手カード」のid
	private string handCardId_b;
		//DeckControllerの合成関数ComCard()を呼ぶためのDeckController変数
	private DeckController deckController;
		/*↑こんなんせんでも、せっかくSerializeで設定した変数があった。
			↑現状使えない。高取先生に質問。*/


	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		//初期データを格納
		startPos = this.transform.position;
		//id = もらえるカードid

		//カードアタッチ時（to カード、フィールド、オブジェクト）に使用
		deckController = GameObject.Find ("DeckController").GetComponent<DeckController>();
			/*↑こんなんせんでも、Serializeで_deckControllerが設定されてる。
			_deckController使った瞬間、下のComCard()とかの赤字が一瞬で黒字化した。
				↑現状使えない。高取先生に質問。*/

		
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
				string handCardId_a = this.id;
				deckController.ComCard(handCardId_a, handCardId_b, startPos);
				Debug.Log ("withCard");
				// withなんたらを初期化する。
				withCard = false;		
				Destroy (this.gameObject);

			} else if (withDeck) {
				string handCardId_a = this.id;
				deckController.ComDeck(handCardId_a, startPos);
				withDeck = false;		
				Destroy (this.gameObject);

			} else if (withField) {
				deckController.ComField(startPos);
				withField = false;		
				Destroy (this.gameObject);

			} else if (withObject) {
				deckController.ComObject(startPos);
				withObject = false;		
				Destroy (this.gameObject);
			
			} else {
				// カードを初期位置へ
				// 本当は、動いて戻したい。↓だと、パッと瞬間移動する。
				// 多分、 speedを定義して、毎フレームそれだけ戻るようにするのだが、、、？
				this.transform.position = startPos;
			}
		}
	}

	// ↓保留。最終的にこの形で同じ（バグった）挙動する。
	void OnMouseDrag(){
		/*
		Vector3 objectPointInScreen
		= Camera.main.WorldToScreenPoint(this.transform.position);
		*/
		// mousePositionは、Screen座標系
		Vector3 mousePointInScreen = Input.mousePosition;
		/* ↑のように、まとめてはいけないのか？
		= new Vector3(Input.mousePosition.x,
			// この処理で、動く予定のないy座標はobjectとmouseで合わせてしまう。
			objectPointInScreen.y,
			Input.mousePosition.z);
		*/

		// y座標の処理を終えたmousePointInScreenを、World座標に戻し、objectのtransform(World座標）に代入。
		Vector3 mousePointInWorld = Camera.main.ScreenToWorldPoint(mousePointInScreen);
		mousePointInWorld.y = this.transform.position.y;
		this.transform.position = mousePointInWorld;
	

	}
		

	
	/*
	void OnTriggerEnter(Collider other){
		// ドラッグを離した時、HandCardと重なっていたら、
		// withCard = trueを返す。それによって、Update()からcardgenerator内の合成関数走らせる。
		if (other.gameObject.tag == "HandCard") {
				withCard = true;
			// Update()内のComCard()の引数。相手カードのidを取得する。
				handCardId_b = this.id;
				Destroy(other.gameObject);

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
		//その他の条件も記述
		}else if(other.gameObject.tag == "Field"){
			withField = false;
		}else if(other.gameObject.tag == "Deck"){
			withDeck = false;
		}else if(other.gameObject.tag == "Obj"){
			withObject = false;
		}



	}
	*/
}
