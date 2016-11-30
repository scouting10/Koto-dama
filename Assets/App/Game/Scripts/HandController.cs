using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// 手札のコントローラークラス 
/// 手札をリストで管理し、不足分のカードをデッキ(DeckController）に要求する役目
/// </summary>
public class HandController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{



	// CardGeneratorからもらうカードデータ。合成カードのリストはCombinationRawData型なので、別の変数com_dataとして区別必要だった。
	public WordRawData word_data;
	public CombinationRawData com_data;


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
	// HandCardsDirectorの合成関数ComCard()を呼ぶためのDeckController変数
	private HandCardsDirector _handCardsDirector;

	// デッキへカードアタッチ時に使う
	private DeckController _deckController;


	void Start ()
	{
		//初期データを格納
		startPos = this.transform.position;

		//カードアタッチ時（to カード、フィールド、オブジェクト）に使用
			//この辺、もうインスペクタでやっちゃってもいいのかな、、、？
		_handCardsDirector = GameObject.Find ("HandCardsDirector").GetComponent<HandCardsDirector>();
		_deckController = GameObject.Find ("DeckController").GetComponent<DeckController> ();

	}


	void Update() 
	{
		
	// 直下のコメントアウトについて。試行錯誤した結果、Updateの外の、OnMouseDragにたどり着いた。
		// この試みでオブジェクトが動かなかった理由の一つを、OnMouseDrag内のコメントアウトに記載

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

			}
		} */


		// 指を離した時、その座標が「何か」と重なってたら、合成関数を走らせる。
		// 何かと重なってるか否かは、下記のOnTriggerEnter,OnTriggerExitで(with「何か=true/false)として検出。
		if (Input.GetMouseButtonUp (0)) {
			if (withCard) {
				// まず、関数の引数になるこのカードidを変数に格納
				string handCardId_a = this.word_data.id;
				// HandCardsDirector内のComCard()をint型の関数にしてやることで、返り値から条件判定ができる。
				int comCard = _handCardsDirector.ComCard(handCardId_a, handCardId_b, startPos);
				if (comCard == -1) {
					Destroy (otherCard);
					Destroy (this.gameObject);
				}
				// withCard = false;  ← いらない。どの(with~)の条件にも当てはまらなければ、startPosに戻る。自動的に、OnTriggerExitでfalseかかる。
				this.transform.position = startPos;


			} else if (withDeck) {
				string handCardId_a = this.word_data.id;
				// このカードは最終的にDestroyされるので、新しいカード要求。新規カードの中身は（今の所）ランダムで良い。だから、引数はstartPosのみ。
				_handCardsDirector.ComDeck(startPos);
				// 内容未定だが、実際にDeckとの合成で起きることは、DeckController内に記述。
				_deckController.ComDeck(handCardId_a);

				//withDeck = false;		
				Destroy (this.gameObject);

			} else if (withField) {
				_handCardsDirector.ComField(startPos);
				//withField = false;		
				Destroy (this.gameObject);

			} else if (withObject) {
				_handCardsDirector.ComObject(startPos);
				Destroy (this.gameObject);
			
			} else {
				// なんの条件にも当てはまらなければ、カードを初期位置へ
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

		//スクリーン座標は、GameViewでのXY軸しかないと考える。つまり、（このゲームにおいて）World座標系のｚ軸はScreen座標系のｙである。ここでｙを動かしてｚを固定する理由がわかる。
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
			handCardId_b = this.word_data.id;
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


	// ドラックが開始したとき呼ばれる.
	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log(1);

	}

	// ドラック中に呼ばれる.
	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log(2);
	}

	// ドラックが終了したとき呼ばれる.
	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log(3);
	}


}
