using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class PopupEquipment : Popup
{
    [SerializeField] private Sprite[] weapons;
    private Dictionary<WeaponGrade, Sprite> spriteDic = new();
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
