# Projektová dokumentace

## Úvod

Webová aplikace knihkupectví
- **Název projektu:** Bookstore / Book e-Shop
- **Autor:** Barbora Volšická
- **Datum:** 08.03.2026
- **Verze:** 1.2

### Popis projektu
Cílem projektu je vytvořit webovou aplikaci pro online knihkupectví. Po přihlášení se uživateli zobrazí seznam knih uložených v databázi, které může přidat a odebrat z košíku a upravovat koupené množství knih. Uživatel s rolí administrátora může vytvořit nové záznamy knih, měnit jejich údaje a odstraňovat je z databáze.

### Cíl projektu
- Poskytnout přehledné uživatelské rozhraní zákazníkovi pro objednávku knih
- Umožnit správu knih přes webové rozhraní
- Zajistit průběžné a bezpečené ukládání dat do databáze

### Použité technologie
- **Frontend:** HTML, CSS, JavaScript, Bootstrap
- **Backend:** C#, ASP.NET Core, Entity Framework Core
- **Databáze:** MySQL
- **Další:** phpMyAdmin

---

## 2. Požadavky
- Autentizace a autorizace uživatele
- Výskyt 2 další databázových entit (kromě uživatele), které mají mezi sebou vazbu pomocí cizího klíče
- Zobrazení těchto db. entit (book, cart item) v listu
- Přidání nových záznamů entit
- Úprava existujících entit
- Smazání entit
- Přehledný design webu

---

## 3. Návrh databáze

### 3.1 Tabulky

#### Tabulka: Books
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

#### Tabulka: Users
| Sloupec | Datový typ | Popis |
|-|-|-|
| userId | INT (PK, AI)  | Jedinečný identifikátor |
| email | VARCHAR(50) | Email uživatele |
| password | TEXT | Heslo |

#### Tabulka: Carts
| Sloupec | Datový typ | Popis |
|-|-|-|
| cartId | INT (PK, AI)  | Jedinečný identifikátor |
| userId | INT (FK) | Cizí klíč (uživatel) |
| createdAt | DATETIME | Datum a čas vytvoření košíku |

#### Tabulka: CartItems
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

---

## 5. Implementace

### 5.1 Struktura projektu
dle MVC struktury; nejdůležitější složky a soubory:

- **/wwwroot** - obsahují všechny soubory se styly, obrázky apod.
  - /css, /img, /js, ... 
- **/Controllers** - všechny kontrolery
  - /AuthController.cs, /BooksController.cs, ...
- **/Data**
  - /BooksDbContext.cs - spojení s databází
- **/Entities** - třídy databázových entit
  - /Book.cs, /User.cs, ...
- **/Models** - modely rozdělené dle použití
  - /Auth/LoginViewModel.cs
  - /Books/AdminBookModel.cs, /CartItemDetailViewModel.cs, /CartViewModel.cs
- **/Views** - všechny views, rozdělené dle použití
  - /Auth
  - /Books
**/Program.cs**

### 5.2 Bezpečnostní opatření
- Ošetření vstupu při změně množství košíku -> nemůže objednat víc než je na skladu
- Validace emailu při registraci a loginu
- Validace dat formuláře při tvorbě či úpravě záznamů knih
- Ošetření případu odstranění knihy existující v košíku -> také ji odstraní

---

## 7. Závěr
Projekt splnil svůj účel - umožňuje správu knih přes jednoduché webové rozhraní dle zadání. V tomto projektu jsem si nejen procvičila existující vědomosti, ale i naučila, jak efektivněji a přehledněji programovat mimo jiné. Zjistila jsem, jak pracovat s architekturou MVC, a jak autentizovat a autorizovat uživatele.

---

## 8. Přílohy
Všechny přiložené dokumenty najdete ve složce *docs*:
- ER diagram (obrázek, texťák)
- Wireframy ve Figmě
- Původní zadání