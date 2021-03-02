
using Soneta.Business;
using Soneta.CRM;

namespace Soneta.Szkolenie.UI
{
    public class KlienciViewInfo : ViewInfo
    {
        public KlienciViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "Klienci";

            // Inicjowanie contextu
            InitContext += KlienciViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += KlienciViewInfo_CreateView;
        }

        private void KlienciViewInfo_InitContext(object sender, ContextEventArgs args)
        {
        }

        void KlienciViewInfo_CreateView(object sender, CreateViewEventArgs args) {
            var view = CRMModule.GetInstance(args.Session).Kontrahenci.WgKodu.CreateView();
            view.Condition &= new FieldCondition.Equal("IsStandard",false);
            args.View = view;
        }
    }
}
