using UnityEngine;
using System.Collections;

/// <summary>
/// コンビネーションの生データ 
/// </summary>
[System.Serializable]//シリアライズ化(インスペクタで内容が確認できる)
public class CombinationRawData
{
	[SerializeField]
	public string id;
	[SerializeField]
	public string word;
	[SerializeField]
	public string prefix1;
	[SerializeField]
	public string prefix2;
	[SerializeField]
	public string latin;
	[SerializeField]
	public string stem;
	[SerializeField]
	public string suffix;
	[SerializeField]
	public string ancient;
	[SerializeField]
	public string mean;
	[SerializeField]
	public string significance;
}
