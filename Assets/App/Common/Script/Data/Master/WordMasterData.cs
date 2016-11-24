using UnityEngine;
using System.Collections;
//Listの使用に必要
using System.Collections.Generic;

[System.Serializable]
public class WordMasterData
{
	[SerializeField]
	//動的配列としてListを宣言
	//例えば int 型の要素を持つ動的配列は以下のように宣言する。
	// List<int> v;
	public List<WordRawData> _wordRawDataList;

}


