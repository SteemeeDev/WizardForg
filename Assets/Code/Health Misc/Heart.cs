using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public bool alive = true;
    [SerializeField] Sprite heartImage;
    [SerializeField] Sprite deadImage;

    [SerializeField] Image sprite;


    private void Update()
    {
        if (alive == false && sprite.sprite == heartImage)
        {
            sprite.sprite = deadImage;
        }
    }
}
