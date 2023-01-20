using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryClothingHandler : MonoBehaviour
{   
    public SpriteRenderer bodyPart;
    public string type;

    private List<Sprite> _options;
    private Image _image;
    public int currentOption = 0;

    private void Start()
    {
        _image = GetComponentsInChildren<Image>()[1];
        LoadOptions();
    }


    private void UpdateOptions()
    {        
        _options = new List<Sprite>(PlayerInventory.instance.GetOptions(type));
    }

    private void LoadOptions() 
    {
        _options = new List<Sprite>(PlayerInventory.instance.GetOptions(type));
        if (_options.Count <= 0) return;
        bodyPart.sprite = _options[currentOption];
        _image.sprite = bodyPart.sprite;
        if (_image.sprite != null) {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1.0f);
        } else {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.0f);
        }

    }

    public void NextOption () 
    {
        UpdateOptions();
        if (_options.Count <= 0) return;

        currentOption++;
        if (currentOption >= _options.Count) {
            currentOption = 0;
        }
        bodyPart.sprite = _options[currentOption];
        _image.sprite = bodyPart.sprite;
        if (_image.sprite != null) {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1.0f);
        } else {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.0f);
        }
        
    }
    public void PreviousOption()
    {
        UpdateOptions();
        if (_options.Count <= 0) return;

        currentOption--;
        if (currentOption < 0) {
            currentOption = _options.Count - 1;
        }
        bodyPart.sprite = _options[currentOption];
        _image.sprite = bodyPart.sprite;
        if (_image.sprite != null) {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1.0f);
        } else {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.0f);
        }    
    }

    public int GetIndexOnOptions(OutfitSprite item)
    {
        return _options.IndexOf(_options.Find(i => i == item.sprite));
    }


    public void HandleWornItemSold()
    {
        UpdateOptions();
        currentOption = 0;
        bodyPart.sprite = _options[currentOption];
        _image.sprite = bodyPart.sprite;
        if (_image.sprite != null) {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1.0f);
        } else {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.0f);
        }
    }
}
