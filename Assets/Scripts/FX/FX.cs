using System.Collections;
using UnityEngine;

public class FX : MonoBehaviour
{
    public FXNames Name;
    public FXSpawnTypes SpawnType;
    public bool Alive;
    public Animator Anim;

    public float AliveTime;

    public void Activate()
    {
        gameObject.SetActive(true);
        Alive = true;
        StartCoroutine(ProcessDeactivate());
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
}