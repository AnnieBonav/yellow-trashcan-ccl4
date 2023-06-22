using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BrewProperties : MonoBehaviour
{
    [SerializeField] private float easeTime;
    [SerializeField] private float bubbling;
    [SerializeField] private float swirl;
    [SerializeField] private List<Color> colours;

    [SerializeField] private GameObject brewDisplay;
    [SerializeField] private GameObject bubbles;
    [SerializeField] private Material baseBrewColour;

    private float _colourTimer = 0;
    private float _bubblingTimer = 0;
    private float _swirlTimer = 0;

    private Color _lastColour;
    private float _lastBubbling;
    private float _lastSwirl;

    private Color _currentColour;
    private float _currentBubbling;
    private float _currentSwirl;


    private Material _brewMaterial;
    private VisualEffect _bubblesVFX;

    private void ResetProperties()
    {
        _colourTimer = 0;
        _bubblingTimer = 0;
        _swirlTimer = 0;
        _currentColour = baseBrewColour.color;
        _lastColour = _currentColour;

        _currentBubbling = 0;
        _lastBubbling = _currentBubbling;

        _currentSwirl = 0;
        _lastSwirl = _currentSwirl;
    }

    void Start()
    {
        _currentColour = baseBrewColour.color;
        _lastColour = _currentColour;
        _lastSwirl = _currentSwirl;
        _lastBubbling = _currentBubbling;
        if (brewDisplay is not null) _brewMaterial = brewDisplay.GetComponent<Renderer>().material;
        if (bubbles is not null) _bubblesVFX = bubbles.GetComponent<VisualEffect>();
    }
    
    void Update()
    {
        if (colours.Count > 0)
        {
            InterpolateColour(Time.deltaTime);
        }

        if (Math.Abs(_currentBubbling - bubbling) > 0.001f)
        {
            InterpolateBubbling(Time.deltaTime);
        }

        if (Math.Abs(_currentSwirl - swirl) > 0.001f)
        {
            InterpolateSwirl(Time.deltaTime);
        }
        UpdateEffects();
    }

    public void UpdateEffects()
    {
        if (_brewMaterial is not null)
        {
            _brewMaterial.SetColor("_Color", _currentColour);
            _brewMaterial.SetFloat("_Strength", _currentBubbling);
            _brewMaterial.SetFloat("_Speed", _currentBubbling);
        }

        if (_bubblesVFX is not null)
        {
            _bubblesVFX.SetFloat("Rate", 1f+bubbling);
            _bubblesVFX.SetVector4("Colour", _currentColour);
        }
    }
    
    private void InterpolateColour(float delta)
    {
        if (_colourTimer < easeTime)
        {
            _colourTimer += delta;
            _currentColour = Color.Lerp(_lastColour, Mix(colours), _colourTimer / easeTime);
            if (_colourTimer < easeTime) return;

            _currentColour = Mix(colours);

            _lastColour = _currentColour;
        }
    }

    private void InterpolateBubbling(float delta)
    {
        if (_bubblingTimer < easeTime)
        {
            _bubblingTimer += delta;
            _currentBubbling = Mathf.LerpUnclamped(_lastBubbling, bubbling, EaseOutBounce(_bubblingTimer / easeTime));

            if (_bubblingTimer < easeTime) return;

            _currentBubbling = bubbling;
            _lastBubbling = bubbling;
        }
    }

    public void InterpolateSwirl(float delta)
    {
        if (_swirlTimer < easeTime)
        {
            _swirlTimer += delta;
            _currentSwirl = Mathf.LerpUnclamped(_lastSwirl, swirl, EaseInCubic(_swirlTimer / easeTime));
            
            if(_swirlTimer < easeTime) return;

            _currentSwirl = swirl;
            _lastSwirl = swirl;
        }
    }

    private float EaseOutBounce(float t)
    {
        if (t < (1 / 2.75f))
        {
            return 7.5625f * t * t;
        }

        if (t < (2 / 2.75f))
        {
            return 7.5625f * (t -= (1.5f / 2.75f)) * t + .75f;
        }

        if (t < (2.5 / 2.75f))
        {
            return 7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f;
        }

        return 7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f;
    }

    private float EaseInCubic(float t)
    {
        return t * t * t;
    }

    public void AddColour(Color value)
    {
        _lastColour = _currentColour;
        colours.Add(value);
        _colourTimer = 0;
    }

    public void AddBubbling(float value)
    {
        _lastBubbling = _currentBubbling;
        bubbling += value;
        _bubblingTimer = 0;
    }

    public void AddSwirl(float value)
    {
        _lastSwirl = _currentSwirl;
        swirl += value;
        _swirlTimer = 0;
    }

    private Color Mix(List<Color> colours)
    {
        Color mix = new Color(0, 0, 0, 1);
        foreach (var colour in colours)
        {
            mix += colour;
        }

        return mix / colours.Count;
    }

    public void ResetColor()
    {
        ResetProperties();
    }
}