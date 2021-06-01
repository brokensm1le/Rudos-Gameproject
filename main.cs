using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    [SerializeField] private Text cur_money;
    private double money;
    public GameObject container;
    public GameObject item;
    public GameObject CanvasObject;
    GameObject[] items = new GameObject[20];
    int count;

    Dictionary<string, double> companies = new Dictionary<string, double>() 
        {
            {"PetroChina", 800},
            {"Visa", 1000},
            {"Walmart", 750},
            {"Pfizer", 900},
            {"McDonald’s", 700},
        };

    // Start is called before the first frame update
    void Start()
    {
        money = 1000;
        count = 0;
        cur_money.text = money.ToString() + " $";
        string t = "";
        try {
            foreach (Text i in container.GetComponentsInChildren<Text>()) {
                if (i.name == "Name") {
                    t += i.text;
                    Text price_i = i.GetComponentsInChildren<Text>()[1];
                    price_i.text = companies[i.text].ToString()+ " $";
                }
            }
        } catch {
           Debug.Log(t); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSum(GameObject company) {
        Text Name_company = company.GetComponentsInChildren<Text>()[0];
        InputField kol_shr = company.GetComponentsInChildren<InputField>()[0];
        //List<GameObject> items = new List<GameObject>();
    	try {
    		double d  = double.Parse(kol_shr.text);
            if (d > 0) {
                kol_shr.text = "";
                double x = companies[Name_company.text] * d;
                if (money - x < 0) {
                    Debug.Log("Мало средств");    
                } else {
                    money -= x;
                    cur_money.text = money.ToString() + " $";
                    item.GetComponentsInChildren<Text>()[0].text = Name_company.text;
                    item.GetComponentsInChildren<Text>()[1].text = d.ToString();
                    items[count] = item;
                    //for (int i = 0; i <= count; i++) {
                        GameObject enemy = Instantiate(items[count], new Vector2(0, 200 - count * 185), Quaternion.identity) as GameObject;
                        enemy.transform.SetParent(CanvasObject.transform, false); 
                    //}
                    count++;
                }
            }

    	} 
    	catch {
    		Debug.Log("Ошибка ввода!");
    	} 
    }

    public void load_scene (int Level) {
    SceneManager.LoadScene (Level);
    }
    public void load_scene () {
    Application.Quit ();
    }
}
