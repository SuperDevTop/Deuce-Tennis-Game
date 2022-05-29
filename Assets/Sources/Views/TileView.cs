using UnityEngine;
using System.Collections;

public class TileView : MonoBehaviour 
{
    public UISprite TileBG;
    public UISprite FieldBG;
    public UISprite Selectable;
    public UISprite Strikable;
    public UILabel DiceNumber;
    public int TileX;
    public int TileY;
    public bool IsCollideWithFirstBounceLine;
    public bool IsCollideWithSecondBounceLine;
    public bool IsCollideWithSecondBounce;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int GetWidth()
    {
        return TileBG.width;
    }

    public int GetHeight()
    {
        return TileBG.height;
    }

    public void SetWidth(int width)
    {
        TileBG.width = width;
        FieldBG.ResetAndUpdateAnchors();
        Selectable.ResetAndUpdateAnchors();
        DiceNumber.ResetAndUpdateAnchors();
    }

    public void SetHeight(int height)
    {
        TileBG.height = height;
        FieldBG.ResetAndUpdateAnchors();
        Selectable.ResetAndUpdateAnchors();
        DiceNumber.ResetAndUpdateAnchors();
    }

    public void SetSize(int width, int height)
    {
        TileBG.width = width;
        TileBG.height = height;
        FieldBG.ResetAndUpdateAnchors();
        Selectable.ResetAndUpdateAnchors();
        DiceNumber.ResetAndUpdateAnchors();
    }

    public Vector2 GetPosition()
    {
        return new Vector2(TileX, TileY);
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if( collision.gameObject.name == "BounceLine" )
    //    {
    //        IsCollideWithBounceLine = true;
    //    }

    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
            return;
        //Debug.Log("Trigger Collide of Tile[" + TileX + "][" + TileY + "] with " + other.gameObject.name);
        if (other.gameObject.name == "SecondBounceLine")
        {
            IsCollideWithSecondBounceLine = true;
        }
        if (other.gameObject.name == "FirstBounceLine")
        {
            IsCollideWithFirstBounceLine = true;
        }
        if (other.gameObject.name == "SecondBounce")
        {
            IsCollideWithSecondBounce = true;
        }
    }
}
