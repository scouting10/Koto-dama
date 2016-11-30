using UnityEngine;
using System.Collections;

/// <summary>
/// ゲームシーンのルートクラス、ゲームシーンに入ったらまず最初に呼ばれるクラス 
/// </summary>
public class GameRootController : MonoBehaviour
{
	[SerializeField]
	private CardGenerator _cardGenerator;
	[SerializeField]
	private DeckController _deckController;


	// Use this for initialization
	private void Start ()
	{
		//マスターデータの初期化
		MasterDataManager.Instance.Initialize ();
		//デッキの初期化
		_cardGenerator.Initialize ();
		_deckController.Initialize();

	}
}
