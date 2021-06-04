using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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




public class main : MonoBehaviour {
    [SerializeField] private Text cur_money;
    private double money;

    public GameObject container;
    public GameObject item;
    public GameObject LastPurchaseObject;
    public GameObject CanvasObject;
    GameObject[] items = new GameObject[100];
    int count_transfer;
    int count_news = 8;
    int count_mail = 7;
    List<int> list_mail = new List<int>();
    List<int> list_news = new List<int>();    


    // Инициализация Map
    Dictionary<string, Pair<double, double>> companies = new Dictionary<string, Pair<double, double>>() {
            {"PetroChina",  new Pair<double, double>(800, 0)},
            {"Visa",        new Pair<double, double>(1000, 0)},
            {"Walmart",     new Pair<double, double>(750, 0)},
            {"Pfizer",      new Pair<double, double>(900, 0)},
            {"McDonald’s",  new Pair<double, double>(700, 0)},
    };


    string[] news = { "США разрабатывает новый пакет санкций, об этом заявляет Bloobmberg.Подробности пока не известны, эксперты предсказывают направленность санкций на российскую и китайскую нефть.", 
                      "В США в этом сезоне от гриппа зафиксировано уже 12 тысяч смертей и 22 миллиона инфицированных.",
                      "Pfizer пошел впред от всех фарм компаний.",
                      "Сеть магазинов Walmart в Канаде решила отказаться от работы с картами Visa из-за высокой комиссии. Об этом сообщает Reuters со ссылкой на заявление ретейлера.",
                      "Путин призвал всех мире заниматься спортом и праильно питаться.",
                      "Грузовик с кетчупом устроил коллапс на дороге в Англии",
                      "Россия окажется в центре внимания НАТО",
                      "Охрана элитного поселка сорвала пробежку знаменитого российского юмориста",
                      
    };

    Dictionary<int, Pair<string, double>[]> news_results = new Dictionary<int, Pair<string, double>[]>() {
            {0,  new Pair<string, double>[]  { new Pair<string, double>("PetroChina", 0.95)}},
            {1,  new Pair<string, double>[]  { new Pair<string, double>("Pfizer", 1.15), new Pair<string, double>("Visa", 1.15)}},
            {2,  new Pair<string, double>[]  { new Pair<string, double>("Pfizer", 1.05)}},
            {3,  new Pair<string, double>[]  { new Pair<string, double>("Visa", 0.95), new Pair<string, double>("Walmart", 1.05)}},
            {4,  new Pair<string, double>[]  { new Pair<string, double>("Pfizer", 0.95), new Pair<string, double>("McDonald’s", 0.95)}},
            {5,  new Pair<string, double>[]  {}},
            {6,  new Pair<string, double>[]  {}},
            {7,  new Pair<string, double>[]  {}},
    };

    string[] mail = { "США разрабатывает новый пакет санкций, об этом заявляет Bloobmberg. Подробности пока не известны, эксперты предсказывают направленность санкций на российскую и китайскую нефть.", 
                      "Хакерская атака на JBS, крупнейшего в мире производителя мяса, вынудила эту бразильскую компанию закрыть все мясокомбинаты компании в США.",
                      "Saxo bank предсказывает уменьшение популярности кредитных карт",
                      "Препарат Pfizer-а провалился, побочное действие - опухоли",
                      "В walmart сменился директор, кто знает какие перемены нас ждут...",
                      "Китай рассматривает законопроект об уменьшении фаст-фуда",
                      "Компания Pfizer стала победителем фармацевтической премии «Зеленый Крест 2019» в категории «Компания года» в номинации «Фармацевтическая компания».",
    };

    
    Dictionary<int, Pair<string, double>[]> mail_results = new Dictionary<int, Pair<string, double>[]>() {
            {0,  new Pair<string, double>[] { new Pair<string, double>("PetroChina", 0.95)}},
            {1,  new Pair<string, double>[] {}},
            {2,  new Pair<string, double>[] {new Pair<string, double>("Walmart", 0.95), new Pair<string, double>("McDonald’s", 0.95)}},
            {3,  new Pair<string, double>[] {new Pair<string, double>("Pfizer", 0.9)}},
            {4,  new Pair<string, double>[] {new Pair<string, double>("Walmart", 0.9)}},
            {5,  new Pair<string, double>[] {new Pair<string, double>("McDonald’s", 0.95)}},
            {6,  new Pair<string, double>[] {new Pair<string, double>("Pfizer", 0.95)}},
    };


