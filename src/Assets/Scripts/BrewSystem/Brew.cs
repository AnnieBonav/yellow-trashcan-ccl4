using System;
using System.Collections;
using System.Collections.Generic;
using Dictionaries;
using UnityEngine;
using UnityEngine.VFX;

public class Brew : MonoBehaviour
{
    [SerializeField] private InteractionsHandler interactionsHandler;
    [SerializeField] private List<RecipeData> recipes;
    [SerializeField] private IngredientDictionary _currentIngredients;
    [SerializeField] private Transform _potionSpawnOrigin;
    [SerializeField] private bool _debugIngredientsIn;
    [SerializeField] private GameObject trashPotion;

    [Header("VFX")]
    [SerializeField] private VisualEffect potionPoof;
    [SerializeField] private VisualEffect importantEffect;
    [SerializeField] private VisualEffect bubblesVFX;

    private Gradient _poofGradient;
    private GradientColorKey[] _poofColor;
    private GradientAlphaKey[] _poofAlfa;

    private void Awake()
    {
        _poofGradient = new Gradient();
        _poofColor = new GradientColorKey[2];
        _poofColor[0].color = Color.white;
        _poofColor[0].time = 0.0f;

        _poofAlfa = new GradientAlphaKey[2];
        _poofAlfa[0].alpha = 1.0f;
        _poofAlfa[0].time = 0.0f;
        _poofAlfa[1].alpha = 0.0f;
        _poofAlfa[1].time = 1.0f;

        InteractionsHandler.InteractionRaised += HandlePotionInteraction;
    }

    private void OnDisable()
    {
        InteractionsHandler.InteractionRaised -= HandlePotionInteraction;
    }

    private void HandlePotionInteraction(InteractionEvents raisedEvent)
    {
        if(raisedEvent == InteractionEvents.GrabPotion || raisedEvent == InteractionEvents.ReleasePotion)
        {
            print("Potion grabbed");
            importantEffect.Stop();
            bubblesVFX.Play();
        }
    }

    private void Start()
    {
        importantEffect.Stop();
        AkSoundEngine.PostEvent("Play_Bubbles", gameObject);
    }

    private RecipeData CurrentBrew()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].Ingredients.Equals(_currentIngredients)) return recipes[i];
        }

        return null;
    }

    public void AddIngredient(IngredientType type)
    {
        switch (type)
        {
            case IngredientType.Liquid:
                _currentIngredients.liquid++;
                break;
            case IngredientType.Mushroom:
                _currentIngredients.mushroom++;
                break;
            case IngredientType.Herb:
                _currentIngredients.herb++;
                break;
            case IngredientType.Bark:
                _currentIngredients.bark++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        
        if (_debugIngredientsIn)
        {
            RecipeData currentBrew = CurrentBrew();

            if (currentBrew is not null) Debug.Log($"Currently {currentBrew.name}");
            else
            {
                Debug.Log("Currently not a known potion");
            }
        }
    }

    public void MakePotion(GameObject flask)
    {
        RecipeData currentBrew = CurrentBrew();
        HandlePlayPoof(currentBrew);
        HandleVFX();
        if (currentBrew is not null)
        {
            Debug.Log($"You made a {currentBrew.name}!!");
            GameObject noobPotion = Instantiate(currentBrew.PotionPrefab);
            noobPotion.transform.position = _potionSpawnOrigin.transform.position;
            interactionsHandler.RaiseInteraction(InteractionEvents.CreateCorrectPotion);

            AkSoundEngine.PostEvent("Play_CorrectPotion", gameObject);
        }
        else
        {
            Debug.Log("You made trash.");
            GameObject noobPotion = Instantiate(trashPotion);
            noobPotion.transform.position = _potionSpawnOrigin.transform.position;
            interactionsHandler.RaiseInteraction(InteractionEvents.CreateIncorrectPotion);

            AkSoundEngine.PostEvent("Play_IncorrectPotion", gameObject);
        }
        interactionsHandler.RaiseInteraction(InteractionEvents.CreatePotion);
        ResetCurrentIngredients();
        Destroy(flask.transform.parent.gameObject);
    }

    private void ResetCurrentIngredients()
    {
        _currentIngredients.liquid = 0;
        _currentIngredients.mushroom = 0;
        _currentIngredients.bark = 0;
        _currentIngredients.herb = 0;
    }

    private void HandleVFX()
    {
        // importantEffect
        importantEffect.Play();
        bubblesVFX.Stop(); // TODO: Add that when the potion is picked the effects are reset
    }

    private void HandlePlayPoof(RecipeData currentBrew)
    {
        if(currentBrew == null)
        {
            print("It was wrong, handling poof as black.");
            _poofColor[1].color = Color.black;
        }
        else
        {
            _poofColor[1].color = currentBrew.PotionColor;
        }
        
        _poofColor[1].time = 1.0f;
        _poofGradient.SetKeys(_poofColor, _poofAlfa);

        potionPoof.SetGradient("ColourGradient", _poofGradient);
        potionPoof.Play();
    }
}
