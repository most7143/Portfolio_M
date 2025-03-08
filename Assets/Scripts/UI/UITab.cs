using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITab : MonoBehaviour, IPointerClickHandler
{
    public UITabController Controller;

    public Image Image;

    public int Index;

    public virtual void Activate()
    {
        Controller.SelectedIndex = Index;
        Image.sprite = ResourcesManager.Instance.LoadSprite("TabBackground_Click");
    }

    public virtual void Deactivate()
    {
        Image.sprite = ResourcesManager.Instance.LoadSprite("TabBackground_Normal");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Controller.Select(Index);
    }
}