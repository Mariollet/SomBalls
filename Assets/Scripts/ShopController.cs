using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private PlayerCustom playerCustom;
    private MenuController menuController;
    public List<GameObject> activeColor;
    public List<GameObject> activeTrail;
    public List<GameObject> diamondColorItem;
    public List<GameObject> emptyDiamondColorItem;
    public List<GameObject> diamondTrailItem;
    public List<GameObject> emptyDiamondTrailItem;
    public List<Text> colorGoldValueTxt;
    public List<int> colorGoldValue;
    public List<Text> trailGoldValueTxt;
    public List<int> trailGoldValue;
    private int playerColor;
    private int playerTrail;
    private int index;
    [HideInInspector] public int playerGold;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerCustom = FindObjectOfType<PlayerCustom>();
        menuController = FindObjectOfType<MenuController>();
        playerGold = PlayerPrefs.GetInt("PlayerGold");

        // Get Player Color
        if (!PlayerPrefs.HasKey("PlayerColor"))
        {
            playerColor = 0;
        }
        else
        {
            playerColor = PlayerPrefs.GetInt("PlayerColor");
        }

        // Get Player Trail
        if (!PlayerPrefs.HasKey("PlayerTrail"))
        {
            playerTrail = 0;
        }
        else
        {
            playerTrail = PlayerPrefs.GetInt("PlayerTrail");
        }

        // Active Color & Trail
        foreach (GameObject activeColor in activeColor)
        {
            activeColor.SetActive(false);
        }
        activeColor[playerColor].SetActive(true);

        foreach (GameObject activeTrail in activeTrail)
        {
            activeTrail.SetActive(false);
        }
        activeTrail[playerTrail].SetActive(true);

        // Set Price
        foreach (Text colorGoldValueTxt in colorGoldValueTxt)
        {
            colorGoldValueTxt.text = colorGoldValue[index].ToString();
            index++;
        }
        index = 0;
        foreach (Text trailGoldValueTxt in trailGoldValueTxt)
        {
            trailGoldValueTxt.text = trailGoldValue[index].ToString();
            index++;
        }
        index = 0;

        // Is already buying, too expensive or purchasable
        PlayerPrefs.SetInt("colorIsBuy0", 1);
        foreach (Text colorGoldValueTxt in colorGoldValueTxt)
        {
            if (PlayerPrefs.GetInt("colorIsBuy" + index.ToString()) == 1)
            {
                colorGoldValueTxt.text = "Take";
                diamondColorItem[index].SetActive(false);
                emptyDiamondColorItem[index].SetActive(false);
            }
            else
            {
                if (playerGold < colorGoldValue[index])
                {
                    emptyDiamondColorItem[index].SetActive(true);
                    diamondColorItem[index].SetActive(false);
                }
                else
                {
                    emptyDiamondColorItem[index].SetActive(false);
                    diamondColorItem[index].SetActive(true);
                }
            }
            index++;
        }
        index = 0;
        PlayerPrefs.SetInt("trailIsBuy0", 1);
        foreach (Text trailGoldValueTxt in trailGoldValueTxt)
        {
            if (PlayerPrefs.GetInt("trailIsBuy" + index.ToString()) == 1)
            {
                trailGoldValueTxt.text = "Take";
                diamondTrailItem[index].SetActive(false);
                emptyDiamondTrailItem[index].SetActive(false);
            }
            else
            {
                if (playerGold < trailGoldValue[index])
                {
                    emptyDiamondTrailItem[index].SetActive(true);
                    diamondTrailItem[index].SetActive(false);
                }
                else
                {
                    emptyDiamondTrailItem[index].SetActive(false);
                    diamondTrailItem[index].SetActive(true);
                }
            }
            index++;
        }
        index = 0;
    }

    public void Initialize()
    {
        playerGold = PlayerPrefs.GetInt("PlayerGold");

        // Get Player Color
        if (!PlayerPrefs.HasKey("PlayerColor"))
        {
            playerColor = 0;
        }
        else
        {
            playerColor = PlayerPrefs.GetInt("PlayerColor");
        }

        // Get Player Trail
        if (!PlayerPrefs.HasKey("PlayerTrail"))
        {
            playerTrail = 0;
        }
        else
        {
            playerTrail = PlayerPrefs.GetInt("PlayerTrail");
        }

        // Active Color & Trail
        foreach (GameObject activeColor in activeColor)
        {
            activeColor.SetActive(false);
        }
        activeColor[playerColor].SetActive(true);

        foreach (GameObject activeTrail in activeTrail)
        {
            activeTrail.SetActive(false);
        }
        activeTrail[playerTrail].SetActive(true);

        // Set Price
        foreach (Text colorGoldValueTxt in colorGoldValueTxt)
        {
            colorGoldValueTxt.text = colorGoldValue[index].ToString();
            index++;
        }
        index = 0;
        foreach (Text trailGoldValueTxt in trailGoldValueTxt)
        {
            trailGoldValueTxt.text = trailGoldValue[index].ToString();
            index++;
        }
        index = 0;

        // Is already buying, too expensive or purchasable
        PlayerPrefs.SetInt("colorIsBuy0", 1);
        foreach (Text colorGoldValueTxt in colorGoldValueTxt)
        {
            if (PlayerPrefs.GetInt("colorIsBuy" + index.ToString()) == 1)
            {
                colorGoldValueTxt.text = "Take";
                diamondColorItem[index].SetActive(false);
                emptyDiamondColorItem[index].SetActive(false);
            }
            else
            {
                if (playerGold < colorGoldValue[index])
                {
                    emptyDiamondColorItem[index].SetActive(true);
                    diamondColorItem[index].SetActive(false);
                }
                else
                {
                    emptyDiamondColorItem[index].SetActive(false);
                    diamondColorItem[index].SetActive(true);
                }
            }
            index++;
        }
        index = 0;
        PlayerPrefs.SetInt("trailIsBuy0", 1);
        foreach (Text trailGoldValueTxt in trailGoldValueTxt)
        {
            if (PlayerPrefs.GetInt("trailIsBuy" + index.ToString()) == 1)
            {
                trailGoldValueTxt.text = "Take";
                diamondTrailItem[index].SetActive(false);
                emptyDiamondTrailItem[index].SetActive(false);
            }
            else
            {
                if (playerGold < trailGoldValue[index])
                {
                    emptyDiamondTrailItem[index].SetActive(true);
                    diamondTrailItem[index].SetActive(false);
                }
                else
                {
                    emptyDiamondTrailItem[index].SetActive(false);
                    diamondTrailItem[index].SetActive(true);
                }
            }
            index++;
        }
        index = 0;
    }

    public void TryToBuy(int color)
    {
        if (PlayerPrefs.GetInt("colorIsBuy" + color.ToString()) == 1 && playerColor != color)
        {
            audioManager.Play("ChangeItem");
            playerCustom.SetColor(color);
        }
        else
        {
            if (playerGold >= colorGoldValue[color] && playerColor != color)
            {
                playerGold -= colorGoldValue[color];
                PlayerPrefs.SetInt("PlayerGold", playerGold);
                menuController.OnGoldChange();
                PlayerPrefs.SetInt("colorIsBuy" + color.ToString(), 1);
                audioManager.Play("Exp");
                playerCustom.SetColor(color);
            }
            else
            {
                audioManager.Play("Denied");
            }
        }
    }
    public void TryToBuyTrail(int trail)
    {
        if (PlayerPrefs.GetInt("trailIsBuy" + trail.ToString()) == 1 && playerTrail != trail)
        {
            audioManager.Play("ChangeItem");
            playerCustom.SetTrail(trail);
        }
        else
        {
            if (playerGold >= trailGoldValue[trail] && playerTrail != trail)
            {
                playerGold -= trailGoldValue[trail];
                PlayerPrefs.SetInt("PlayerGold", playerGold);
                menuController.OnGoldChange();
                PlayerPrefs.SetInt("trailIsBuy" + trail.ToString(), 1);
                audioManager.Play("Exp");
                playerCustom.SetTrail(trail);
            }
            else
            {
                audioManager.Play("Denied");
            }
        }
    }

    public void ResetItem()
    {
        for (int i = 0; i < activeColor.Count; i++)
        {
            PlayerPrefs.SetInt(("colorIsBuy" + i.ToString()), 0);

        }
        PlayerPrefs.SetInt("colorIsBuy0", 1);
        for (int i = 0; i < activeTrail.Count; i++)
        {
            PlayerPrefs.SetInt(("trailIsBuy" + i.ToString()), 0);
        }
        PlayerPrefs.SetInt("trailIsBuy0", 1);
    }
}