    // Start is called before the first frame update
    void Start() {
        money = 1000;
        count_transfer = 0;
        cur_money.text = money.ToString() + " $";

        foreach (Text i in container.GetComponentsInChildren<Text>()) 
        {
            if (i.name == "Name") 
            {
                Text price_i = i.GetComponentsInChildren<Text>()[1];
                price_i.text = companies[i.text].first.ToString()+ " $";
            }
        }

        while (list_news.Count < 3) {
            int r_news =  Random.Range(0, count_news);
            if (list_news.IndexOf(r_news) == -1) {
                list_news.Add(r_news);
            }
        }

        while (list_mail.Count < 3) {
            int r_mail =  Random.Range(0, count_mail);
            if (list_mail.IndexOf(r_mail) == -1) {
                list_mail.Add(r_mail);
            }
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    public void Next_month() {
        for (int i = 0; i < count_transfer; i++) {
            GameObject enemy = items[i];
            Destroy(enemy);
        }
        count_transfer = 0;
        for (int i = 0; i < list_news.Count; i++) {
            foreach (Pair<string,double> item in news_results[list_news[i]]) {
                companies[item.first].first *= item.second; 
            }
        }

        for (int i = 0; i < list_mail.Count; i++) {
            foreach (Pair<string,double> item in mail_results[list_mail[i]]) {
                companies[item.first].first *= item.second; 
            }
        }

        foreach (Text i in container.GetComponentsInChildren<Text>()) {
            if (i.name == "Name") {
                Text price_i = i.GetComponentsInChildren<Text>()[1];
                price_i.text = (companies[i.text].first - companies[i.text].first % 0.001).ToString()+ " $";
            }
        }

        list_mail.Clear();
        list_news.Clear();
        while (list_news.Count < 3) {
            int r_news =  Random.Range(0, count_news);
            if (list_news.IndexOf(r_news) == -1) {
                list_news.Add(r_news);
            }
        }

        while (list_mail.Count < 3) {
            int r_mail =  Random.Range(0, count_mail);
            if (list_mail.IndexOf(r_mail) == -1) {
                list_mail.Add(r_mail);
            }
        }
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
                 if (money - x < 0) {
                    kol_shr.text = "";
                    Debug.Log("Мало средств");    
                } else {
                    money -= x;
                    cur_money.text = (money - money % 0.001).ToString() + " $";
                    companies[Name_company.text].second += d;
                    for (int i = 0; i < count_transfer; i++) {
                        items[i].transform.Translate(new Vector3(0, (float) -1.3 , 0));
                    }
                    item.GetComponentsInChildren<Text>()[0].text = Name_company.text;
                    item.GetComponentsInChildren<Text>()[1].text = "-" + x.ToString() + "$";
                    GameObject enemy = Instantiate(item, new Vector2(0, 1470), Quaternion.identity) as GameObject;
                    items[count_transfer] = enemy;
                    enemy.transform.SetParent(LastPurchaseObject.transform, false);
                    count_transfer++;
                }
            }

    	} catch {
            kol_shr.text = "";
    		Debug.Log("Ошибка ввода!");
    	} 
    }




    // Процесс продажи
    public void SellShr(GameObject company) {
        Text Name_company = company.GetComponentsInChildren<Text>()[0];
        InputField kol_shr = company.GetComponentsInChildren<InputField>()[1];
        try {
            double d  = double.Parse(kol_shr.text);
            if (d > companies[Name_company.text].second) {
                kol_shr.text = "";
                Debug.Log("У вас нет столько акций! " + companies[Name_company.text].second.ToString());
                return;
            }
            if (d > 0) {
                kol_shr.text = "";
                double x = companies[Name_company.text].first * d;
                money += x;
                cur_money.text = (money - money % 0.001).ToString() + " $";
                companies[Name_company.text].second -= d;
                for (int i = 0; i < count_transfer; i++) {
                        items[i].transform.Translate(new Vector3(0, (float) -1.3 , 0));
                }
                item.GetComponentsInChildren<Text>()[0].text = Name_company.text;
                item.GetComponentsInChildren<Text>()[1].text = "+" + x.ToString() + "$";
                GameObject enemy = Instantiate(item, new Vector2(0, 1470), Quaternion.identity) as GameObject;
                items[count_transfer] = enemy;
                enemy.transform.SetParent(LastPurchaseObject.transform, false);
                count_transfer++;
            }
        } catch {
            kol_shr.text = "";
            Debug.Log("Ошибка ввода!");
        }
    }


    // Процесс открытия Mail, News, Info
    public void open(GameObject prefab) {
        GameObject enemy = Instantiate(prefab, prefab.transform.position, Quaternion.identity) as GameObject;
        enemy.transform.SetParent(CanvasObject.transform, false);
        if (prefab.name == "Prefab_myshares") {
            foreach (Text i in enemy.GetComponentsInChildren<Text>()) {
                if (i.name == "Name") {
                    i.GetComponentsInChildren<Text>()[1].text = (companies[i.text].first).ToString();
                    i.GetComponentsInChildren<Text>()[2].text = (companies[i.text].second).ToString();
                    i.GetComponentsInChildren<Text>()[3].text = (companies[i.text].first * companies[i.text].second).ToString();
                }
            }
        }
        if (prefab.name == "Prefab_news") {
            int j = 0;
            foreach (Text i in enemy.GetComponentsInChildren<Text>()) {
                if (i.name == "Text_news") {
                    i.text = news[list_news[j]];
                    j++;
                }
            }
        }
        if (prefab.name == "Prefab_mail") {
            int j = 0;
            foreach (Text i in enemy.GetComponentsInChildren<Text>()) {
                if (i.name == "Mail_text") {
                    i.text = mail[list_mail[j]];
                    j++;
                }
            }
        }
    }
}
