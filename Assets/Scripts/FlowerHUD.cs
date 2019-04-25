using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerHUD : MonoBehaviour
{
    public Text _redFlowerCount;
    public Text _blueFlowerCount;
    public Text _yellowFlowerCount;


    private void Start()
    {
        Farmer.AddFlowerEventObserver(FlowerCountUpdate);
    }

    private void FlowerCountUpdate(Dictionary<FlowerColor, int> flowerValues)
    {
        foreach (FlowerColor color in flowerValues.Keys)
        {
            switch (color)
            { 
                case FlowerColor.RED:
                    _redFlowerCount.text = flowerValues[color].ToString();
                    break;
                case FlowerColor.BLUE:
                    _blueFlowerCount.text = flowerValues[color].ToString();
                    break;
                case FlowerColor.YELLOW:
                    _yellowFlowerCount.text = flowerValues[color].ToString();
                    break;
            }
        }
    }
}
