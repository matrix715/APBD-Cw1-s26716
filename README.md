# Uczelniana wypożyczalnia sprzętu

## Opis projektu
Projekt przedstawia konsolową aplikację w języku C#, która obsługuje uczelnianą wypożyczalnię sprzętu.  
System umożliwia rejestrowanie użytkowników i sprzętu, wypożyczanie urządzeń, zwroty, oznaczanie sprzętu jako niedostępnego oraz generowanie raportów podsumowujących stan wypożyczalni.

Projekt został przygotowany w ramach ćwiczenia z programowania obiektowego w C#.

## Funkcjonalności
Aplikacja umożliwia:
- dodanie nowego użytkownika do systemu,
- dodanie nowego sprzętu danego typu,
- wyświetlenie listy całego sprzętu z aktualnym statusem,
- wyświetlenie sprzętu dostępnego do wypożyczenia,
- wypożyczenie sprzętu użytkownikowi,
- zwrot sprzętu wraz z naliczeniem kary za opóźnienie,
- oznaczenie sprzętu jako niedostępnego,
- wyświetlenie aktywnych wypożyczeń danego użytkownika,
- wyświetlenie listy przeterminowanych wypożyczeń,
- wygenerowanie raportu podsumowującego stan wypożyczalni.

## Model domenowy
W projekcie występują następujące elementy domenowe:
- `Equipment` – klasa abstrakcyjna reprezentująca wspólne cechy sprzętu,
- `Laptop`, `Camera`, `Projector` – typy sprzętu dziedziczące po `Equipment`,
- `User` – klasa abstrakcyjna reprezentująca użytkownika systemu,
- `Student`, `Teacher` – typy użytkowników dziedziczące po `User`,
- `Rental` – klasa opisująca wypożyczenie sprzętu.

## Struktura projektu
Projekt został podzielony na dwie główne części:
- `Models` – klasy domenowe opisujące obiekty systemu,
- `Services` – klasy odpowiedzialne za logikę biznesową.

### Modele
- `Equipment` – przechowuje wspólne dane sprzętu, takie jak identyfikator, nazwa i status,
- `Laptop` – przechowuje dodatkowo procesor i RAM,
- `Camera` – przechowuje dodatkowo liczbę megapikseli i typ,
- `Projector` – przechowuje dodatkowo rozdzielczość i jasność,
- `User` – przechowuje identyfikator, imię i nazwisko,
- `Student` – przechowuje numer studenta oraz limit wypożyczeń równy 2,
- `Teacher` – przechowuje wydział oraz limit wypożyczeń równy 5,
- `Rental` – przechowuje informacje o użytkowniku, sprzęcie, dacie wypożyczenia, terminie zwrotu, dacie zwrotu i karze.

### Serwisy
- `UserService` – odpowiada za przechowywanie i wyszukiwanie użytkowników,
- `EquipmentService` – odpowiada za przechowywanie sprzętu, wyświetlanie dostępnych urządzeń i oznaczanie sprzętu jako niedostępnego,
- `RentalService` – odpowiada za wypożyczanie i zwracanie sprzętu oraz kontrolę limitów,
- `ReportService` – odpowiada za generowanie raportów oraz pobieranie przeterminowanych i aktywnych wypożyczeń.

## Decyzje projektowe

Logika biznesowa nie została umieszczona w `Program.cs`.  
`Program.cs` pełni rolę interfejsu konsolowego i obsługi menu, natomiast operacje systemowe zostały przeniesione do klas serwisowych.

Dziedziczenie zostało zastosowane tam, gdzie wynika ono bezpośrednio z modelu domeny:
- `Laptop`, `Camera`, `Projector` dziedziczą po `Equipment`,
- `Student` i `Teacher` dziedziczą po `User`.

## Kohezja, coupling i odpowiedzialności klas
W projekcie starano się zadbać o czytelny podział odpowiedzialności:
- klasy z folderu `Models` reprezentują dane i obiekty domenowe,
- klasy z folderu `Services` realizują logikę operacji,
- `Program.cs` odpowiada za komunikację z użytkownikiem przez konsolę.

Kohezja została zachowana przez to, że każda klasa ma jedno główne zadanie.  
Przykładowo `RentalService` zajmuje się tylko wypożyczeniami i zwrotami, a `ReportService` tylko raportowaniem.

Sprzężenie między klasami zostało ograniczone przez rozdzielenie odpowiedzialności między modele i serwisy.  
Dzięki temu zmiana sposobu raportowania nie wymaga zmian w klasach modelu, a zmiana danych użytkownika nie wpływa bezpośrednio na logikę sprzętu.

## Reguły biznesowe
W projekcie zaimplementowano następujące reguły:
- student może mieć maksymalnie 2 aktywne wypożyczenia,
- nauczyciel może mieć maksymalnie 5 aktywnych wypożyczeń,
- sprzęt oznaczony jako `Unavailable` nie może zostać wypożyczony,
- sprzęt wypożyczony otrzymuje status `Rented`,
- opóźniony zwrot skutkuje naliczeniem kary,
- kara wynosi 10 jednostek za każdy dzień opóźnienia.



## Przykładowe dane startowe
Po uruchomieniu programu do pamięci ładowani są przykładowi użytkownicy i sprzęt:
- użytkownicy: `Student`, `Teacher`,
- sprzęt: `Laptop`, `Camera`, `Projector`.

Dzięki temu można od razu testować funkcje wypożyczania, zwrotów i raportowania.

## Instrukcja uruchomienia

1. Sklonuj repozytorium z GitHub.
2. Otwórz projekt w Visual Studio 2022.
3. Ustaw projekt `Cwiczenia2` jako projekt startowy.
4. Uruchom aplikację.
5. Po uruchomieniu program ładuje przykładowe dane do pamięci i wyświetla menu tekstowe.
