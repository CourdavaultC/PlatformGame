using UnityEngine;
using System.Linq;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance LoadAndSaveData dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        // Récupérer le nbre de coins sauvegardées
        Inventory.instance.coinsCount = PlayerPrefs.GetInt("coinsCount", 0);
        Inventory.instance.UpdateTextUI();

        string[] itemsSaved = PlayerPrefs.GetString("inventoryItems", "").Split(',');

        for (int i = 0; i < itemsSaved.Length; i++)
        {
            // Pour éviter que Unity renvoi une chaîne de caractères vide fonction if
            if (itemsSaved[i] != "")
            {
                // Ajouter l'item à l'inventaire
                // Conversion au format string
                int id = int.Parse(itemsSaved[i]);
                Item currentItem = ItemsDatabase.instance.allItems.Single(x => x.id == id);
                // ajouter l'élément à l'inventaire
                Inventory.instance.content.Add(currentItem);
            }
        }

        // Mise à jour du UpdateInventoryUI
        Inventory.instance.UpdateInventoryUI();

        // Récupérer le nbre de PV
        /*int currentHealth = PlayerPrefs.GetInt("playerHealth", PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.currentHealth = currentHealth;
        PlayerHealth.instance.healthBar.SetHealth(currentHealth);*/
    }

    public void SaveData()
    {
        // Sauvegarder le nbre de coins
        PlayerPrefs.SetInt("coinsCount", Inventory.instance.coinsCount);

        if (CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            // Sauvegarder le niveau atteind
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
        }

        // Sauvegarder les PV
        /*PlayerPrefs.SetInt("playerHealth", PlayerHealth.instance.currentHealth);*/

        // Sauvegarde des items
        string itemsInInventory = string.Join(",", Inventory.instance.content.Select(x => x.id));
        PlayerPrefs.SetString("inventoryItems", itemsInInventory);
    }
}
