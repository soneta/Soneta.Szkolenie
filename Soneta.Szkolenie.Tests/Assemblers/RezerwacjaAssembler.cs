using Soneta.Business;
using Soneta.Test;
using Soneta.CRM;

namespace Soneta.Szkolenie.Tests
{
    static class RezerwacjaAssembler
    {
        internal static IRowBuilder<Rezerwacja> NrRezerwacji(this IRowBuilder<Rezerwacja> builder, string value)
           => builder.Enqueue(r => r.NrRezerwacji = value);

        internal static IRowBuilder<Rezerwacja> Data(this IRowBuilder<Rezerwacja> builder, string value)
           => builder.Enqueue(r => r.Data = Types.Date.Parse(value));

        internal static IRowBuilder<Rezerwacja> Klient(this IRowBuilder<Rezerwacja> builder, string value)
           => builder.Enqueue(r => r.Klient = r.Session.Get<CRMModule>().Kontrahenci.WgKodu[value]);

        internal static IRowBuilder<Rezerwacja> Lot(this IRowBuilder<Rezerwacja> builder, string value)
           => builder.Enqueue(r => r.Lot = r.Session.Get<SzkolenieModule>().Loty.WgKod[value]);

        internal static IRowBuilder<Rezerwacja> Maszyna(this IRowBuilder<Rezerwacja> builder, string value)
           => builder.Enqueue(r => r.Maszyna = r.Session.Get<SzkolenieModule>().Maszyny.WgNrBoczny[value]);
    }
}
