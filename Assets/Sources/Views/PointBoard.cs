using UnityEngine;
using System.Collections;

public class PointBoard : MonoBehaviour 
{
    public UISprite BoardBG;
    public UILabel PointLabel;

    public int GetBoardWidth()
    {
        return BoardBG.width;
    }

    public void SetPoint(string point)
    {
        PointLabel.text = point;
    }

}
