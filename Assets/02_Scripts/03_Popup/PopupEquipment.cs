using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class PopupEquipment : Popup
{
    [SerializeField] public Sprite[] weapons;
    private Dictionary<WeaponGrade, Sprite> spriteDic = new();
    [SerializeField] public Sprite redWeapon;
    
    [SerializeField, GetComponentInChildrenName("Img_Weapon")] private Image imgWeapon;
    
    [SerializeField, GetComponentInChildrenName("Btn_Previous")] private Button btnPrevious;
    [SerializeField, GetComponentInChildrenName("Btn_Next")] private Button btnNext;
    
    [SerializeField, GetComponentInChildrenName("Btn_Equipment")] private Button btnEquipment;
    [SerializeField, GetComponentInChildrenName("Btn_Buy")] private Button btnBuy;

    [SerializeField, GetComponentInChildrenName("Txt_Price")] private Text txtPrice;
    
    private int currentIndex = 0;
    protected override void Awake()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            spriteDic[(WeaponGrade)i] = weapons[i];
        }
        
        imgWeapon.sprite = spriteDic[(WeaponGrade)currentIndex];
        
        btnPrevious.onClick.AddListener(() =>
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = weapons.Length - 1;
            SetWeapon();

        });
        
        btnNext.onClick.AddListener(() =>
        {
            currentIndex++;
            if (currentIndex > weapons.Length - 1)
                currentIndex = 0;
            SetWeapon();
        });
        
        btnBuy.onClick.AddListener(() =>
        {
            // 현재 무기를 가지고 있다면 리턴
            if(DataManager.I.playerData.ownedWeapons.Contains(currentIndex))
                return;
            
            var a = DefaultTable.Weapons.GetList().Find(x => x.TID == currentIndex);
            Unified price = new Unified(a.Price);
            
            // 현재 가지고 있는 돌이 요구량 이상이라면 구매 성공
            if (GoodsManager.I.GetStone().CompareTo(price) > 0 || PlayerManager.I.isTest)
            {
                if (!PlayerManager.I.isTest)
                {
                    // 재화 감소
                    GoodsManager.I.DecreaseStone(price);
                }
                
                // 보유 리스트에 추가
                DataManager.I.playerData.ownedWeapons.Add(currentIndex);
                
                // 다시 세팅
                SetWeapon();
            }
            else
            {
                if (PopupManager.I.IsPopupOpen<PopupYes>())
                    PopupManager.I.GetPopup<PopupYes>().Close();
                var popupResult = PopupManager.I.GetPopup<PopupYes>();
                popupResult.Open();
                popupResult.SetText();
            }
        });
        
        btnEquipment.onClick.AddListener(() =>
        {
            // 현재 무기를 장착중이라면 리턴
            if(DataManager.I.playerData.currentWeapon == currentIndex)
                return;
            
            // 장착 중인 무기 변경
            DataManager.I.playerData.currentWeapon = currentIndex;
            
            // 장착 무기로 스프라이트 변경
            
            if(currentIndex == weapons.Length - 1)
                PlayerManager.I.player.weapon.ChangeWeaponSprite(redWeapon);
            else
                PlayerManager.I.player.weapon.ChangeWeaponSprite(weapons[currentIndex]);
        });
    }

    public override void Open()
    {
        base.Open();
        currentIndex = 0;
        SetWeapon();
    }

    private void SetWeapon()
    {
        bool isOwned = DataManager.I.playerData.ownedWeapons.Contains(currentIndex);
        ToggleBtn(isOwned);
        imgWeapon.sprite = spriteDic[(WeaponGrade)currentIndex];
        var weapons = DefaultTable.Weapons.GetList().Find(x => x.TID == currentIndex);
        txtPrice.text = new Unified(weapons.Price).IntPart.BigintToString();
    }

    private void ToggleBtn(bool isOwened)
    {   
        btnEquipment.gameObject.SetActive(isOwened);
        btnBuy.gameObject.SetActive(!isOwened);
    }
}
