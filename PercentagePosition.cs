// Percentage Position for Unity2D
// Sets/Gets position relative by percentage according to screen width/height
// Original Author: MohHeader
// Modified by: ahmed-shehata

using UnityEngine;
using System.Collections;

public class PercentagePosition : MonoBehaviour {
	public float VerticalPercent;
	public float HorizontalPercent;

	public Horizontal horizontal;
	public Vertical vertical;
	private float CameraAspectRatio = 0;

	// x
	public enum Horizontal{
		Right,
		Center,
		Left
	}

	// y
	public enum Vertical{
		Top,
		Center,
		Bottom
	}
	void Awake(){
		CameraAspectRatio = Camera.main.aspect;
		RePosition ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.update += UpdateOnChangeAspectRatio;
		#endif
	}


	void UpdateOnChangeAspectRatio(){
		if (CameraAspectRatio != Camera.main.aspect) {
			CameraAspectRatio = Camera.main.aspect;
			RePosition ();
		}
	}

	[ContextMenu("RePosition")]
	public void RePosition(){
		Vector2 SpriteSize = new Vector2(0,0);
		 
		float cheight = 2f * Camera.main.orthographicSize;
		float cwidth = cheight * Camera.main.aspect;

 

		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite != null) {
			SpriteSize.x = sprite.bounds.size.x / transform.localScale.x;
			SpriteSize.y = sprite.bounds.size.y / transform.localScale.y;
		}

		Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth
			, Camera.main.pixelHeight
			, 0));
		if (transform != null) {
			float x = transform.position.x;
			float y = transform.position.y;

			switch (horizontal) {
			case Horizontal.Right:
				x = world.x   - HorizontalPercent*cwidth;
				break;
			case Horizontal.Left:
				x = - world.x  + HorizontalPercent*cwidth;
				break;
			case Horizontal.Center:
				x =  HorizontalPercent*cwidth;
				break;
			default:
				break;
			}

			switch (vertical) {
			case Vertical.Top:
				y = world.y   - VerticalPercent*cheight;
				break;
			case Vertical.Bottom:
				y = - world.y   + VerticalPercent*cheight;
				break;
			case Vertical.Center:
				y = VerticalPercent*cheight;
				break;
			default:
				break;
			}

			transform.position = new Vector3 (x, y, transform.position.z);

		}

	}



	[ContextMenu("GetPositionFromCurrent")]
	public void GetPositionFromCurrent(){

		Vector2 SpriteSize = new Vector2(0,0);

		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite != null) {
			SpriteSize.x = sprite.bounds.size.x / transform.localScale.x;
			SpriteSize.y = sprite.bounds.size.y / transform.localScale.y;
		}

		Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth
			, Camera.main.pixelHeight
			, 0));

		float x = transform.position.x;
		float y = transform.position.y;
		float cheight = 2f * Camera.main.orthographicSize;
		float cwidth = cheight * Camera.main.aspect;

		switch (horizontal) {
		case Horizontal.Right:
			HorizontalPercent = (world.x  - x)/cwidth;
			break;
		case Horizontal.Left:
			HorizontalPercent = (world.x  + x)/cwidth;
			break;
		case Horizontal.Center:
			HorizontalPercent = x/cwidth;
			break;
		default:
			break;
		}

		switch (vertical) {
		case Vertical.Top:
			VerticalPercent = (world.y  - y)/cheight;
			break;
		case Vertical.Bottom:
			VerticalPercent = (world.y  + y)/cheight;
			break;
		case Vertical.Center:
			VerticalPercent = (y)/cheight ;
			break;
		default:
			break;
		}
	}



}
