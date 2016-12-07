using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// 手札のコントローラークラス 
/// 手札をリストで管理し、不足分のカードをデッキ(DeckController）に要求する役目
/// </summary>
public class HandController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{



	// CardGeneratorからもらうカードデータ。合成カードのリストはCombinationRawData型なので、別の変数com_dataとして区別必要だった。
	public WordRawData word_data;
	public CombinationRawData com_data;
	// HandDirectorの合成関数ComCard()を呼ぶためのDeckController変数
	public HandDirector _handDirector;
	// デッキへカードアタッチ時に使う
	public DeckController _deckController;

	/*
	// 自分のRectTransformへのパス,初期位置情報を格納
	private RectTransform firstRect; //どうやら、RectTransformは関数ちっくで、firstRectの中に、Start（）のfirstRect.ancoredPositionのデータも格納してる。
	// ↑　と言うわけで、初期位置以外の用途に使うRectTransformは別に用意する。
	private RectTransform rect;
	*/

	// ↑なんかうまくいかんかったから普通でやったらできた。しかし、HandDirectorの初期カード配置では、Rectをいじらないとどうしようもなかったが？？？
	public Vector3 startPos;

	// タグとは異なる、HandDirectorのhandList内での自分の番号
	public int number;

	// OnDrag周りの判定
	// 重なってる何かをboolで。
	private bool withCard = false;
	private bool withField = false;
	private bool withDeck = false;
	private bool withObject = false;
	// Dragされているかいないか
	private bool onDragging = false;

	// カード合成時
	// OnTriggerからUpdateに渡される、「相手カード」のidと、Destroy用に格納するotherのGameObject
	private string handCardId_b;






	void Start ()
	{
		_handDirector = GameObject.Find ("HandDirector").GetComponent<HandDirector> ();
		_deckController = GameObject.Find ("DeckController").GetComponent<DeckController> ();

		/*
		// 初期位置データを格納
		firstRect=GetComponent<RectTransform>();
		firstRect.anchoredPosition = this.gameObject.GetComponent<RectTransform> ().anchoredPosition;
		// その他用途のRectも作っておく。
		rect = GetComponent<RectTransform>();
		*/

		//上で失敗。最初に戻す。
		startPos = transform.position;

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

		/* 
		↓3Dで作っていた頃の名残。ここの部分と、OnTrigger関係が、全てOnDragの中に入った。

		// 指を離した時、その座標が「何か」と重なってたら、合成関数を走らせる。
		// 何かと重なってるか否かは、下記のOnTriggerEnter,OnTriggerExitで(with「何か=true/false)として検出。
		if (Input.GetMouseButtonUp (0)) {
			if (withCard) {
				// まず、関数の引数になるこのカードidを変数に格納
				string handCardId_a = this.word_data.id;
				// HandDirector内のComCard()をint型の関数にしてやることで、返り値から条件判定ができる。
				int comCard = _handDirector.ComCard(handCardId_a, handCardId_b, startPos);
				if (comCard == -1) {
					Destroy (otherCard);
					Destroy (this.gameObject);
				}
				// withCard = false;  ← いらない。どの(with~)の条件にも当てはまらなければ、startPosに戻る。自動的に、OnTriggerExitでfalseかかる。

				transform.position = startPos;

			// とりあえず、withcard以外のRectTransformについては放置
			} else if (withDeck) {
				string handCardId_a = this.word_data.id;
				// このカードは最終的にDestroyされるので、新しいカード要求。新規カードの中身は（今の所）ランダムで良い。だから、引数はstartPosのみ。
				_handDirector.ComDeck(startPos);
				// 内容未定だが、実際にDeckとの合成で起きることは、DeckController内に記述。
				_deckController.ComDeck(handCardId_a);

				//withDeck = false;		
				Destroy (this.gameObject);

			} else if (withField) {
				//_handDirector.ComField(startPos);
				//withField = false;		
				Destroy (this.gameObject);

			} else if (withObject) {
				//_handDirector.ComObject(startPos);
				Destroy (this.gameObject);
			
			} else {
				// なんの条件にも当てはまらなければ、カードを初期位置へ
					// 本当は、動いて戻したい。↓だと、パッと瞬間移動する。
					// 多分、 speedを定義して、毎フレームそれだけ戻るようにするのだが、、、？
				transform.position = startPos;
			}
		}
		*/
	}

	/* カードを3Dオブジェクトで動かそうとした時のドラッグ処理。2Dにすると、anchoredPositionになるので？反応しなくなる？
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
	そこで、つかうのがOnBeginDrag。しかし、結局問題は中身のような？
	参考：http://qiita.com/ayumegu/items/c07594f408363f73008c */




	// ドラックが開始したとき呼ばれる.
	// 以下、ドロップされた時の関数も同様だが、ドラッグされている時にはドロップ判定をオフにする。

	public void OnPointerDown(PointerEventData eventData)
	{
		_handDirector.OnPointerDown (this);

	}

