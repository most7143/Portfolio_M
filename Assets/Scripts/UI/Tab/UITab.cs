using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITab : MonoBehaviour, IPointerClickHandler
{
    public UITabController Controller;

    public CanvasGroup CanvasGroup;

    public Image Image;

    public int Index;

    public virtual void Activate()
    {
        Controller.SelectedIndex = Index;
        Image.sprite = ResourcesManager.Instance.LoadSprite("TabBackground_Click");
        if (CanvasGroup != null)
        {
            CanvasGroup.alpha = 1;
            CanvasGroup.blocksRaycasts = true;
        }
    }

    public virtual void Deactivate()
    {
        Image.sprite = ResourcesManager.Instance.LoadSprite("TabBackground_Normal");
        if (CanvasGroup != null)
        {
            CanvasGroup.alpha = 0;
            CanvasGroup.blocksRaycasts = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Controller.Select(Index);
    }
}