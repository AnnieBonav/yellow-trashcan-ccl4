using UnityEngine;
using Random = UnityEngine.Random;

public class PotionKnowledgebase : MonoBehaviour
{
    private static PotionKnowledgebase instance;
    public static PotionKnowledgebase Instance => instance;

    private RecipeData[] availableRecipes;

    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            availableRecipes = Resources.LoadAll<RecipeData>("Recipes");
            foreach (var recipe in availableRecipes)
            {
                Debug.Log(recipe.name);
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public RecipeData RandomRecipe()
    {
        if (availableRecipes.Length <= 0) return null;
        return availableRecipes[Random.Range(0, availableRecipes.Length)];
    }
    
    
}