	// ドラック中に呼ばれる.
	public void OnDrag(PointerEventData eventData)
	{
		
		this.transform.position = eventData.position;
		// _handDirector.OnPointerDown (this);  値を入れるのは、一箇所（今回なら、Down）だけ。バグが起きた時に原因箇所が特定しづらい。

	}

	// ドラックが終了したとき呼ばれる.
	public void OnPointerUp(PointerEventData eventData)
	{
		_handDirector.OnPointerUp ();

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_handDirector.OnPointerEnter (this); // ポインターを合わされた側で呼ばれる関数なので、thisで良い。

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_handDirector.OnPointerExit (this);

	}


/* HandDirector側で、OnPointerUpを実装する前の試行錯誤の後。

 
		// ドラッグを離した時、HandCardと重なっていたら、
		// withCard = trueを返す。それによって、Update()からcardgenerator内の合成関数走らせる。
		if (eventData.pointerEnter.tag == "HandCard") { //pointerEnterはOnPointerEnterが記述されてるオブジェクトが入る
			withCard = true;
			// handCardId_b = eventData.pointerEnter.GetComponent<HandController>().word_data.id; //pointerEnter.word_data.idではだめ
				// ^ Get and Set Id_b in OnPointerEnter method because this code cause self-Combination when a card distached somewhere.

			// カード合成、また、合成の成否判定をHandDirectorから返してもらう。
			int comCard = _handDirector.ComCard();




			//その他の条件も記述 フィールド、他のオブジェクトの情報の取り方は保留。
		//} else if (other.gameObject.tag == "Field") {
		//	withField = true;
		} else if (eventData.pointerEnter.tag == "DeckCard") {
			withDeck = true;
		//} else if (other.gameObject.tag == "Obj") {
		//	withObject = true;
		}

		if (withCard) {
			_handDirector.ComCard ();
																下のことが、OnBeginDrag(this)で全部の情報をHandDirectorに送って解決した。
																関数の引数になるこのカードidを変数に格納
																string handCardId_a = this.word_data.id;
																// HandDirctor内にidを送る。　// HandDirector内のComCard()をint型の関数にしてやることで、返り値から条件判定ができる。
																_handDirector.ComCard1(handCardId_a);
																 
																				元々は、これでHandDirectorにカードIDを送っていた。int型なのは、合成の成否を数字で返してもらって判定していたため。　int comCard = _handDirector.ComCard(handCardId_a, handCardId_b, startPos);
																											if (comCard == -1) {
																																			Destroy (eventData.pointerEnter.gameObject);
																																			Destroy (this.gameObject);
																											}
																											// withCard = false;  ← いらない。どの(with~)の条件にも当てはまらなければ、startPosに戻る。自動的に、OnTriggerExitでfalseかかる。

																											// transform.position = startPos; ここでDestroy処理もしていた頃の名残。

			// とりあえず、withcard以外のRectTransformについては放置
		} else if (withDeck) {
			string handCardId_a = this.word_data.id;
			// このカードは最終的にDestroyされるので、新しいカード要求。新規カードの中身は（今の所）ランダムで良い。だから、引数はstartPosのみ。
			_handDirector.ComDeck(startPos);
			// 内容未定だが、実際にDeckとの合成で起きることは、DeckController内に記述。
			_deckController.ComDeck(handCardId_a);

			//withDeck = false;		
			Destroy (this.gameObject);

		} else if (withField) {
			//_handDirector.ComField(startPos);
			//withField = false;		
			Destroy (this.gameObject);

		} else if (withObject) {
			//_handDirector.ComObject(startPos);
			Destroy (this.gameObject);

		} else  {
			// なんの条件にも当てはまらなければ、カードを初期位置へ
			// 本当は、動いて戻したい。↓だと、パッと瞬間移動する。
			// 多分、 speedを定義して、毎フレームそれだけ戻るようにするのだが、、、？
			transform.position = startPos;
		}
*/

		



	// マウスオーバー、もしくは、ドロップされた時に呼ばれる関数たち
	public void OnPointerEnter(PointerEventData eventData)
	{
		_handDirector.OnPointerEnter (eventData.pointerEnter.gameObject.GetComponent<HandController>());
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_handDirector.OnPointerExit ();

	}
	public void OnDrop(PointerEventData eventData)
	{
	
	}


	

	/* 3Dでカード作っていた時の名残。Imageになると、Colliderとかがない。
	void OnTriggerEnter(Collider other){
		// ドラッグを離した時、HandCardと重なっていたら、
		// withCard = trueを返す。それによって、Update()からcardgenerator内の合成関数走らせる。
		if (other.gameObject.tag == "HandCard") {
			withCard = true;
			// Update()内のComCard()の引数。相手カードのidを取得する。
			handCardId_b = this.word_data.id;
			otherCard = other.gameObject;
				

		//その他の条件も記述
		} else if (other.gameObject.tag == "Field") {
			withField = true;
		} else if (other.gameObject.tag == "DeckCard") {
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
		}else if(other.gameObject.tag == "DeckCard"){
			withDeck = false;
		}else if(other.gameObject.tag == "Obj"){
			withObject = false;
		}



	}
	*/




}
