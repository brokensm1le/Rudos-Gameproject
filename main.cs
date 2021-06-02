using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Pair<T, U> {
    public Pair() {
    }

    public Pair(T f, U s) {
        this.first = f;
        this.second = s;
    }

    public T first { get; set; }
    public U second { get; set; }
}




public class main : MonoBehaviour
{
    [SerializeField] private Text cur_money;
    private double money;

    public GameObject container;
    public GameObject item;
    public GameObject LastPurchaseObject;
    public GameObject CanvasObject;
    GameObject[] items = new GameObject[100];
    int count;



    // Инициализация Map
    Dictionary<string, Pair<double, double>> companies = new Dictionary<string, Pair<double, double>>() 
        {
            {"PetroChina",  new Pair<double, double>(800, 0)},
            {"Visa",        new Pair<double, double>(1000, 0)},
            {"Walmart",     new Pair<double, double>(750, 0)},
            {"Pfizer",      new Pair<double, double>(900, 0)},
            {"McDonald’s",  new Pair<double, double>(700, 0)},
        };

    // Start is called before the first frame update
    void Start()
    {
        money = 1000;
        count = 0;
        cur_money.text = money.ToString() + " $";
        foreach (Text i in container.GetComponentsInChildren<Text>()) 
        {
            if (i.name == "Name") 
            {
                Text price_i = i.GetComponentsInChildren<Text>()[1];
                price_i.text = companies[i.text].first.ToString()+ " $";
            }
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    public void Next() {
        for (int i = 0; i < count; i++) {
            GameObject enemy = items[i];
            Destroy(enemy);
        }
        count = 0;
    }




    // Процесс покупки
    public void SetSum(GameObject company) {
        Text Name_company = company.GetComponentsInChildren<Text>()[0];
        InputField kol_shr = company.GetComponentsInChildren<InputField>()[0];
    	try {
    		double d  = double.Parse(kol_shr.text);
            if (d > 0) {
                kol_shr.text = "";
                double x = companies[Name_company.text].first * d;
                 if (money - x < 0) 
                {
                    Debug.Log("Мало средств");    
                } else {
                    money -= x;
                    cur_money.text = money.ToString() + " $";
                    companies[Name_company.text].second = d;
                    item.GetComponentsInChildren<Text>()[0].text = Name_company.text;
                    item.GetComponentsInChildren<Text>()[1].text = d.ToString();
                    //for (int i = 0; i <= count; i++) {
                        GameObject enemy = Instantiate(item, new Vector2(0, 1470-190*count), Quaternion.identity) as GameObject;
                        items[count] = enemy;
                        enemy.transform.SetParent(LastPurchaseObject.transform, false); 
                    //}
                    count++;
                }
            }

    	} 
    	catch {
    		Debug.Log("Ошибка ввода!");
    	} 
    }



    // Процесс открытия Mail, News, Info
    public void open(GameObject what) {
        GameObject enemy = Instantiate(what, what.transform.position, Quaternion.identity) as GameObject;
        enemy.transform.SetParent(CanvasObject.transform, false);
    }
}
