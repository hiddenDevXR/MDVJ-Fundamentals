using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolingTest : MonoBehaviour
{
    public List<Item> itemPool;
    public int poolMaxSize = 3;
    public AudioClip itemAdded, itemDestroyed, itemBrandNew;

    void Start()
    {
        itemPool = new List<Item>();
    }

    
    public void AddToPool(Item item)
    {

        item.index++;
        item.gameObject.SetActive(false);

        if (item.index == 3)
        {
            if(itemPool.Contains(item))
            {
                itemPool.Remove(item);
            }
            gameObject.GetComponent<AudioSource>().PlayOneShot(itemDestroyed, 1f);
            Destroy(item.gameObject);
            return;
        }

        

        if (!itemPool.Contains(item))        
        {
           if(itemPool.Count < poolMaxSize)
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(itemBrandNew, 1f);
                itemPool.Add(item);             
            }                
        }

        else
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(itemAdded, 1f);
        }

        ManageItems();
    }

    public void ManageItems()
    {
        if(itemPool.Count == 3)
        {
            int randIndex = Random.Range(0, poolMaxSize);
            itemPool[randIndex].gameObject.SetActive(true);
        }
    }
}
