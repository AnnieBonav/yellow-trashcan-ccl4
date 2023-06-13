using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewProperties : MonoBehaviour
{
    [SerializeField] private float easeTime;
    [SerializeField] private float bubbling;
    [SerializeField] private float swirl;
    [SerializeField] private List<Color> colours;
    [SerializeField] private float volatility;

    
    [SerializeField] private MeshRenderer colourDisplay;
    
    private float _colourTimer = 0;
    private Color _lastColour;
    
    private Color _currentColour = Color.cyan;
    private float _currentBubbling;

   
    
    
    
    void Start()
    {
        _lastColour = _currentColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (colours.Count > 0)
        {
            InterpolateColour(Time.deltaTime);
        }
        colourDisplay.material.color = _currentColour;
    }

    public void InterpolateColour(float delta)
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
    
    public void AddColour(Color value)
    {
        Debug.Log($"Added {value}");
        _lastColour = _currentColour;
        colours.Add(value);
        _colourTimer = 0;
    }

    public void AddBubbling(float value)
    {
        
    }

    public void AddSwirl(float value)
    {
        
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
}
