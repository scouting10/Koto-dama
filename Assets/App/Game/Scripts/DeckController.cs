﻿using UnityEngine;
using System.Collections;

/// <summary>
/// デッキのコントローラークラス
/// デッキに含まれるカードのリストを保持したり、手札(HandController）にカードを提供する役目を担う 
/// </summary>
public class DeckController : MonoBehaviour
{
	[SerializeField]
	private HandController _handController;

	public GameObject deckCardPrefab;



	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		//手札の初期化
		_handController.Initialize ();	

		GameObject setUp = Instantiate (deckCardPrefab)as GameObject;
		setUp.transform.position = new Vector3 (-5, 0, -3);
	}
}
