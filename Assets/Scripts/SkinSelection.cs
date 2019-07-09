using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinSelection : MonoBehaviour
{
    public GameObject skins;

    private Image _defaultSkin;
    private Image _mazeSkin;
    private Image _egyptSkin;

    private void Start()
    {
        _defaultSkin = skins.transform.GetChild(0).GetComponentInChildren<Image>();
        _mazeSkin = skins.transform.GetChild(1).GetComponentInChildren<Image>();
        _egyptSkin = skins.transform.GetChild(2).GetComponentInChildren<Image>();
        
        setColor();
    }

    private void setColor()
    {
        _defaultSkin.color = Color.white;
        _mazeSkin.color = Color.white;
        _egyptSkin.color = Color.white;
        
        if (PlayerPrefs.GetInt("Skin", 0) == 0)
        {
            _defaultSkin.color = Color.green;
        }
        else if (PlayerPrefs.GetInt("Skin", 0) == 1)
        {
            _mazeSkin.color = Color.green;
        }
        else if (PlayerPrefs.GetInt("Skin", 0) == 2)
        {
            _egyptSkin.color = Color.green;
        }
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void setSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("Skin", skinIndex);
        
        setColor();
    }
}
