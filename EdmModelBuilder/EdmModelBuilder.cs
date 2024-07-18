using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataEpplus.Models;

namespace ODataEpplus.EdmModelBuilder
{
    public static class EdmModelBuilder
    {
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<CRM>("CRMData");
            return builder.GetEdmModel();
        }
    }
}
