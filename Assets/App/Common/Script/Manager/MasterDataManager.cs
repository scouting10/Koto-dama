using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ゲーム部分のデータを管理するクラス 
/// </summary>
public class MasterDataManager : SingletonMonoBehaviour<MasterDataManager>
{
	[SerializeField]
	private TextAsset _wordMasterJson;
	[SerializeField]
	private TextAsset _combinationMasterJson;
	[SerializeField]
	private WordMasterData _wordMasterData;
	[SerializeField]
	private CombinationMasterData _combinationMasterData;

	/// <summary>
	/// Awake関数(Start関数の前に呼ばれるUnity側で用意された関数)
	/// </summary>
	private void Awake ()
	{
		//シーンを跨いでも削除されないための記述
		DontDestroyOnLoad (gameObject);
	}

	/// <summary>
	/// 初期化処理を行う関数 
	/// </summary>
	public void Initialize ()
	{
		//JSONテキストパース
		_wordMasterData = JsonUtility.FromJson <WordMasterData> (_wordMasterJson.text);
		_combinationMasterData = JsonUtility.FromJson<CombinationMasterData> (_combinationMasterJson.text);
	}
}
