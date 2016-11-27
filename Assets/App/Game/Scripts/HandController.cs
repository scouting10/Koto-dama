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

	//カード離した先での判定Bool
	private bool withCard = false;
	private bool withField = false;
	private bool withDeck = false;
	private bool withObject = false;

	//カード合成時、OnTriggerからUpdateに渡される、「相手カード」のid
	private string handCardId_b;

	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		

		
	}


	/*void Update() 
	{
		//指でつままれたら、指の位置に座標を変える。
		if (Input.GetkeyStay (0) && tag == "HandCard") {
			this.transfrom.position = new Vector3 (指のいちx, 0, 指のいちｚ);
		}	
		
		// んで、指を離した時、その座標がカードと重なってたら、合成関数を走らせる。
		// カードと重なってるか否かは、下記のOnTriggerでwithCatdとして検出。
		if (Input.GetKeyUp (0) && withCard ==true) {
			string handCardId_a = this.id;
			CardGenerator内の合成関数（handCardId_a,handardId_b）;
			// で、多分ここでDestroy? 相手カードってどうやって壊すん？？
			Destroy.this;
		}
	}
		

	

	void OnTriggerEnter(Collider other){
		// ドラッグを離した時、HandCardと重なっていたら、
		// withCard = trueを返す。それによって、Update()からcardgenerator内の合成関数走らせる。
		if(other.tag == HandCard){
			withCard = true;
			//Update()で相手のidを使う為に格納する。
			handCardId_b = this.id;

		//その他の条件も記述
		}else if(other.tag == Field){
			withField = true;
		}else if(other.tag == Deck){
			withDeck = true;
		}else if(other.tag == Object){
			withObject = true;
		}

	//多分、OnTriggerExitで一つずつfalseに直すコードも必要。
	void OnTriggerExit(Collider other){
		// 対象から外れた時、withなんたらをfalseに戻す。
		if(other.tag==HandCard){
			withCard = false;
		//その他の条件も記述
		}else if(other.tag == Field){
			withField = false;
		}else if(other.tag == Deck){
			withDeck = false;
		}else if(other.tag == Object){
			withObject = true;
		}



	}*/
	
}
