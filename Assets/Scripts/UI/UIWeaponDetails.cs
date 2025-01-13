using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponDetails : MonoBehaviour
{
    public CanvasGroup Group;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    public Image WeaponIcon;

    public float AliveTime = 3f;
    private float fadeDuration = 0.5f;

    public void Refresh(WeaponNames weaponName)
    {
        WeaponData weaponData = Resources.Load<WeaponData>("ScriptableObject/Weapon/" + weaponName.ToString());
        if (weaponData != null)
        {
            NameText.SetText(weaponData.NameString);
            DescText.SetText(weaponData.DescString);
            WeaponIcon.sprite = weaponData.Icon;
        }

        StartCoroutine(StartShow());
    }

    private IEnumerator StartShow()
    {
        Group.blocksRaycasts = true;
        FadeIn();
        yield return new WaitForSeconds(AliveTime);
        FadeOut();
        Group.blocksRaycasts = false;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(Group, Group.alpha, 1));
    }

    // ���̵� �ƿ� ����
    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(Group, Group.alpha, 0));
    }

    // ���̵� ȿ���� ó���ϴ� �ڷ�ƾ
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cg.alpha = endAlpha; // �������� ��Ȯ�� alpha �� ����
    }
}