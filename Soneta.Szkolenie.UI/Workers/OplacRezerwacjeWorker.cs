using System;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Szkolenie;
using Soneta.Szkolenie.UI;
using Soneta.Tools;
using Soneta.Types;

[assembly: Worker(typeof(OplacRezerwacjeWorker), typeof(Rezerwacja))]

namespace Soneta.Szkolenie.UI
{
    public class OplacRezerwacjeWorker
    {
        [Context]
        public Context Context { get; set; }

        [Context]
        public Rezerwacja Rezerwacja { get; set; }

        [Action("Loty widokowe/Opłać rezerwcję", Mode = ActionMode.SingleSession | ActionMode.OnlyForm, Target = ActionTarget.Menu | ActionTarget.Toolbar)]
        public MessageBoxInformation OplacRezerwacje()
        {
            return new MessageBoxInformation("Opłać rezerwację?".Translate())
            {
                Text = "Czy oznaczyć rezerwację jako zapłaconą".Translate(),
                YesHandler = OplataRezerwacji,
                NoHandler = () => null
            };
        }

        public bool IsVisibleOplacRezerwacje()
        {
            var uiLocation = Context[typeof(UILocation)] as UILocation;
            return Rezerwacja.State != RowState.Added &&
                (uiLocation.FolderNormalizedPath == "LotyWidokowe/Rezerwacje" || uiLocation.FolderNormalizedPath == "LotyWidokowe/Klienci");
        }

        public bool IsEnabledOplacRezerwacje()
        {
            return Rezerwacja.CzyOplacona != CzyOplacone.Oplacone;
        }

        private object OplataRezerwacji()
        {
            if (Rezerwacja == null)
            {
                return "Brak rezerwacji w kontekście";
            }

            if (Rezerwacja.CzyOplacona != CzyOplacone.Nieoplacone)
                return "Rezerwacja jest już opłacona.";

            using (var t = Rezerwacja.Session.Logout(true))
            {
                Rezerwacja.CzyOplacona = CzyOplacone.Oplacone;
                t.Commit();
            }

            return new MessageBoxInformation("Opłacona".Translate())
            {
                Text = "Rezerwacja opłacona".Translate(),
                OKHandler = () => FormAction.SaveAndClose
            };
        }
    }
}
