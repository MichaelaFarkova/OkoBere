# Oko Bere

Hra Oko Bere v C# s WPF.

## Dokumentace

### Enumerace Stav

Definuje možné stavy hry:
- `Nic`: Neutrální stav.
- `Start`: Nové kolo.
- `Hit`: Hráè žádá další kartu.
- `Stand`: Hráè ukonèuje tah.

### Tøída `MainWindow`

#### Promìnné

- `random`: Pro generování náhodných èísel.
- `karty`: HashSet pro uložení již vygenerovaných karet.
- `dealerovaRuka`, `hracovaRuka`: Celkové hodnoty karet dealera a hráèe.
- `dealerovaSkrytaKartaUri`: URI skryté karty dealera.
- `skore`: Celkové skóre hráèe.
- `dealerovaKartaObrazekId`, `hracovaKartaObrazekId`: Indexy pro sledování poètu zobrazených karet.
- `stav`: Aktuální stav hry, instance výètu Stav.

#### Metody

- `NahodneUriZadnihoObrazkuKarty()`: Vrací URI náhodného zadního obrázku karty.
- `NahodneUriAHodnotaKarty()`: Generuje a vrací URI a hodnotu náhodné karty, která ještì nebyla rozdána.
- `AktualizovatSkore(int noveSkore)`: Aktualizuje a zobrazuje nové skóre.
- `NastavitZdrojObrazku(Image obrazek, string uri)`: Nastavuje zdroj obrázku pro daný `Image` element.
- `DalsiDealerovaKartaObrazek()`: Vrací další obrázek karty dealera podle indexu.
- `DalsiHracovaKartaObrazek()`: Vrací další obrázek karty hráèe podle indexu.
- `VymazatObrazkyKaret()`: Vymaže všechny zobrazené obrázky karet.
- `Vyhra()`, `Prohra()`, `Remiza()`: Metody pro zpracování výsledku hry.
- `MainWindow()`: Konstruktor tøídy, inicializuje komponenty a nastavuje poèáteèní hodnoty.
- `HerniSmycka(object sender, EventArgs e)`: Herní smyèka, která øídí prùbìh hry podle aktuálního stavu.
- `Hit(object sender, RoutedEventArgs e)`: Obsluha události tlaèítka "Hit", nastavuje stav na `Hit`.
- `Stand(object sender, RoutedEventArgs e)`: Obsluha události tlaèítka "Stand", nastavuje stav na `Stand`.
- `DalsiKolo(object sender, RoutedEventArgs e)`: Obsluha události tlaèítka "Další kolo", resetuje hru a pøipravuje nové kolo.
