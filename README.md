# Oko Bere

Hra Oko Bere v C# s WPF.

## Dokumentace

### Enumerace Stav

Definuje mo�n� stavy hry:
- `Nic`: Neutr�ln� stav.
- `Start`: Nov� kolo.
- `Hit`: Hr�� ��d� dal�� kartu.
- `Stand`: Hr�� ukon�uje tah.

### T��da `MainWindow`

#### Prom�nn�

- `random`: Pro generov�n� n�hodn�ch ��sel.
- `karty`: HashSet pro ulo�en� ji� vygenerovan�ch karet.
- `dealerovaRuka`, `hracovaRuka`: Celkov� hodnoty karet dealera a hr��e.
- `dealerovaSkrytaKartaUri`: URI skryt� karty dealera.
- `skore`: Celkov� sk�re hr��e.
- `dealerovaKartaObrazekId`, `hracovaKartaObrazekId`: Indexy pro sledov�n� po�tu zobrazen�ch karet.
- `stav`: Aktu�ln� stav hry, instance v��tu Stav.

#### Metody

- `NahodneUriZadnihoObrazkuKarty()`: Vrac� URI n�hodn�ho zadn�ho obr�zku karty.
- `NahodneUriAHodnotaKarty()`: Generuje a vrac� URI a hodnotu n�hodn� karty, kter� je�t� nebyla rozd�na.
- `AktualizovatSkore(int noveSkore)`: Aktualizuje a zobrazuje nov� sk�re.
- `NastavitZdrojObrazku(Image obrazek, string uri)`: Nastavuje zdroj obr�zku pro dan� `Image` element.
- `DalsiDealerovaKartaObrazek()`: Vrac� dal�� obr�zek karty dealera podle indexu.
- `DalsiHracovaKartaObrazek()`: Vrac� dal�� obr�zek karty hr��e podle indexu.
- `VymazatObrazkyKaret()`: Vyma�e v�echny zobrazen� obr�zky karet.
- `Vyhra()`, `Prohra()`, `Remiza()`: Metody pro zpracov�n� v�sledku hry.
- `MainWindow()`: Konstruktor t��dy, inicializuje komponenty a nastavuje po��te�n� hodnoty.
- `HerniSmycka(object sender, EventArgs e)`: Hern� smy�ka, kter� ��d� pr�b�h hry podle aktu�ln�ho stavu.
- `Hit(object sender, RoutedEventArgs e)`: Obsluha ud�losti tla��tka "Hit", nastavuje stav na `Hit`.
- `Stand(object sender, RoutedEventArgs e)`: Obsluha ud�losti tla��tka "Stand", nastavuje stav na `Stand`.
- `DalsiKolo(object sender, RoutedEventArgs e)`: Obsluha ud�losti tla��tka "Dal�� kolo", resetuje hru a p�ipravuje nov� kolo.
