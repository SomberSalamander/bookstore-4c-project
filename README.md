# Projektová dokumentace

## 1. Úvod

Webová aplikace knihkupectví
- **Název projektu:** Bookstore / Book e-Shop
- **Autor:** Barbora Volšická
- **Datum:** 15.03.2026
- **Verze:** 1.1

### 1.1 Popis projektu
Cílem projektu je vytvořit webovou aplikaci pro online knihkupectví. Po přihlášení se uživateli zobrazí seznam knih uložených v databázi, které může přidat a odebrat z košíku a upravovat koupené množství knih. Uživatel s rolí administrátora může vytvořit nové záznamy knih, měnit jejich údaje a odstraňovat je z databáze.

### 1.2 Cíl projektu
- Poskytnout přehledné uživatelské rozhraní zákazníkovi pro objednávku knih
- Umožnit správu knih přes webové rozhraní
- Zajistit průběžné a bezpečené ukládání dat do databáze

### 1.3 Použité technologie
- **Frontend:** HTML, CSS, JavaScript, Bootstrap
- **Backend:** C#, ASP.NET Core, Entity Framework Core
- **Databáze:** MySQL
- **Další:** phpMyAdmin

---

## 2. Požadavky
- Autentizace a autorizace uživatele
- Výskyt 2 další databázových entit (kromě uživatele), které mají mezi sebou vazbu pomocí cizího klíče
- Zobrazení těchto db. entit (book, cartItem) v listu
- Přidání nových záznamů entit
- Úprava existujících entit
- Smazání entit
- Přehledný design webu

## 2.1 Uživatelské role
| Role | Popis |
|------|-------|
| **Neregistrovaný uživatel** | Může se pouze přihlásit nebo registrovat. Nemůže procházet seznam knih, ani ostatní stránky. |
| **Registrovaný uživatel (Customer)** | Může přidávat knihy do košíku, upravovat množství a odebírat položky. |
| **Administrátor** | Může dělat to samé co registrovaný zákazník, zároveň má přístup k administračním stránkám a může provádět CRUD operace s knihami. |

---

## 3. Návrh databáze

### 3.1 Tabulky

#### Tabulka: books
| Sloupec | Datový typ | Popis |
|-|-|-|
| bookId | INT (PK, AI)  | Jedinečný identifikátor |
| title | VARCHAR(50) | Název knihy |
| author | VARCHAR(50) | Jméno autora |
| publisher | VARCHAR(50)  | Nakladatelství |
| description | TEXT | Popis |
| price | FLOAT | Cena |
| stock | INT | Kusů skladem |
| img | VARCHAR(50) | Název souboru s obrázkem |

#### Tabulka: users
| Sloupec | Datový typ | Popis |
|-|-|-|
| userId | INT (PK, AI)  | Jedinečný identifikátor |
| email | VARCHAR(50) | Email uživatele |
| password | TEXT | Heslo |

#### Tabulka: carts
| Sloupec | Datový typ | Popis |
|-|-|-|
| cartId | INT (PK, AI)  | Jedinečný identifikátor |
| userId | INT (FK) | Cizí klíč (uživatel) |
| createdAt | DATETIME | Datum a čas vytvoření košíku |

#### Tabulka: cartItems
| Sloupec | Datový typ | Popis |
|-|-|-|
| cartItemId | INT (PK, AI)  | Jedinečný identifikátor |
| cartId | INT (FK) | Cizí klíč (košík) |
| bookId | INT (FK) | Cizí klíč (kniha) |
| quantity | INT | Množství |

### 3.2 Vztahy

- carts.cartId < cartItems.cartId
- books.bookId < cartItems.bookId
- carts.userId - users.userId

---

## 4. Návrh aplikace

### 4.1 Struktura webu
- **Login.cshtml** - přihlášení
- **Register.cshtml** - registrace
- **List.cshtml** - seznam knih
- **Detail.cshtml** - dodatečné informace knihy
- **Cart.cshtml** - košík přihlášeného uživatele
- **AdminList.cshtml** - stručný seznam knih pro administrátora
- **AdminBook.cshtml** - formulář knihy při přidávání či úpravě záznamu
- **_AuthLayout.cshtml, _BooksLayout.cshtml** - šablony
- **BooksDbContext.cs** - spojení s databází

### 4.3 Logika aplikace
- Připojení k databázi pomocí Entity Framework Core
- CRUD operace

## 4.4 Autentizace a autorizace

### Autentizace
- Probíhá pomocí claimů a **Cookie Autentizace**
- Po přihlášení se vytvoří cookie s identitou uživatele

### Autorizace
- Role jsou řešeny pomocí claimů
- Administrátorské stránky jsou chráněny atributem:
```csharp
[Authorize(Roles = "Admin")]
```
- (Běžný uživatel nemá přístup do administrační sekce)

---

## 4.5 Detailnější popis logiky aplikace

### Práce s košíkem
- Každý uživatel má právě jeden aktivní košík
- Položky košíku jsou ukládány do tabulky *cartItems*
- Při změně množství se kontroluje zda již kniha v košíku je (jestli ano, tak se pouze zvýší množství o 1 kus), zároveň se nedá zadat hodnota vyšší než je na skladu
- Při smazání knihy administrátorem se odstraní i její položky v košících

### CRUD operace knih
- Probíhají přes formulář *AdminBook.cshtml*
- Obrázek knihy je uložen jako název souboru (obrázek musí být uložen ve složce *\wwwroot\img\books\**)

---

## 5. Implementace

### 5.1 Struktura projektu
dle MVC struktury; nejdůležitější složky a soubory:

- **/wwwroot** - obsahují všechny soubory se styly, obrázky apod.
  - /css, /img, /js, /lib/bootstrap... 
- **/Controllers** - všechny kontrolery
  - /AuthController.cs, /BooksController.cs, ...
- **/Data**
  - /BooksDbContext.cs - spojení s databází
- **/Entities** - třídy databázových entit
  - /Book.cs, /User.cs, /Cart.cs, /CartItem.cs
- **/Models** - modely rozdělené dle použití
  - /Auth/LoginViewModel.cs
  - /Books/AdminBookModel.cs, /CartItemDetailViewModel.cs, /CartViewModel.cs
- **/Views** - všechny views, rozdělené do těchto složek dle použití:
  - /Auth
  - /Books
- **/Program.cs**

### 5.2 Bezpečnostní opatření
- Ošetření vstupu při změně množství košíku -> nemůže objednat víc než je na skladu
- Validace emailu při registraci a loginu
- Validace dat formuláře při tvorbě či úpravě záznamů knih
- Ošetření případu odstranění knihy existující v košíku -> také ji odstraní

---

## 6. Závěr
Projekt splnil svůj účel - umožňuje správu knih přes jednoduché webové rozhraní dle zadání. V tomto projektu jsem si nejen procvičila existující vědomosti, ale i naučila, jak efektivněji a přehledněji programovat mimo jiné. Zjistila jsem, jak pracovat s architekturou MVC, a jak autentizovat a autorizovat uživatele.

---

## 7. Přílohy
Všechny přiložené dokumenty najdete ve složce */docs*:
- ER diagram (obrázek, texťák)
- Počáteční Wireframe ve Figmě
- Původní zadání

## 8. Changelog (verze 1.1)

| Verze | Datum | Popis |
|-------|--------|--------|
| **1.0** | 08.03.2026 | Struktura, většina informací dokumentace |
| **1.1** | 15.03.2026 | Poslední změny - formát, doplnění nedostatků |