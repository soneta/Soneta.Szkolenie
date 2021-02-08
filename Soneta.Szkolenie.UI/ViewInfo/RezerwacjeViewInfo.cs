using Soneta.Business;
using Soneta.CRM;
using System;

namespace Soneta.Szkolenie.UI
{
    public class RezerwacjeViewInfo : ViewInfo
    {
        public RezerwacjeViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "Rezerwacje";

            // Inicjowanie contextu
            InitContext += RezerwacjeViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += RezerwacjeViewInfo_CreateView;
        }

        public class RezerwacjeParams : ContextBase // Klasa parametrów używanych w filtrze. Musi dziedziczyć z klasy ContextBase
        {
            public RezerwacjeParams(Context context) : base(context) {}

            private Maszyna _maszyna = null;
            public Maszyna Maszyna
            {
                get => _maszyna;
                set
                {
                    _maszyna = value;
                    Context.Set(this);
                }
            }

            private Kontrahent _klient = null;
            public Kontrahent Klient
            {
                get => _klient;
                set
                {
                    _klient = value;
                    Context.Set(this);
                }
            }

            private CzyOplacone _czyOplacone;
            public CzyOplacone CzyOplacone
            {
                get => _czyOplacone;
                set
                {
                    _czyOplacone = value;
                    Context.Set(this);
                }
            }
        }

        private void RezerwacjeViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            if (!args.Context.Contains(typeof(RezerwacjeParams)))
                args.Context.Set(new RezerwacjeParams(args.Context)); // dodanie parametrów do kontekstu jeśli nie istnieją
        }

        private void RezerwacjeViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            var parameters = new RezerwacjeParams(args.Context);
            args.Context.Get(out parameters); // ew. pobranie parametrów z kontekstu

            args.View = ViewCreate(parameters); // utworzenie View
            var cond = RowCondition.Empty; // i uzupełnienie warunków wg ustawionych parametrów

            if (parameters.Maszyna != null)
                cond &= new FieldCondition.Equal("Maszyna", parameters.Maszyna);
            if (parameters.Klient != null)
                cond &= new FieldCondition.Equal("Klient", parameters.Klient);
            if (parameters.CzyOplacone != CzyOplacone.Razem)
                cond &= new FieldCondition.Equal("CzyOplacone", parameters.CzyOplacone);

            args.View.Condition = cond;
        }

        private View ViewCreate(RezerwacjeParams pars)
        {
            View view = SzkolenieModule.GetInstance(pars.Session).Rezerwacje.CreateView();
            return view;
        }
    }
}
