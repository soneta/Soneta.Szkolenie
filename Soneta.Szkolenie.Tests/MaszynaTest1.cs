using NUnit.Framework;
using Soneta.Business;
using Soneta.Test;
using Soneta.Types;

namespace Soneta.Szkolenie.Tests {

    public class MaszynyTest1 : DbTransactionTestBase
    {
        public override void ClassSetup()
        {
            LoadAssembly("Soneta.Szkolenie");
            base.ClassSetup();
        }

        [Test]
        public void NowaMaszynaTest1()
        {
            var maszyna = Add(new Maszyna());

            InUITransaction(() => maszyna.NrBoczny = "SP-DEF");
            InUITransaction(() => maszyna.Producent = "Cessna Ltd.");
            InUITransaction(() => maszyna.Model = "172p SkyHawk");
            InUITransaction(() => maszyna.DataProd = Date.Parse("1998-10-12"));

            SaveDispose();

            maszyna = Get(maszyna);

            Assert.AreEqual("SP-DEF", maszyna.NrBoczny, "Źle podstawiony nr boczny");
            Assert.AreEqual("Cessna Ltd.", maszyna.Producent, "Źle podstawiony producent");
            Assert.AreEqual("172p SkyHawk", maszyna.Model, "Źle podstawiony model");
            Assert.AreEqual(Date.Parse("1998-10-12"), maszyna.DataProd, "Źle podstawiona data produkcji");
        }
    }
}