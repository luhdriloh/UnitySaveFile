using UnityEngine;

public enum FlowerColor
{
    BLUE,
    RED,
    YELLOW
};

public class Flower : MonoBehaviour
{
    public FlowerColor _color;
    public int _numberOfFlowers;

    private static float _defaultZ = -3;

    private void Awake()
    {
        float yPos = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y, _defaultZ + (yPos * .01f));
    }

    public void PickFlower()
    {
        Destroy(gameObject);
    }
}
