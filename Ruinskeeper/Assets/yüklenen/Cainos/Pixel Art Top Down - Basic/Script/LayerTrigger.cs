using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; // SortingGroup için gerekli

namespace Cainos.PixelArtTopDown_Basic
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers
    public class LayerTrigger : MonoBehaviour
    {
        public string layer; // Yeni layer adı
        public string sortingLayer; // Yeni sorting layer adı

        private void OnTriggerExit2D(Collider2D other)
        {
            // Oyun nesnesinin layer'ını değiştir
            other.gameObject.layer = LayerMask.NameToLayer(layer);

            // Sorting Group bileşeni varsa, sorting layer'ını değiştir
            SortingGroup sortingGroup = other.gameObject.GetComponent<SortingGroup>();
            if (sortingGroup != null)
            {
                sortingGroup.sortingLayerName = sortingLayer;
            }
            else
            {
                // Sorting Group yoksa, SpriteRenderer'ları değiştir
                SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sortingLayerName = sortingLayer;
                }

                // Eğer alt nesnelerde SpriteRenderer varsa onları da değiştir
                SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer childSr in srs)
                {
                    childSr.sortingLayerName = sortingLayer;
                }
            }
        }
    }
}
