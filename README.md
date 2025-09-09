BookOrderApp
📚 Projektbeschreibung

BookOrderApp ist eine kleine Desktop-Anwendung, die die Verwaltung von Buchbestellungen ermöglicht. Sie dient als Testaufgabe und Beispiel für den Einsatz von C#, Avalonia UI und SQLite.

Funktionen der Anwendung:

Hinzufügen neuer Buchbestellungen

Löschen vorhandener Bestellungen

Anzeige aller Bestellungen

Suche nach Bestellungen nach Lesername oder Buch

Verwaltung mehrerer Bücher pro Bestellung

Verwaltung der Bücher-Datenbank (Titel, Autor, 8-stellige Buchnummer)

Die Anwendung nutzt Entity Framework Core für die Datenbankzugriffe und AvaloniaUI für die plattformübergreifende GUI.

🏗️ Technologien

C# 11 / .NET 8

AvaloniaUI 11 (Cross-Plattform GUI)

Entity Framework Core mit SQLite (lokale Datenbank)

Git für Versionskontrolle

🗂️ Projektstruktur
BookOrderApp/
│
├─ BookOrder/                  # Hauptprojekt
│  ├─ Models/                  # Datenmodelle: Book, Order, OrderBook
│  ├─ Data/                    # DbContext und Factory
│  ├─ Services/                # Business-Logik: OrderService, BookService
│  ├─ Views/                   # Avalonia UI Seiten
│  ├─ App.axaml / App.axaml.cs # Avalonia Startpunkt
│  └─ MainWindow.axaml         # Hauptfenster
│
├─ scripts/                    # SQL Skripte zur DB-Erstellung
│  └─ create_db.sql            # Tabellen und Beispiel-Daten
│
└─ README.md

⚙️ Installation

Repository klonen:

git clone https://github.com/dein-benutzername/BookOrderApp.git
cd BookOrderApp


Abhängigkeiten installieren:

dotnet restore


Datenbank erstellen (SQLite):

sqlite3 orders.db < scripts/create_db.sql

🚀 Anwendung starten
dotnet run --project BookOrder/BookOrder.csproj


Die Hauptseite zeigt die Optionen:

Bestellungen anzeigen

Neue Bestellung hinzufügen

Bücher verwalten

📝 Features
1. Bestellung hinzufügen

Eingabe: Name, Adresse, Buchnummer oder Buch aus Liste auswählen

Mehrere Bücher pro Bestellung möglich

Menge pro Buch angeben

Anzeige aktueller Bestellpositionen

2. Bestellungen anzeigen & suchen

Alle Bestellungen in einer Tabelle

Suchfunktion nach Lesername oder Buchtitel

Möglichkeit, Bestellungen zu bearbeiten oder zu löschen

3. Bücher verwalten

Bücher hinzufügen, löschen und suchen

8-stellige Buchnummern zur eindeutigen Identifikation

Vorschau aller Bücher in der Liste

💡 Besonderheiten

Mehrere Bücher pro Bestellung, auch mehrfach dasselbe Buch möglich

Avalonia UI für plattformübergreifende Desktop-Anwendungen

Datenbank kann einfach mit SQL-Skripten wiederhergestellt werden

Clean Code Prinzipien: Services, Models, Views klar getrennt

✅ Fazit

Die Anwendung demonstriert:

Objektorientiertes Design

EF Core / SQLite für persistente Daten

Unit-Testing und Testbarkeit

Moderne GUI mit Avalonia

Für eine Bewerbung zeigt das Projekt vollständige Implementierung, Testbarkeit und gute Struktur.
