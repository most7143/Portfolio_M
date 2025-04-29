using UnityEngine;
using UnityEngine.UI;

public class ImageLoop : MonoBehaviour
{
    public Image TargetImage;
    public Sprite[] Sprites;
    public float Speed = 5f;

    private int currentIndex = 0;
    private float timer = 0f;

    private void Update()
    {
        if (Sprites == null || Sprites.Length == 0 || Sprites == null)
            return;

        timer += Time.deltaTime;

        float frameDuration = 1f / Speed;

        if (timer >= frameDuration)
        {
            timer -= frameDuration;
            currentIndex = (currentIndex + 1) % Sprites.Length;
            TargetImage.sprite = Sprites[currentIndex];
        }
    }
}