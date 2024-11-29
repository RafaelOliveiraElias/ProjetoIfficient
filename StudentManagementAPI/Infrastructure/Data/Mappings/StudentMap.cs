using CsvHelper.Configuration;

namespace Infrastructure.Data.Mappings
{
    public sealed class StudentMap : ClassMap<RawStudent>
    {
        public StudentMap()
        {
            Map(m => m.Registration).Name("matricula");
            Map(m => m.Name).Name("nome");
            Map(m => m.matematica).Name("matematica");
            Map(m => m.portugues).Name("portugues");
            Map(m => m.biologia).Name("biologia");
            Map(m => m.quimica).Name("quimica");
        }
    }
}
