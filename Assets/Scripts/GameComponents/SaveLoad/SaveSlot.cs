using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField]
    private int slotID;
    public int SlotID => slotID;

    [SerializeField]
    private Image image;


    [SerializeField]
    private GameObject SLRButton;
    [SerializeField] 
    private Text SLRButtonLabel;

    [SerializeField] 
    private Button deleteButton;

    [SerializeField]
    private TextMeshProUGUI EmptySlot;

    private bool localmode = false;

    public void TargerButton()
    {
        if (localmode)
        {
            GameManager.Instance.SaveLoad.SaveSlot(slotID);
            SlotUpdate();
        }
        else
        {
            GameManager.Instance.Loading.LoadGameScene(slotID);
        }    
    }

    public void DeleteButton()
    {
        GameManager.Instance.SaveLoad.DeleteSlot(slotID);

        SlotUpdate();
    }

    public void SlotUpdate()
    {
        SlotInfo slot = GameManager.Instance.SaveLoad.GetSlot(slotID);

        if (slot != null)
        {
            EmptySlot.gameObject.SetActive(false);

            image.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);

            image.sprite = GameManager.Instance.SaveLoad.BackgroundImages.Where(i => i.name == slot.BackgroundImageName).First();
        }
        else
        {
            EmptySlot.gameObject.SetActive(true);

            image.gameObject.SetActive(false);
            deleteButton.gameObject.SetActive(false);
        }

        localmode = SaveLoadMenu.instance.Mode;

        SLRButton.SetActive(true);

        if (localmode)
        {
            SLRButtonLabel.text = slot == null ? "Сохранить" : "Перезапизать";
        }
        else
        {
            if (slot == null)
                SLRButton.SetActive(false);

            SLRButtonLabel.text = "Загрузить";
        }
    }
}
