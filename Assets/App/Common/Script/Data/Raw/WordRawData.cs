using UnityEngine;
using System.Collections;

/// <summary>
/// 単語1つ文の生データ 
/// </summary>
[System.Serializable]//シリアライズ化(インスペクタで内容が確認できる)
public class WordRawData
{
	[SerializeField]
	public string id;
	[SerializeField]
	public string word;
	[SerializeField]
	public string variant1;
	[SerializeField]
	public string variant2;
	[SerializeField]
	public string variant3;
	[SerializeField]
	public string variant4;
	[SerializeField]
	public string origin1;
	[SerializeField]
	public string origin2;
	[SerializeField]
	public string wordType;
	[SerializeField]
	public string mean;
	[SerializeField]
	public string derivative1;
	[SerializeField]
	public string derivative2;
	[SerializeField]
	public string derivative3;
	[SerializeField]
	public string derivative4;
}