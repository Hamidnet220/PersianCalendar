using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TaminIranSocialInsurance
{
    [Guid("7D94E2D1-0755-4B78-ABCE-0DF56A26077C")]
    [ComVisible(true)]
    public interface IDswRepository 
    {
        int Add(DskWor00 entity);
        int Remove(DskWor00 entity);
        int Update(DskWor00 entity);
        void RefreshConnection(string connectionString);
        List<DskWor00> Find(params object[] keys);
        List<DskWor00> GetAll();
        void AddStandardDbfFiles(string path, bool create);
        string ConvertToUnicode(string inputString);
        string ConvertToIranSystem(string inputString);
        void FoxproToNetClassGenerator(string path, string connectonString, string className);
        int Count();


    }
}
