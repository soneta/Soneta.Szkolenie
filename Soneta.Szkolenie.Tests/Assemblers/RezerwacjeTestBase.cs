using Soneta.Business.UI;
using Soneta.Szkolenie.UI;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soneta.Szkolenie.Tests
{
    public abstract class RezerwacjeTestBase : SzkolenieTestBase
    {
        protected void RunUstawRabatWorker(string rabat, bool dodawac, bool obnizac)
        {
            InTransaction(() =>
            {
                var worker = Context.CreateObject(null,
                    typeof(UstawRabatWorker),
                    new UstawRabatWorkerParams(Context)
                    {
                        Rabat = Percent.Parse(rabat),
                        DodawacRabaty = dodawac,
                        ObnizacRabaty = obnizac
                    }) as UstawRabatWorker;

                if (worker.UstawRabat() is MessageBoxInformation result)
                    result.YesHandler();
            });

            SaveDispose();
        }
    }
}
