using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoSingleton<PopupManager>
{
    [GetComponentInChildren(true)] public List<Popup> popups;
    public bool isPopupOpened;    
    
    
    public T GetPopup<T>() where T : Popup
    {
        T popup = popups.OfType<T>().FirstOrDefault();

        // 팝업이 존재하지 않으면 Resources 폴더에서 로드
        if (popup == null)
        {
            string popupName = typeof(T).Name;
            T prefab = Resources.Load<T>(popupName);
            if (prefab != null)
            {
                popup = Instantiate(prefab);
                popup.gameObject.SetActive(false);
                popups.Add(popup);
            }
            else
            {
                Debug.LogError($"Failed to load popup: {popupName}");
                return null;
            }
        }
        
        // 모든 열려 있는 팝업을 닫음
        foreach (var openPopup in popups.Where(p => p.gameObject.activeSelf && popup.popupType == p.popupType))
        {
            openPopup.Close();
        }
        
        return popup;
    }

    public bool IsPopupOpen<T>() where T : Popup
    {
        return popups.OfType<T>().Any(popup => popup.gameObject.activeSelf);
    }
    
    public void RegisterPopup(Popup popup)
    {
        if (popups.Contains(popup) == false)
        {
            popups.Add(popup);            
        }
    }

    public void UnregisterPopup(Popup popup)
    {
        if (popups.Contains(popup))
        {
            popups.Remove(popup);
        }
    }
}
