using Soneta.Business;
using Soneta.Types;
using System.ComponentModel;

[assembly: NewRow(typeof(Soneta.Szkolenie.Rezerwacja))]

namespace Soneta.Szkolenie
{
    public class Rezerwacja : SzkolenieModule.RezerwacjaRow
    {
        // Funkcja definiująca zawartość dropdowna lub lookupa wyswietlanego dla pola.
        // Jej nazwa musi zaczynać się od "GetList", a następnie zawierać nazwę edytowanego property
        public object GetListCzyOplacona()  
        {
            // z enuma wydzielamy do wyświetlenia tylko to, co nas interesuje
            // dzięki temu "Razem" nie pojawi się w dropdownie
            return new[] { 
                CzyOplacone.Nieoplacone, 
                CzyOplacone.Oplacone 
            };  
        }

        protected override void OnAdded() 
        {
            base.OnAdded();
            this.Data = Date.Today;
            this.CzyOplacona = CzyOplacone.Nieoplacone;
        }

        [AttributeInheritance]
        [Description("Zarezerwowany lot")]
        public new Lot Lot
        {
            get => base.Lot;
            set
            {
                base.Lot = value;

                var poRabacie = Percent.Hundred;
                if (Klient != null)
                    poRabacie -= Klient.Rabat;

                CenaLotu = Lot.Cena * poRabacie;
            }
        }

        [AttributeInheritance]
        [Description("Maszyna zarezerwowana na lot")]
        public new Maszyna Maszyna 
        {
            get => base.Maszyna;
            set => value = base.Maszyna;
        }

        [AttributeInheritance]
        [Description("Cena za lot po uwzględnieniu rabatu")]
        public new Currency CenaLotu 
        { 
            get { return base.CenaLotu; }
            set {base.CenaLotu = value;}
        }
    }
}
