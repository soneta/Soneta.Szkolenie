using Soneta.Business.UI;
using Soneta.Szkolenie.UI;

[assembly: FolderView("LotyWidokowe", // wymagane: to ścieżka folderu
    Caption = "Loty widokowe", // niewymagane - jeśli nie podane, pobrane będzie ze ścieżki powyżej
    Priority = 0, // opcjonalne: Priority = 0 umieszcza kafel blisko lewej górnej strony widoku kafli
    Description = "Szkolenie techniczne - przykład dodatku", // opcjonalne: opis poniżej tytułu kafla
    BrickColor = FolderViewAttribute.BlueBrick, // opcjonalne: Kolor kafla
    Icon = "TableFolder.ico" // opcjonalne: Ikona wyświetlana na kaflu
                             // Więcej nie ma potrzeby definiować bo jest to kafel "organizacyjny" - przechodzący do widoku innych kafli
)]

[assembly: FolderView("LotyWidokowe/Klienci",
    Priority = 0,
    Description = "Klienci",
    TableName = "Kontrahenci", // Tabela, której widok będzie prezentowany
    ViewType = typeof(KlienciViewInfo), // ViewInfo, które będzie użyte do wyświetlenia listy
    BrickColor = FolderViewAttribute.YellowBrick
)]

[assembly: FolderView("LotyWidokowe/Loty",
    Caption = "Katalog lotów",
    Priority = 100,
    Description = "Katalog lotów",
    TableName = "Loty",
    ViewType = typeof(KatalogLotowViewInfo)
)]

[assembly: FolderView("LotyWidokowe/Maszyny",
    Caption = "Katalog maszyn",
    Priority = 200,
    Description = "Katalog maszyn",
    TableName = "Maszyny",
    ViewType = typeof(KatalogMaszynViewInfo)
)]

[assembly: FolderView("LotyWidokowe/Rezerwacje",
    Priority = 300,
    Description = "Lista rezerwacji",
    TableName = "Rezerwacje",
    ViewType = typeof(RezerwacjeViewInfo),
    BrickColor = FolderViewAttribute.BlackBrick
)]
