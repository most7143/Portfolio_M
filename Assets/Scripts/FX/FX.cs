using System.Collections;
using UnityEngine;

public class FX : MonoBehaviour
{
    public FXNames Name;
    public string NameString;
    public FXSpawnTypes SpawnType;
    public SpriteRenderer Renderer;

    public bool isRandomFlip;
    public bool Alive;
    public Animator Anim;

    public float AliveTime;

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<FXNames>(NameString);
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        Alive = true;
        StartCoroutine(ProcessDeactivate());

        if (isRandomFlip)
        {
            Flip();
        }
    }

    public void Deactivate()
    {
        Alive = false;
        gameObject.SetActive(false);
    }

    private IEnumerator ProcessDeactivate()
    {
        yield return new WaitForSeconds(AliveTime);
        Deactivate();
    }

    private void Flip()
    {
        int Range = Random.Range(0, 2);

        if (Range == 0)
        {
            Renderer.flipX = true;
        }
        else
        {
            Renderer.flipX = false;
        }
    }
}