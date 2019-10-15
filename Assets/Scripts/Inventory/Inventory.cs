using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Linear
{
    public class Inventory : MonoBehaviour
    {
        #region Variables
        public List<Item> inv = new List<Item>();//list of items
        public Item selectedItem;
        public bool showInv;

        public Vector2 scr;
        public Vector2 scrollPos;

        public int money;

        public string sortType = "";
        public Transform dropTransform;
        public GameObject curWeapon;
        public GameObject curHelm;

        [System.Serializable]
        public struct equipment
        {
            public string name;
            public Transform location;
            public GameObject curItem;
        };
        public equipment[] equipmentSlots;
        public GUISkin invSkin;
        #endregion

        void Start()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            inv.Add(ItemData.CreateItem(0));
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.I))
            {
                inv.Add(ItemData.CreateItem(Random.Range(0, 3)));
            }
            if(Input.GetKeyDown(KeyCode.P))
            {
                inv[0].Amount += 1;
                Debug.Log(inv[0].Amount);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                showInv = !showInv;
                if (showInv)
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    return;
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    return;
                }
            }
        }

        private void OnGUI()
        {
            if (showInv)
            {
                scr.x = Screen.width / 16;
                scr.y = Screen.height / 9;

                GUI.Box(new Rect(0, 0, scr.x * 8, Screen.height), "");
                Display();
                GUI.skin = invSkin;
                if (selectedItem != null)
                {
                    GUI.Box(new Rect(4.75f * scr.x, 0.25f * scr.y, 1.5f * scr.x, 1.5f * scr.y), selectedItem.Name);
                    GUI.DrawTexture(new Rect(4.75f * scr.x, 0.55f * scr.y, 1.5f * scr.x, 1.5f * scr.y), selectedItem.Icon);
                    // GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0f);
                    GUI.Box(new Rect(4.2f * scr.x, 2.2f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Description);
                    GUI.Button(new Rect(6f * scr.x, 3.15f * scr.y, 1.5f * scr.x, 0.25f * scr.y), "Current Amount: "+selectedItem.Amount);
                    ItemUse(selectedItem.Type);
                }
                else
                {
                    return;
                }
                GUI.skin = null;

            }
        }

        void Display()
        {
            if (inv.Count <= 34)//if we have 34 or less items in inventory
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
            }
            else//more than 34 items
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollPos, new Rect(0, 0, 0, 8.5f * scr.y + ((inv.Count - 34) * (0.25f * scr.y))), false, true);

                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
                GUI.EndScrollView();
            }
        }

        void ItemUse(ItemType Type)
        {
            switch (Type)
            {
                case ItemType.Ingredient:
                    break;
                case ItemType.Potion:
                    break;
                case ItemType.Scroll:
                    break;
                case ItemType.Food:
                    break;
                case ItemType.Apparel:
                    break;
                case ItemType.Weapon:
                    break;  
                case ItemType.Resource:
                    break;
                case ItemType.Equipment:
                    break;
                case ItemType.Armour:
                    break;
                case ItemType.QuestItem:
                    break;
                case ItemType.Money:
                    break;
                case ItemType.Junk:
                    break;
                case ItemType.Miscellaneous:
                    break;
                default:
                    break;
            }
            if (GUI.Button(new Rect(6f * scr.x, 8f * scr.y, 1.5f * scr.x, 0.25f * scr.y), "Discard"))
            {
                for (int i = 0; i < equipmentSlots.Length; i++)
                {
                    //check equiped item
                    if (equipmentSlots[i].curItem != null && selectedItem.ItemMesh.name == equipmentSlots[i].curItem.name)
                    {
                        // if deleted
                        Destroy(equipmentSlots[i].curItem);
                    }
                }
                // sawn in front
                GameObject droppedItem = Instantiate(selectedItem.ItemMesh, dropTransform.position, Quaternion.identity);
                droppedItem.name = selectedItem.Name;
                // just in case
                droppedItem.AddComponent<Rigidbody>().useGravity = true;
                //reduce or delete
                if (selectedItem.Amount > 1)
                {
                    selectedItem.Amount--;
                }
                else
                {
                    inv.Remove(selectedItem);
                    selectedItem = null;
                    return;
                }
            }
        }
    }
}