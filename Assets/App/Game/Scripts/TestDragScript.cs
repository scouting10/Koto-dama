using UnityEngine;
using UnityEngine.EventSystems;

public class TestDragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
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
